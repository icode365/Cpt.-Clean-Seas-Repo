using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int score = 0;

    public TMP_Text finalScoreText;

    public int bottleScore = 1;
    public int cardboardScore = 5;
    public int jarScore = 15;
    public int pizzaboxScore = 20;
    public ParticleSystem partSys;
    public GameObject scoreCoinsPrefab;

    public TMP_Text scoreText;

    private void Awake()
    {
        if(scoreText == null)
        {
            print("~ERROR~ SCORE TEXT from " + this.gameObject + "is NOT ASSIGNED!");
        }
    }

    private void Start()
    {
        PoolManager.Instance.InstatiatePoolPrefabs(scoreCoinsPrefab, 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.gameOver) return;

        Collectibles collectible = other.GetComponent<Collectibles>();

        if (collectible == null || other.CompareTag("Obstacle")) return;

        if (partSys != null) partSys.Play();

        if (AudioManager.Instance != null) AudioManager.Instance.PlayClip(1);

        AddScore(collectible);

        /*
        if (collectible.type == SpawnableTypes.Bottle)
            scoreIncrement = bottleScore;

        else if (collectible.type == SpawnableTypes.Cardboardbox)
            scoreIncrement = cardboardScore;

        else if (collectible.type == SpawnableTypes.Jar)
            scoreIncrement = jarScore;

        else if (collectible.type == SpawnableTypes.Pizzabox)
            scoreIncrement = pizzaboxScore;*/



        UpdateScore();
    }

    private void AddScore(Collectibles collectible)
    {
        int scoreIncrement = 0;

        switch (collectible.type)
        {
            case SpawnableTypes.Bottle:
                scoreIncrement = bottleScore;
                break;

            case SpawnableTypes.Cardboardbox:
                scoreIncrement = cardboardScore;
                break;

            case SpawnableTypes.Jar:
                scoreIncrement = jarScore;
                break;

            case SpawnableTypes.Pizzabox:
                scoreIncrement = pizzaboxScore;
                break;
        }

        score += scoreIncrement;
    }

    void UpdateScore() => 
        scoreText.text = score.ToString("000");
    
    public void ShowFinalScore()
    {
        StartCoroutine(ShowFinalScoreUI());
    }

    public IEnumerator ShowFinalScoreUI()
    {
        int tmp_score = 0;

        while (tmp_score != score+1)
        {
            yield return new WaitForSeconds(0.1f/score);
            finalScoreText.text = tmp_score.ToString();
            tmp_score++;
        }
    }
}
