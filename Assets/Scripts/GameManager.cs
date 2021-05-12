using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioManager audioManager { get; private set; }
    public PoolsManager poolsManager { get; private set; }
    public Bat Bat;

    public int MinObstaclesInSection;
    public int MaxObstaclesInSection;

    public float StartingChanceToSpawnBees; //set in inspector
    public float ChanceToSpawnBees;

    public TextMeshProUGUI YouDiedText; //set in inspector
    public TextMeshProUGUI FinalScoreText; //set in inspector
    public TextMeshProUGUI InGameText; //set in inspector
    public TextMeshProUGUI MultiplierText; //set in inspector

    public GameObject PlayAgainButton;
    public GameObject ExitButton;

    public int MaxDecalsPerSection; //set in inspector
    public bool gameOver { get; private set; }

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip[] audioClips;

    public float MaxPointsMultiplier;
    public float CurrentPointsMultiplier;
    public float PointsMultiplier; //set by designer in inspector

    // Start is called before the first frame update
    void Awake()
    {
        PlayAgainButton.SetActive(false);
        ExitButton.SetActive(false);
        audioManager = GetComponent<AudioManager>();
        ChanceToSpawnBees = StartingChanceToSpawnBees;
        YouDiedText.gameObject.SetActive(false);
        FinalScoreText.gameObject.SetActive(false);
        poolsManager = GetComponent<PoolsManager>();
        poolsManager.Init(this);
    }

    public void SetScoreText(ref float score)
    {
        string text = score.ToString("0");
        InGameText.text = text;
    }

    public void SetMultiplier()
    {
        Debug.Log(CurrentPointsMultiplier);
        if (CurrentPointsMultiplier <= 0)
        {
            string text = "";
            MultiplierText.text = text;
        }
        else
        {
            string text = CurrentPointsMultiplier.ToString("0.0");
            MultiplierText.text = "x" + text;
        }
    }

    public void GameOver()
    {
        PlayAgainButton.SetActive(true);
        ExitButton.SetActive(true);
        gameOver = true;
        FinalScoreText.text = InGameText.text;
        YouDiedText.gameObject.SetActive(true);
        FinalScoreText.gameObject.SetActive(true);
        //play game over sound
        //--->
        sfxSource.clip = audioClips[1];
        sfxSource.Play();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}