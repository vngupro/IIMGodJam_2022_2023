using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public int maxGun;
    public int currentSlot;
    public GunInfo[] guns;

    // Start is called before the first frame update
    void Start()
    {
        currentSlot = 0;
        guns = new GunInfo[maxGun];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(currentSlot >= maxGun - 1)
            {
                currentSlot = 0;
            }
            else if(currentSlot >= 0)
            {
                currentSlot++;
            }
        }
        if(guns[currentSlot] != null && Input.GetKeyDown(KeyCode.E))
        {
            Drop();
        }
    }

    public void Drop()
    {
        GameObject.Find("GunManager").GetComponent<AllGuns>().Spawn(guns[currentSlot], transform.position);
        guns[currentSlot] = null;
    }
}
