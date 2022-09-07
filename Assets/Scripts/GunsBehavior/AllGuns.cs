using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllGuns : MonoBehaviour
{
    public GameObject[] gunsPrefab;

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

}
