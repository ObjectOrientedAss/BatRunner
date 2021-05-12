using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuBat : MonoBehaviour
{
    Vector3 startingPos;
    public GameObject CreditsPanel; //set in inspector

    private void Awake()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, startingPos) > 25)
        {
            transform.position = Vector3.Lerp(transform.position, startingPos, 1f * Time.deltaTime);
        }
        else
        {
            Vector3 dir = Random.insideUnitSphere;
            transform.position = Vector3.Lerp(transform.position, transform.position + (dir * 10f), 1f * Time.deltaTime);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Credits()
    {
        CreditsPanel.SetActive(true);
    }

    public void MainMenu()
    {
        CreditsPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Quitted");
    }
}
