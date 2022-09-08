using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
    private int maxGun = 4;
    public int currentSlot = 0;

    public GameObject[] guns;

    public Image[] Items;
    public Image[] SlotBack;

    private void Awake()
    {
        guns = new GameObject[4];
        foreach (Image item in Items)
        {
            item.color = Color.clear;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(guns[currentSlot] != null)
        {
            Debug.Log(guns[currentSlot].name);
            Debug.Log(guns[currentSlot].GetComponent<GunInfo>().BulletsLeft);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeSlot();
        }
        HiglightSlot();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (guns[currentSlot] != null)
            {
                DropGun();
            }
        }
    }

    public void AddGun(GameObject gun)
    {
        if(Input.GetKey(KeyCode.E))
        {
            gun.GetComponent<GunBehavior>().over = false;
            if (guns[currentSlot] != null)
            {
                ChangeGun(gun);
            }
            else
            {
                guns[currentSlot] = gun;
                Items[currentSlot].sprite = gun.GetComponent<SpriteRenderer>().sprite;
                Items[currentSlot].color = gun.GetComponent<SpriteRenderer>().color;
                gun.SetActive(false);
            }
        }
    }



    public void ChangeGun(GameObject gun)
    {
        DropGun();
        guns[currentSlot] = gun;
        Items[currentSlot].sprite = gun.GetComponent<SpriteRenderer>().sprite;
        Items[currentSlot].color = gun.GetComponent<SpriteRenderer>().color;
        gun.SetActive(false);
    }

    public void DropGun()
    {
        guns[currentSlot].transform.position = GameObject.Find("Player").transform.position;
        guns[currentSlot].SetActive(true);
        Items[currentSlot].sprite = null;
        Items[currentSlot].color = Color.clear;
        StartCoroutine(guns[currentSlot].GetComponent<GunBehavior>().Wait());
        guns[currentSlot] = null;
    }

    private void ChangeSlot()
    {
        if(currentSlot + 2 > maxGun)
        {
            currentSlot = 0;
        }
        else
        {
            currentSlot++;
        }
    }

    private void HiglightSlot()
    {
        for (int i = 0; i < maxGun; i++)
        {
            if(i == currentSlot)
            {
                SlotBack[i].color = new Color(255f, 255f, 0f, 0.3f);
            }
            else
            {
                SlotBack[i].color = new Color(0f, 0f, 0f, 0.3f);
            }
        }
    }
}
