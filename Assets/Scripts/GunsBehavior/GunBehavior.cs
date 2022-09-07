using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : MonoBehaviour
{
    public GunInfo gunData;
    public float pickCoolDown;
    [HideInInspector] public bool over;
    private void Update()
    {
        if(!over)
        {
            Invoke("Wait", pickCoolDown);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && over)
        {
            PlayerGun playerGun = collision.GetComponent<PlayerGun>();
            if (Input.GetKey(KeyCode.Space))
            {
                if (playerGun.guns[playerGun.currentSlot] == null)
                {
                    playerGun.guns[playerGun.currentSlot] = gunData;
                    Destroy(gameObject);
                }
                else
                {
                    playerGun.Drop();
                    playerGun.guns[playerGun.currentSlot] = gunData;
                    Destroy(gameObject);
                }
            }
            
        }
    }

    void Wait()
    {
        over = true;
    }
}
