using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IDamageable
{
    [SerializeField]
    float _maxHealth;
	public float Health { get ; set; }
	[SerializeField] private AudioClip[] hurtSounds;

	void Start()
    {
		Health = _maxHealth;
		GameManager.instance.hpDisplay.text = "Health: " +_maxHealth.ToString();

	}
  
	public void TakeDamage(float damage)
	{
		Health -= damage;
		GameManager.instance.hpDisplay.text="Health: "+Health.ToString();
		SoundFXManager.instance.PlayRandomSoundFXClip(hurtSounds, transform, 0.5f);
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
