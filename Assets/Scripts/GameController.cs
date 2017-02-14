using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public float leftWidth;
    public float rightWidth;
    public float height;
    public float awayDepth;

    //The fruits we'll drop
    public GameObject[] fruits;

    //Update ScoreBoard/timer 
    public GameObject ScoreBoardText;
    public GameObject TimerText;

    //Score/Timer
    private int score;
    private float timer; 
    //Reference to Text UI control where the score should be displayed
    private TextMesh scoreMesh;
    private TextMesh timerMesh;

    //bomb sound
    private AudioSource bombSound;

    //List of spawn fruits
    List<GameObject> spawnedFruits; 

    // Use this for initialization
    void Start () {
        //Start score at 0 and display
        score = 0;
        scoreMesh = ScoreBoardText.GetComponent<TextMesh>();
        UpdateTextDisplay();

        timer = 120f;
        timerMesh = TimerText.GetComponent<TextMesh>();

        spawnedFruits = new List<GameObject>();

        //Get bomb sound
        bombSound = gameObject.GetComponent<AudioSource>();

        //Values chosen to spawn near view frame. Feel free to play around with them.
        var cam = Camera.main;
        leftWidth = cam.transform.position.x - 1;
        rightWidth = cam.transform.position.x + 1;
        height = cam.transform.position.y + 1.5f;
        awayDepth = cam.transform.position.z + 2;
        StartCoroutine(SpawnWaves());

    }

    void FixedUpdate()
    {
        //Update Timer and Screen Text
        timer -= Time.deltaTime;
        timerMesh.text = "Time: " + Mathf.Round(timer);

        if (timer <= 0)
        {
            OnMenu();
        }
    }

    //Adds Score when tapping fruit
    public void AddScore()
    {
        score+=100;
        UpdateTextDisplay();
    }

    //Puts text on screen
    private void UpdateTextDisplay()
    {
        string displayString = "Score: " + score.ToString();

        if (scoreMesh != null)
        {
            scoreMesh.text = displayString;
        }
    }

    //Voice command for reseting level
    void OnRestart()
    {
        SceneManager.LoadScene("Main");
    }
    
    //Voice command for loading menu
    void OnMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    //Called by bomb to clear field 
    public void BombClear()
    {
        bombSound.Play();

        while (spawnedFruits.Count > 0)
        {
            Destroy(spawnedFruits[0]);
            spawnedFruits.RemoveAt(0);
        }

        //Take away time and score
        timer -= 30;
        score -= 500;
        UpdateTextDisplay();
    }

    //Spawn fruits
    IEnumerator SpawnWaves()
    {
        var cam = Camera.main;
        while (true)
        {
            //Spawn the fruit within the player's view
            Vector3 spawnPosition = new Vector3(Random.Range(leftWidth, rightWidth), height, Random.Range(awayDepth, awayDepth+ 2));

            //Instatiate random fruit
            spawnedFruits.Add(Instantiate(fruits[Random.Range(0,6)], spawnPosition, Random.rotation));
            
            //Wait before spawning another
            yield return new WaitForSeconds(3f);
        }
    }
}
