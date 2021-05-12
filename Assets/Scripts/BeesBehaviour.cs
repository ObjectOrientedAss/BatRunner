using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeesBehaviour : MonoBehaviour
{
    public GameManager gm;
    public float Score;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < gm.Bat.transform.position.z)
        {
            float distance = gm.Bat.transform.position.z - transform.position.z;
            if (distance >= transform.localScale.z * 5f)
            {
                gm.poolsManager.DisposeBees(this);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Bat>().CollectBees(ref Score);
            gm.poolsManager.DisposeBees(this);
            gm.sfxSource.clip = gm.audioClips[4];
            gm.sfxSource.Play();
            gm.CurrentPointsMultiplier += gm.PointsMultiplier;
            gm.CurrentPointsMultiplier = Mathf.Clamp((float)gm.CurrentPointsMultiplier, 0, (float)gm.MaxPointsMultiplier);
            gm.SetMultiplier();
        }
    }
}
