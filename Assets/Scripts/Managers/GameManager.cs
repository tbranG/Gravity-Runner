using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public GameObject player_ref;
    public GameObject[] spawners;

    private int scene_index;
    public bool playerDead;

    [SerializeField] private float wait_time;
    private float add_timer;
    private int score;
    
    public int ScoreValue
    {
        get { return score; }
        set 
        {
            score = value;
           
            if(uiLoaded)
                UIManager.uiManager.UpdateScoreDisplay(score);
        }
    }
    public int highscore;


    [Header("UI")]
    public bool loadUi;
    public string ui_scene;
    
    private static bool uiLoaded;
    private static bool gamePaused;

    public float timeSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        SetupGame();

        gameManager = this;
       
        scene_index = SceneManager.GetActiveScene().buildIndex;
        player_ref = GameObject.FindGameObjectWithTag("Player");

        //apenas para teste
        Time.timeScale = timeSpeed;
    }

    private void SetupGame()
    {
        if (loadUi)
        {
            SceneManager.LoadScene(ui_scene, LoadSceneMode.Additive);
            uiLoaded = true;
        }

        score = 0;
        gamePaused = false;
        playerDead = false;
        add_timer = wait_time;
    }

    private void Start()
    {       
        spawners = GameObject.FindGameObjectsWithTag("Spawner");
        highscore = PlayerPrefs.GetInt("HighScore", 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(scene_index);
        }

        if (Input.GetKeyDown(KeyCode.P) && !playerDead)
        {
            SetPauseState(!gamePaused);
        }

        if (!playerDead) 
        {
            if (player_ref == null && !playerDead)
            {
                playerDead = true;
                foreach (GameObject obj in spawners)
                {
                    obj.GetComponent<ObstacleSpawner>().active = false;
                }

                if(score > highscore)
                {
                    highscore = score;
                    PlayerPrefs.SetInt("HighScore", highscore);
                }

                if (uiLoaded)
                {
                    UIManager.uiManager.SetupResultsPanel(ScoreValue, highscore);
                }     
            }
            
            if (!gamePaused)
            {
                add_timer = Mathf.Lerp(add_timer, 0f, 0.5f);       
                if (add_timer <= 0.1f)
                {
                    AddScore();
                    add_timer = wait_time;
                }
            }        
        }
    } 

    public void SetPauseState(bool paused)
    {
        gamePaused = paused;

        float time_scale = gamePaused == true ? 0f : 1f;
        Time.timeScale = time_scale;

        if (uiLoaded)
            UIManager.uiManager.SetPausePanel(paused);
    }

    public void AddScore()
    {
        ScoreValue++;
    }
}
