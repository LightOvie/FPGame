using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IDamageable
{
    [SerializeField]
    float _maxHealth;
	public float Health { get ; set; }


    //public static Action<float> OnHealthChange;

	// Start is called before the first frame update
	void Start()
    {
		Health = _maxHealth;
		//OnHealthChange(Health);
    }

    // Update is called once per frame
  
	public void TakeDamage(float damage)
	{
		Health -= damage;
		//OnHealthChange?.Invoke(Health);

	}

}
