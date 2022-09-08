using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ulimate : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int scoreMinUlt;
    [SerializeField] private int scoreLost;
    [SerializeField] private float croissanceDifficulty;
    private bool ultimateMustache;

    private void Update()
    {
        score = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<Score>().score;

        if(score >= scoreMinUlt + scoreLost)
        {
            ultimateMustache = true;
        }
        if (ultimateMustache)
        {
            ultimateMustache = false;
            scoreLost += score;
            score = 0;
            scoreMinUlt += (int)(scoreMinUlt /croissanceDifficulty);
        }
    }
}
