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
			Gun gun = other.GetComponent<Gun>();
			if (gun != null) {

				
				gun.AddAmmo(ammoAmount);
			}

			Destroy(gameObject);	

		}
	}

}
