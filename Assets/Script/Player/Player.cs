using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IDamageable
{
    [SerializeField]
    float _maxHealth;
	public float Health { get ; set; }
	
	void Start()
    {
		Health = _maxHealth;
		
    }
  
	public void TakeDamage(float damage)
	{
		Health -= damage;
		if (Health <= 0)
		{
			Die();
		}

		Debug.Log(Health.ToString());

	}
	void Die()
	{

		if (!GameManager.instance.isGameOver)
		{
			GameManager.instance.GameOver();
		}
		
	}
}
