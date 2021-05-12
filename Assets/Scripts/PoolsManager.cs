using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolsManager : MonoBehaviour
{
    public GameObject[] CavernSectionPrefabs;
    public Queue<CavernSectionBehaviour>[] CavernSectionsQueues;
    public int cavernSectionsQueueLength;

    public GameObject[] ObstaclesPrefabs;
    public Queue<ObstacleBehaviour>[] ObstaclesQueues;
    public int obstaclesQueuesLength;

    public GameObject BeesPrefab;
    public Queue<BeesBehaviour> BeesQueue;
    public int beesQueueLength;

    public GameObject[] DecalPrefabs;
    public Queue<DecalBehaviour>[] DecalsQueues;
    public int decalsQueuesLength;

    public Vector3 lastCavernSectionPosition;

    public Material[] decalWebMaterials;
    public Material[] decalMossMaterials;

    public void Init(GameManager gm)
    {
        //get the needed references

        //cavern sections
        CavernSectionsQueues = new Queue<CavernSectionBehaviour>[CavernSectionPrefabs.Length];
        for (int i = 0; i < CavernSectionPrefabs.Length; i++)
        {
            Queue<CavernSectionBehaviour> queue = new Queue<CavernSectionBehaviour>();
            for (int j = 0; j < cavernSectionsQueueLength; j++)
            {
                GameObject cavernSection = Instantiate(CavernSectionPrefabs[i]);
                CavernSectionBehaviour script = cavernSection.GetComponent<CavernSectionBehaviour>();
                script.Init(gm);
                script.ownerQueueIndex = i;
                cavernSection.SetActive(false);
                queue.Enqueue(script);
            }
            CavernSectionsQueues[i] = queue;
        }

        //obstacles
        ObstaclesQueues = new Queue<ObstacleBehaviour>[ObstaclesPrefabs.Length];
        for (int i = 0; i < ObstaclesPrefabs.Length; i++)
        {
            Queue<ObstacleBehaviour> queue = new Queue<ObstacleBehaviour>();
            for (int j = 0; j < obstaclesQueuesLength; j++)
            {
                GameObject obstacle = Instantiate(ObstaclesPrefabs[i]);
                ObstacleBehaviour script = obstacle.GetComponent<ObstacleBehaviour>();
                script.gm = gm;
                script.ownerQueueIndex = i;
                obstacle.SetActive(false);
                queue.Enqueue(script);
            }
            ObstaclesQueues[i] = queue;
        }

        DecalsQueues = new Queue<DecalBehaviour>[DecalPrefabs.Length];
        for (int i = 0; i < DecalPrefabs.Length; i++)
        {
            Queue<DecalBehaviour> queue = new Queue<DecalBehaviour>();
            for (int j = 0; j < decalsQueuesLength; j++)
            {
                GameObject decal = Instantiate(DecalPrefabs[i]);
                DecalBehaviour script = decal.GetComponent<DecalBehaviour>();
                script.gm = gm;
                script.ownerQueueIndex = i;
                script.materials = i == 0 ? decalWebMaterials : decalMossMaterials; //così, me annava...
                decal.SetActive(false);
                queue.Enqueue(script);
            }
            DecalsQueues[i] = queue;
        }

        BeesQueue = new Queue<BeesBehaviour>();
        for (int i = 0; i < beesQueueLength; i++)
        {
            GameObject bees = Instantiate(BeesPrefab);
            BeesBehaviour script = bees.GetComponent<BeesBehaviour>();
            script.gm = gm;
            bees.SetActive(false);
            BeesQueue.Enqueue(script);
        }

        //create the first part of the cavern...
        for (int i = 0; i < 8; i++)
        {
            PlaceCavernSection();
        }
    }

    public void PlaceCavernSection()
    {
        CavernSectionBehaviour cavernSection = GetCavernSection();
        cavernSection.transform.position = lastCavernSectionPosition;
        cavernSection.OnSpawn();
        lastCavernSectionPosition += new Vector3(0, 0, cavernSection.transform.localScale.z * 10f); //10 is the real scale of the cavern model now
        cavernSection.gameObject.SetActive(true);
    }

    /// <summary>
    /// Dispose the surpassed cavern section and put a new one at the end of the line
    /// </summary>
    /// <param name="cavernSection"></param>
    public void DisposeCavernSection(CavernSectionBehaviour cavernSection)
    {
        cavernSection.gameObject.SetActive(false);
        CavernSectionsQueues[cavernSection.ownerQueueIndex].Enqueue(cavernSection);
        PlaceCavernSection();
    }

    public void DisposeBees(BeesBehaviour bees)
    {
        bees.gameObject.SetActive(false);
        BeesQueue.Enqueue(bees);
    }

    public BeesBehaviour GetBees()
    {
        return BeesQueue.Dequeue();
    }

    private CavernSectionBehaviour GetCavernSection()
    {
        return CavernSectionsQueues[Random.Range(0, CavernSectionsQueues.Length)].Dequeue();
    }

    public void DisposeObstacle(ObstacleBehaviour obstacle)
    {
        obstacle.gameObject.SetActive(false);
        ObstaclesQueues[obstacle.ownerQueueIndex].Enqueue(obstacle);
    }

    public ObstacleBehaviour GetObstacle()
    {
        return ObstaclesQueues[Random.Range(0, ObstaclesQueues.Length)].Dequeue();
    }

    public DecalBehaviour GetDecal()
    {
        return DecalsQueues[Random.Range(0, DecalsQueues.Length)].Dequeue();
    }

    public void DisposeDecal(DecalBehaviour decal)
    {
        decal.gameObject.SetActive(false);
        DecalsQueues[decal.ownerQueueIndex].Enqueue(decal);
    }
}
