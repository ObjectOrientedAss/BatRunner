using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform Bat;
    public GameManager GameManager; //set in inspector
    public float ZDistanceToKeep;
    public float YDistanceToKeep;
    public float LerpSpeed;

    // Update is called once per frame
    void LateUpdate()
    {
        if (!GameManager.gameOver)
        {

            transform.position = new Vector3(transform.position.x, transform.position.y, Bat.transform.position.z - ZDistanceToKeep);
            //transform.position = Vector3.Lerp(transform.position, new Vector3(Bat.transform.position.x, Bat.transform.position.y + YDistanceToKeep, Bat.transform.position.z - ZDistanceToKeep), LerpSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, new Vector3(Bat.transform.position.x, Bat.transform.position.y + YDistanceToKeep, transform.position.z), LerpSpeed * Time.deltaTime);
        }
    }
}
