using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
	const float timeAfterShoot = 3f;

	[Header("Reference")]
	[SerializeField] GunData gunData;
	[SerializeField] Transform muzzle;
	[SerializeField] ParticleSystem muzzleParticleSystem;
	[SerializeField] GameObject bullet;
	[SerializeField] Camera fpsCamera;
	[SerializeField]
	TMP_Text bulletText;
	float timeSinceLastShoot;

	
	private void Awake()
	{
		gunData = Instantiate(gunData);
		gunData.currentAmo = gunData.magSize;
		gunData.reloading = false;
		gunData.totalAmo = gunData.startMagSize;


	}
	private void Start()
	{

		

		


	}
	
	private void Update()
	{
		timeSinceLastShoot += Time.deltaTime;
		Debug.DrawRay(muzzle.position, muzzle.forward);

		DisplayBullets();
		
	}
	private void OnEnable()
	{
		PlayerShoot.shootInput += Shoot;
		PlayerShoot.reloadInput += StartReload;
	}

	private void OnDisable()
	{
		PlayerShoot.shootInput -= Shoot;
		PlayerShoot.reloadInput -= StartReload;
	}
	public void StartReload()
	{
		if (!gunData.reloading)
		{
			StartCoroutine(Reload());
		}


	}


	private IEnumerator Reload()
	{
		gunData.reloading = true;

		yield return new WaitForSeconds(gunData.reloadTime);

		if (gunData.totalAmo <= 0)
		{
			
			gunData.totalAmo = 0;
			gunData.currentAmo = 0;
		}
		else
		{
			if (gunData.currentAmo <= 0)
			{
				
				if (gunData.totalAmo >= gunData.magSize)
				{
				
					gunData.totalAmo -= gunData.magSize;
					gunData.currentAmo = gunData.magSize;
				}
				else
				{
				
					gunData.currentAmo = gunData.totalAmo;
					gunData.totalAmo = 0;
				}
			}
			else
			{
				
				int neededAmo = gunData.magSize - gunData.currentAmo;
				if (gunData.totalAmo >= neededAmo)
				{
					
					gunData.totalAmo -= neededAmo;
					gunData.currentAmo += neededAmo;
				}
				else
				{
				
					gunData.currentAmo += gunData.totalAmo;
					gunData.totalAmo = 0;
				}
			}
			
			gunData.reloading = false;
		}
	}
	private bool CanShoot() => !gunData.reloading && timeSinceLastShoot > 1f / (gunData.fireRate / 60f);

	public void Shoot()
	{
		
		if (gunData.currentAmo > 0)
		{
			if (CanShoot() && !GameManager.instance.isGameOver)
			{
				Debug.Log($"Shoot");

				Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

				Vector3 targetPoint;
				int layerMask = ~LayerMask.GetMask("DetectionTrigger");

				if (Physics.Raycast(ray, out RaycastHit hit,Mathf.Infinity,layerMask))
				{
					
					targetPoint = hit.point;
					
					IDamageable damageable = hit.transform.GetComponent<IDamageable>();
					damageable?.TakeDamage(gunData.damage);
						

				}
				else
				{
					Debug.Log("No hit detected.");
					targetPoint = ray.GetPoint(75);

				}

				Vector3 directionWithoutSpread = targetPoint - muzzle.position;

				float x = Random.Range(-gunData.spread, gunData.spread);
				float y = Random.Range(-gunData.spread, gunData.spread);

				Vector3 directionWtihSpread = directionWithoutSpread + new Vector3(x, y, 0);

				GameObject currentBullet = Instantiate(bullet, muzzle.position, muzzle.rotation);
				currentBullet.transform.forward = directionWtihSpread.normalized;

				currentBullet.GetComponent<Rigidbody>().AddForce(directionWtihSpread.normalized * gunData.shootForce, ForceMode.Impulse);
				//currentBullet.GetComponent<Rigidbody>().AddForce(fpsCamera.transform.up * upwardForce, ForceMode.Impulse);
				Destroy(currentBullet, timeAfterShoot);


				gunData.currentAmo--;

				timeSinceLastShoot = 0;
				OnGunShoot();
				if (CheckIfAmoEmpty())
				{
					StartCoroutine(Reload());
					Debug.Log("Reloading");
				}
			}
			
		}
	}


	public bool CheckIfAmoEmpty() => gunData.currentAmo <= 0;

	
	private void OnGunShoot()
	{

		if (muzzleParticleSystem != null)
		{

			Instantiate(muzzleParticleSystem, muzzle.position, muzzle.rotation);

		}

			
	}
	public void DisplayBullets()
	{
		bulletText.text = string.Format(gunData.currentAmo.ToString() + "/" + gunData.totalAmo.ToString());
	}
}
