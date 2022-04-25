using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject gamePause_pnl;
    public GameObject gameOver_pnl;

    public TMP_Text countdownTxt;

    public Slider distanceCoveredSlider;
    public float roundDuration = 60f;

    public static bool gameOver = false;
    public static bool gamePause = true;

    public GameObject waterPlane;
    public GameObject loadingScreen;
    public Slider loadingSldr;

    public ScoreManager scoreManager;
    public ObstacleSpawner obstacleSpawner;
    public CollectiblesSpawner collectibleSpawner;

    private void Start()
    {
        gameOver = false;      
    }

    public void StartGame()
    {
        StartCoroutine(Countdown());
    }

    public IEnumerator Countdown()
    {
        if (countdownTxt == null) yield break;

        int countdownTime = 5;
        int time = countdownTime;
        
        while (time >= 0)
        {
            countdownTxt.text = time.ToString();
            yield return new WaitForSeconds(1f);
            time--;
        }

        countdownTxt.gameObject.SetActive(false);
        
        gamePause = false;
        
        collectibleSpawner.gameObject.SetActive(true);
        obstacleSpawner.gameObject.SetActive(true);
    }

    float timer;
    
    void Update()
    {
        if (gamePause) return;
        
        UpdateDistanceBar();
    }

    private void UpdateDistanceBar()
    {
        timer += Time.deltaTime;

        if (distanceCoveredSlider != null)
            distanceCoveredSlider.value = timer;

        if (!gameOver && roundDuration <= timer)
            OnGameOver();
    }

    public void OnGameOver()
    {
        AudioManager.Instance.PlayClip(2);
        gameOver = true;

        gameOver_pnl.SetActive(true);
        scoreManager.ShowFinalScore();
    }

    #region ----Button----

    public void StartRound()
    {
        SceneManager.LoadScene(0);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    #endregion

    #region----Scene Operations----
    List<AsyncOperation> asyncOperations = new();

    public void LoadALevel(int levelIndex)
    {
        loadingScreen.SetActive(true);

        asyncOperations.Add(SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene()));
        asyncOperations.Add(SceneManager.LoadSceneAsync(1));

        //StartCoroutine(GetSceneLoadingProgress());
    }

 /*   IEnumerator GetSceneLoadingProgress()
    {
        for(int i = 0; i < asyncOperations.Count; i++)
        {
            while(!asyncOperations[i].isDone)
            {
                float totalProgress = 0;

                foreach(AsyncOperation operation in asyncOperations)
                {
                    totalProgress += operation.progress;
                }

                loadingSldr.value = Mathf.RoundToInt((totalProgress/asyncOperations.Count )*100);
                yield return null;
            }
        }

        loadingScreen.SetActive(false);
    }*/
    #endregion
}
