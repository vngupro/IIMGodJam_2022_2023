using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof (TextMeshProUGUI))]

public class Score : MonoBehaviour
{
    public int score = 0;
    private TextMeshProUGUI text;

    private void Awake()
    {
        GameEvents.OnAddScore.AddListener(AddScore2);
    }
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        text = GetComponent<TextMeshProUGUI>();
        text.text = score.ToString();
    }
    public void AddScore(int points,int pointsMultiplier)
    {
        score += points * pointsMultiplier;
        text.text = score.ToString();
    }

    public void AddScore2(ScoreEventData data)
    {
        score += data.baseScore * data.scoreMultiplier;
        text.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }
}
