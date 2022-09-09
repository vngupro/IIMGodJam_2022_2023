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

    [SerializeField] private ParticleSystem ultParticles;
    [SerializeField] private float buffTime;
    public bool underUlt;
    private float timeUltLeft;

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
            scoreMinUlt += (int)(scoreMinUlt * croissanceDifficulty/100);
            Instantiate(ultParticles, transform.position, Quaternion.identity);
            underUlt = true;
            timeUltLeft = buffTime;
            
        }
        if (timeUltLeft > 0)
        {
            timeUltLeft -= Time.deltaTime;
        }
        else
        {
            underUlt = false;
            
        }
    }
}
