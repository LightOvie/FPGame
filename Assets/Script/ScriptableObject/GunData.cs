using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/New Gun")]
public class GunData : ScriptableObject
{
	[Header("Name")]
	public string weaponName;

	[Header("Shooting Info")]
	public float damage;
	public float maxDistance;

	[Header("Reloading Info")]
	public float reloadTime;
	public float fireRate;

	public int currentAmo;
	public int magSize;
	public float spread;
	public float shootForce;
	public int totalAmo;
	public  int startMagSize = 90;
	[HideInInspector]
	public bool reloading;





}
