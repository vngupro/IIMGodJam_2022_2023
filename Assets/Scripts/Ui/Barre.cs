using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barre : MonoBehaviour
{
    [SerializeField] private Gradient gradient;
    private float pourcent;


    void Update()
    {
        pourcent = (float)GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().score / (float)GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().scoreMinUlt;
        SpriteRenderer _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = gradient.Evaluate(pourcent);
        if(pourcent > 1)
        {
            pourcent = 1;
        }
        transform.localScale = new Vector3(transform.localScale.x, pourcent, transform.localScale.z);
    }
}
