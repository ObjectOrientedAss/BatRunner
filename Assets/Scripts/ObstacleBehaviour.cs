using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    public int ownerQueueIndex;
    public GameManager gm;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < gm.Bat.transform.position.z)
        {
            float distance = gm.Bat.transform.position.z - transform.position.z;
            if (distance >= transform.localScale.z * 2)
            {
                gm.poolsManager.DisposeObstacle(this);
            }
        }
    }
}
