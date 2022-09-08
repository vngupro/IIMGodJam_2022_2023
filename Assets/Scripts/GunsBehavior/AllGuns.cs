using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AllGuns : MonoBehaviour
{
    public GameObject[] gunsPrefab;
    public Image[] inventorySlotsBackground;
    public Image[] inventorySlotsItems;


    int max;
    int current;

    private void Start()
    {
        max = GameObject.Find("Player").GetComponent<PlayerGun>().maxGun;
    }
    public void Spawn(GunInfo gun, Vector2 pos)
    {
        foreach (GameObject item in gunsPrefab)
        {
            if(item.GetComponent<GunBehavior>().gunData == gun)
            {
                item.transform.position = pos;
                Instantiate(item);
            }
        }
    }

    private void Update()
    {
        current = GameObject.Find("Player").GetComponent<PlayerGun>().currentSlot;

        for (int i = 0; i < max; i++)
        {
            if (i == current)
            {
                inventorySlotsBackground[i].color = new Color(255f, 255f, 0f, 0.3f);
            }
            else
            {
                inventorySlotsBackground[i].color = new Color(0f, 0f, 0f, 0.3f);
            }
        }
    }

    public void NewItem(GunInfo gunInfo,int index)
    {
        foreach (GameObject item in gunsPrefab)
        {
            if(item.GetComponent<GunBehavior>().gunData == gunInfo)
            {
                inventorySlotsItems[index].sprite = item.GetComponent<SpriteRenderer>().sprite;
                Color newColor = inventorySlotsItems[index].color;
                newColor = new Color(newColor.r, newColor.g, newColor.b, 1f);
                inventorySlotsItems[index].color = newColor;
            }
            else
            {
                Color newColor = inventorySlotsItems[index].color;
                newColor = new Color(newColor.r, newColor.g, newColor.b, 0f);
                inventorySlotsItems[index].color = newColor;
            }
        }
    }

}
