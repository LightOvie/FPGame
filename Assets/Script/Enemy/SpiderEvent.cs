using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpiderEvent : MonoBehaviour
{
	[Header("Spider Attack")]
	float attackDamage = 10f;
	float attackRange = 2.5f;

	int playerMask ;

	private void Awake()
	{
		playerMask = LayerMask.GetMask("Player");
	}
	//Look better why it's doesnt work
	public void SpiderAttack()
	{
		Collider[] hitPlayer = Physics.OverlapSphere(transform.position, attackRange, playerMask);

		foreach (Collider player in hitPlayer)
		{
			if (player.gameObject==this.gameObject)
			{
				continue;
			}
			IDamageable playerDamageable = player.GetComponent<IDamageable>();
			if (playerDamageable!= null)
			{
				playerDamageable?.TakeDamage(attackDamage);
				
			}
		}
	}


	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}
}
