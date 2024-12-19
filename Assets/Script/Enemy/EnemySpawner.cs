using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject[] enemyPrefabs;
	public int maxEnemies = 5;
	public float spawnRadius = 10f;
	public float spawnInterval = 2f;
	public float activaionDistance = 50f;

	private int currentEnemyCount = 0;
	private float lastSpawnTime = 0f;

	public void CheckAndActivate(Vector3 playerPosition)
	{
		float distanceToPlayer = Vector3.Distance(playerPosition, transform.position);

		if (distanceToPlayer <= activaionDistance)
		{
			if (Time.time -lastSpawnTime>=spawnInterval && currentEnemyCount<maxEnemies)
			{
				SpawnEnemy();
				lastSpawnTime = Time.time;	
			}
		}
	}

	void SpawnEnemy()
	{

		if (enemyPrefabs.Length==0)
		{
			Debug.LogWarning("EnemySpawner does not contain any prefab");
			return;

		}

		GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

		Vector3 spawnPosition= transform.position+Random.insideUnitSphere*spawnRadius;
		spawnPosition.y = transform.position.y;
		
		GameObject enemy=Instantiate(enemyPrefab, spawnPosition,Quaternion.identity);
		currentEnemyCount++;
		//When enemy die currentEnemyCount shoud decrease
		// enemy.GetComponent<EnemyScript>().Die();

	}
}
