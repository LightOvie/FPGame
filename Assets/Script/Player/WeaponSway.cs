using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{


	float swayAmount = 0.05f;
	float smoothAmount = 6f;
	private Vector3 initialPosition;

	private void Start()
	{
		initialPosition = transform.localPosition;
	}



	public void UpdateSway(float mouseX, float mouseY)
	{
		float moveX = -mouseX * swayAmount;
		float moveY = -mouseY * swayAmount;

		Vector3 finalPosition = new Vector3(moveX, moveY, 0) + initialPosition;
		transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition, Time.deltaTime * smoothAmount);
	}


}
