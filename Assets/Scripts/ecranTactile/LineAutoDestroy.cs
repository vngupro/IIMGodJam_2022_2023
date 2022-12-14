using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAutoDestroy : MonoBehaviour
{
    [SerializeField] private float delayBeforeDeath = 1f;
    public bool readyToDie;
    void Update()
    {
        if (readyToDie)
        {
            Destroy(gameObject, delayBeforeDeath);
        }
    }
}
