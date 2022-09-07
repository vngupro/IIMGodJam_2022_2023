using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]

public class DialogueSystem : MonoBehaviour
{
    public string[] sentences;
    private TextMeshPro text;
    private bool Trigger = false;

    private void Start()
    {
        text = GetComponent <TextMeshPro>();
    }

    private bool DoesPlayerSkip()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void Update()
    {
        if(Trigger)
        {
            if(Input.GetKeyDown(KeyCode.M))
            {
                StartCoroutine(Talking());
                Trigger = false;
            }
        }
    }

    IEnumerator Talking()
    {
        foreach (string item in sentences)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.M));
            text.text = item;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Trigger = true;
        }
    }
}
