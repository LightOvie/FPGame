using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{


    [SerializeField]
    private GameObject spiderPrefab;
    [SerializeField]
    private GameObject bigSpiderPrefab;

    private float spriderInterval = 3.5f;
    private float bigSpiderInterval = 10f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy(spriderInterval, spiderPrefab));
        StartCoroutine(SpawnEnemy(bigSpiderInterval, bigSpiderPrefab));
    }




    private IEnumerator SpawnEnemy(float interval, GameObject enemyPrefab)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemyPrefab, new Vector3(Random.Range(-3f, 3), Random.Range(-3f, 3), 0), Quaternion.identity);
        StartCoroutine(SpawnEnemy(interval, enemyPrefab));
    }
}
