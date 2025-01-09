using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Detection : MonoBehaviour
{

    EnemyScript enemy;
    // Start is called before the first frame update
    void Start()
    {
       enemy = GetComponentInParent<EnemyScript>();
    }
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			
			enemy.SetState(EnemyStats.Attack);
			
		}
	}


	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			
			enemy.SetState( EnemyStats.Chase);
		}
	}
}

