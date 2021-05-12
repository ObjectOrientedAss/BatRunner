using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalBehaviour : MonoBehaviour
{
    [HideInInspector] public int ownerQueueIndex;
    [HideInInspector] public GameManager gm;
    [HideInInspector] public Material[] materials;
    public MeshRenderer meshRenderer; //set in inspector

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < gm.Bat.transform.position.z)
        {
            float distance = gm.Bat.transform.position.z - transform.position.z;
            if (distance >= 10f)
            {
                gm.poolsManager.DisposeDecal(this);
            }
        }
    }

    public void SetMaterial()
    {
        meshRenderer.material = materials[Random.Range(0, materials.Length)];
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with decal!");
            gm.poolsManager.DisposeDecal(this);
            //other.gameObject.GetComponent<Bat>().CollectBees(ref Score);
            //gm.poolsManager.DisposeBees(this);
            int random = Random.Range(2, 4);
            gm.sfxSource.clip = gm.audioClips[random];
            gm.sfxSource.Play();
            gm.CurrentPointsMultiplier = 0;
            gm.SetMultiplier();
        }
    }
}
