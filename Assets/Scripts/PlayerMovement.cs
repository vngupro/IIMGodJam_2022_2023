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
    private Animator Anim;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("PlayerMovement");
            return;
        }
        instance = this;
    }
    private void Start()
    {
        Anim = GetComponent<Animator>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // reset anim

        if(movement.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if(movement.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        if ( movement.x != 0)
        {
            Anim.SetBool("walkSide", true);
        }
        else
        {
            Anim.SetBool("walkSide", false);
        }
        if (movement.y < 0)
        {
            Anim.SetBool("walkFront", true);
        }
        else
        {
            Anim.SetBool("walkFront", false);
        }
        if (movement.y > 0)
        {
            Anim.SetBool("walkBehind", true);
        }
        else
        {
            Anim.SetBool("walkBehind", false);
        }



    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);



       
        
    }



    

}
