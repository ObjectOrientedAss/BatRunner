using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    string tagToCompare = "CavernSection";
    public GameManager gm;

    public void SpawnObstacle(ref int minObstacles, ref int maxObstacles)
    {
        int obstaclesToSpawn = Random.Range(minObstacles, maxObstacles);
        Vector3 position = transform.position;
        Vector3 direction = Vector3.zero;
        float distance = 20f;

        for (int i = 0; i < obstaclesToSpawn; i++)
        {
            direction = new Vector3(Random.insideUnitSphere.x, Random.insideUnitSphere.y, Random.insideUnitSphere.z);
            direction.z = Mathf.Clamp(direction.z, -2, 2);
            if (RayCastHelper.RayCast(ref position, ref direction, ref distance, ref tagToCompare) == RayCastResult.TagHit)
            {
                ObstacleBehaviour obstacle = gm.poolsManager.GetObstacle();
                obstacle.transform.position = RayCastHelper.LastHitInfos.point;
                obstacle.transform.up = RayCastHelper.LastHitInfos.normal;
                obstacle.gameObject.SetActive(true);
            }
        }
    }

    public void SpawnBees()
    {
        Vector3 position = transform.position;
        Vector3 direction = new Vector3(Random.insideUnitSphere.x, Random.insideUnitSphere.y, Random.insideUnitSphere.z);
        float distance = 20f;

        if (RayCastHelper.RayCast(ref position, ref direction, ref distance, ref tagToCompare) == RayCastResult.TagHit)
        {
            BeesBehaviour bees = gm.poolsManager.GetBees();
            bees.transform.position = RayCastHelper.LastHitInfos.point + (RayCastHelper.LastHitInfos.normal * 1.2f);
            bees.gameObject.SetActive(true);
        }
    }

    public void SpawnDecal()
    {
        int decalsToPlace = Random.Range(0, gm.MaxDecalsPerSection);
        for (int i = 0; i < decalsToPlace; i++)
        {
            Vector3 pos = transform.position;
            Vector3 dir = new Vector3(Random.insideUnitCircle.x, 1, Random.insideUnitCircle.y);
            float distance = 20f;

            if (RayCastHelper.RayCast(ref pos, ref dir, ref distance, ref tagToCompare) == RayCastResult.TagHit)
            {
                DecalBehaviour decal = gm.poolsManager.GetDecal();
                decal.transform.position = RayCastHelper.LastHitInfos.point + ((transform.position - RayCastHelper.LastHitInfos.point) * 0.5f);
                decal.SetMaterial();
                decal.gameObject.SetActive(true);
            }
        }
    }
}
