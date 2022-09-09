using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : MonoBehaviour
{
    public bool over = true;
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        over = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && over)
        {
            GameObject.Find("InventoryManager").GetComponent<InventoryManager>().AddGun(gameObject);
        }
    }
}
