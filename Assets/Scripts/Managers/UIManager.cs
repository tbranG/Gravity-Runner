using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManager;
    public Text scoreDisplay;
    public GameObject pausePanel;
    
    [Header("EndGame Panel")]
    public GameObject resultPanel;
    public Text score_value;
    public Text highscore_value;

    [Header("DashBar")]
    public Slider dash_slider;
    public float bar_value;

    [Header("Cursor")]
    public Texture2D cursorPoint;
    public Texture2D cursorClick;

    private GameObject player_ref;
   

    private void Awake()
    {
        uiManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        resultPanel.SetActive(false);

        Cursor.SetCursor(cursorPoint, Vector2.zero, CursorMode.Auto);
        Cursor.visible = false;

        player_ref = GameObject.FindGameObjectWithTag("Player");
        bar_value = player_ref.GetComponent<PlayerMovement>().dash_cooldown;
    }

    private void Update()
    {
        if (dash_slider.value < 2.5f)
        {        
            dash_slider.value += Time.deltaTime / 4f;
        }
    }

    public void UpdateScoreDisplay(int value)
    {
        scoreDisplay.text = "" + value;
    }

    public void SetPausePanel(bool active)
    {
        pausePanel.SetActive(active);
        Cursor.visible = active;
    }

    public void SetupResultsPanel(int score, int highscore)
    {
        resultPanel.SetActive(true);
        Cursor.visible = true;

        score_value.text = "" + score;
        highscore_value.text = "" + highscore;
    }
}
