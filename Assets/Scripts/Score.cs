using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof (TextMeshProUGUI))]

public class Score : MonoBehaviour
{
    private int score = 0;
    private TextMeshProUGUI text;
    
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

    public int GetScore()
    {
        return score;
    }
}
