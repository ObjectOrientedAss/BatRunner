using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavernSectionBehaviour : MonoBehaviour
{
    public int ownerQueueIndex;
    public GameManager gm;
    public ObstacleSpawner ObstacleSpawner; //set in inspector

    public void Init(GameManager gm)
    {
        this.gm = gm;
        ObstacleSpawner.gm = gm;
    }

    public void OnSpawn()
    {
        ObstacleSpawner.SpawnObstacle(ref gm.MinObstaclesInSection, ref gm.MaxObstaclesInSection);

        float random = Random.Range(0, gm.ChanceToSpawnBees);
        if (random <= gm.ChanceToSpawnBees)
        {
            ObstacleSpawner.SpawnBees();
        }

        ObstacleSpawner.SpawnDecal();
    }

    private void Update()
    {
        if(transform.position.z < gm.Bat.transform.position.z)
        {
            float distance = gm.Bat.transform.position.z - transform.position.z;
            if(distance >= transform.localScale.z * 10f)
            {
                gm.poolsManager.DisposeCavernSection(this);
            }
        }
    }
}