using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;

    public CircleCollider2D playerCollider;

    public GameObject player;
    public static PlayerMovement instance;

    

  
    




    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("PlayerMovement");
            return;
        }
        instance = this;
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(movement.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -180);
        }
        else if(movement.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        else if (movement.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);

        }
        else if (movement.y < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);

        }

        


    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);



       
        
    }



    

}
