using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonalRotation : MonoBehaviour
{
	[HideInInspector]
	public float sensetivity = 100f;
	[Header("Player is a body and camera as a head")]
	[SerializeField]
	Transform playerBody;
	[SerializeField]
	Transform playerHead;
	[SerializeField]
	Transform crossHair;


	float mouseX;
	float mouseY;
	float angle;

	[Header("Follow objects")]
	[SerializeField] Transform weapon;
	[SerializeField] Transform flashLight;

	[HideInInspector] public WeaponSway weaponSway;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		weaponSway = weapon.GetComponent<WeaponSway>();
		flashLight.position =transform.position;
	}


	void Update()
	{

		Rotation();
		UpdateFlashlightPosition();

	}

	private void UpdateFlashlightPosition()
	{
		if (flashLight != null)
		{
			
			flashLight.position = playerHead.position;

			flashLight.rotation = Quaternion.Euler(0f, playerHead.eulerAngles.y, 0f);
		}
	}
	public void Rotation()
	{

		mouseX = Input.GetAxis("Mouse X") * sensetivity * Time.deltaTime;
		mouseY = Input.GetAxis("Mouse Y") * sensetivity * Time.deltaTime;
		playerBody.Rotate(Vector3.up, mouseX);

		angle -= mouseY;
		angle = Mathf.Clamp(angle, -10, 20f);
		playerHead.localRotation = Quaternion.Euler(angle, 0f, 0f);
		weapon.localRotation = Quaternion.Euler(angle, 0f, 0f);
		flashLight.localRotation = Quaternion.Euler(angle,0f,0f);
		weaponSway.UpdateSway(mouseX, mouseY);


		
		
	}

	


	
}
