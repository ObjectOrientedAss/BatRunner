using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    private InputHandler inputHandler;
    private Animator animator;

    [Header("DESIGN SECTION")]
    public float BatHorizontalSpeed; //the bat movement speed on X axis
    public float BatVerticalSpeed; //the bat movement speed on Y axis
    public float BatStartingSpeed; //the bat movement speed on Z axis

    public float BatCurrentSpeed;
    public float BatSpeedMultiplier;

    public float waveSpeedMultiplier;
    public float waveSpeedMin;
    public float waveSpeedMax;
    public float waveIntervalMultiplier;
    public float waveIntervalMin;
    public float waveIntervalMax;
    public float waveExponentMultiplier;
    public float waveExponentMin;
    public float waveExponentMax;

    public float XLimit;
    public float YLimit;

    private SonarFx sonar;

    public float Score;

    GameManager gm;
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        //take the references
        animator = GetComponent<Animator>();
        animator.SetBool("Alive", true);

        CurrentBatWingsSpeed = StartingBatWingsSpeed;
        animator.SetFloat("BatWingsSpeed", CurrentBatWingsSpeed);

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        inputHandler = GetComponent<InputHandler>();
        inputHandler.Init();

        BatCurrentSpeed = BatStartingSpeed;
        sonar = GetComponent<SonarFx>();
    }

    public void CollectBees(ref float score)
    {
        Score += score;
        gm.SetScoreText(ref Score);
    }

    public float ScoreDistanceMultiplier;
    private float scoreAccumulator;

    private void IncreaseScore()
    {
        scoreAccumulator += ScoreDistanceMultiplier + gm.CurrentPointsMultiplier;
        if(scoreAccumulator >= 1)
        {
            Score += scoreAccumulator;
            scoreAccumulator = 0;
            gm.SetScoreText(ref Score);
        }
    }

    public float BatWingsSpeedMultiplier;
    public float StartingBatWingsSpeed;
    public float CurrentBatWingsSpeed;
    public float MaxBatWingsSpeed;

    private void Update()
    {
        //Debug.Log(gm.CurrentPointsMultiplier);
        if (!gm.gameOver)
        {
            CurrentBatWingsSpeed += Time.deltaTime * BatWingsSpeedMultiplier;
            CurrentBatWingsSpeed = Mathf.Clamp(CurrentBatWingsSpeed, StartingBatWingsSpeed, MaxBatWingsSpeed);
            animator.SetFloat("BatWingsSpeed", CurrentBatWingsSpeed);

            //game loop
            Vector3 direction = new Vector3(inputHandler.movementHorizontal * BatHorizontalSpeed, inputHandler.movementVertical * BatVerticalSpeed, BatCurrentSpeed);
            transform.position += direction * Time.deltaTime;

            //----------
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -XLimit, XLimit), Mathf.Clamp(transform.position.y, -YLimit, YLimit), transform.position.z);

            BatCurrentSpeed += Time.deltaTime * BatSpeedMultiplier;
            //sonar.waveSpeed += Time.deltaTime * waveSpeedMultiplier;
            //sonar.waveSpeed = Mathf.Clamp(sonar.waveSpeed, waveSpeedMin, waveSpeedMax);
            sonar.waveSpeed += Time.deltaTime * waveSpeedMultiplier;

            sonar.waveInterval += Time.deltaTime * waveIntervalMultiplier;
            sonar.waveInterval = Mathf.Clamp(sonar.waveInterval, waveIntervalMin, waveIntervalMax);
            sonar.waveExponent -= Time.deltaTime * waveExponentMultiplier;
            sonar.waveExponent = Mathf.Clamp(sonar.waveExponent, waveExponentMin, waveExponentMax);

            IncreaseScore();
        }
    }

    private void Start()
    {
        gm.musicSource.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            //Debug.Log("DIED");
            animator.SetBool("Alive", false);
            animator.SetLayerWeight(1, 1f);
            ps.gameObject.SetActive(false);
            gm.GameOver();
        }
    }
}
