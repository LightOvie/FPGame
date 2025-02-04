using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDrop : MonoBehaviour
{
	int ammoAmount = 10;
	
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			
			Debug.Log("Player Inside");
			Gun gun = other.GetComponentInChildren<Gun>();
			if (gun != null) {

				
				Debug.Log("Work Ammo Added");
				gun.AddAmmo(ammoAmount);
			}

			Destroy(gameObject);	
				
		}
	}

}
