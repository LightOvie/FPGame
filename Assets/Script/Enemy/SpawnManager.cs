using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public List<EnemySpawner> spawners=new List<EnemySpawner>();
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        spawners.AddRange(FindObjectsOfType<EnemySpawner>());

        if (player==null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var spawner in spawners)
        {
            spawner.CheckAndActivate(player.position);
        }
    }
}
