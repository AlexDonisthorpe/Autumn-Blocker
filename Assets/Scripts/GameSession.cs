using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    // Configuration Parameters
    [Range(0.1f, 10f)][SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 50;
    [SerializeField] int pointsDeductedPerBallLost = 100;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool isAutoPlayEnabled;

    // State Variables
    [SerializeField] int currentScore;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if(gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            ResetGame();
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;        
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = currentScore.ToString();
    }
    
    public void DeductScore()
    {
        currentScore = currentScore - pointsDeductedPerBallLost;
        UpdateScoreText();
        if(currentScore <= 0)
        {
            FindObjectOfType<SceneLoader>().LoadGameOverScene();
        }
    }
    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }


}
