using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreVie : MonoBehaviour
{
    [SerializeField] private Gradient gradient;
    private float pourcent;
    public PlayerHealth Player;
    

    void Update()
    {
        pourcent =  (float)Player.currentHealth / 100.0f;
        SpriteRenderer _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = gradient.Evaluate(pourcent);
        if (pourcent > 1)
        {
            pourcent = 1;
        }
        transform.localScale = new Vector3(1 - pourcent, transform.localScale.y, transform.localScale.z);
    }
}
