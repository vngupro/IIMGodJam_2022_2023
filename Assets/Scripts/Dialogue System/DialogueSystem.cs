using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public string[] sentences;
    public float[] waitTime;
    public bool[] isOnClick;
    private bool isFinished = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !isFinished)
        {
            
        }
    }


}
