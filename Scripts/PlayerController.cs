using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   private Rigidbody2D rb;
    private Vector2 direction; 

    public float moveSpeed =3f;
    public float jumpStrength = 4f;


    private Collider2D collider;   //reference to the collider 
    private Collider2D[] results; //a 2D array to tell us if we collided with ground or the ladder


    private bool grounded; //bool check for if on ground or the ladder
    private bool climbing; // bool check for climbing 


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        results = new Collider2D[4];
    }

    // Update is called once per frame
    private void Update()
    {
        collisionCheck();
        directionCheck(); 
    }

    //New method to check if the player is touching the ground or the ladder
    private void collisionCheck()
    {
        grounded = false;   //initail set to false //Then we check if it is gounded or not 
        climbing = false; //asumption that we are not climbing 
        Vector3 size = collider.bounds.size;  //this is to reduce the size of the collider
        size.y += 0.1f;                      // to give the illusion that the player is on the stairs
        size.x /=2f;

        int amount = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0f, results);  //OverlapBoxNonAlloc
        //Finds all colliders touching or inside of the given box, and stores them into the buffer.

        for (int i=0; i<amount; i++)
        {
            GameObject hit = results[i].gameObject;
            if(hit.layer == LayerMask.NameToLayer("Ground"))
            {
            grounded = hit.transform.position.y < (transform.position.y - 0.5f); // the collision has to be grounded if it is below the player
            Physics2D.IgnoreCollision(collider, results[i], !grounded); // the physics engine is to ignore the collider if the collision is above us 
            //simply makes the player movement more smooth
            //player doesnt appear floating 
            }

            else if(hit.layer == LayerMask.NameToLayer("Ladders"))
            {
               climbing = true; 
            }
        }

    }

    private void directionCheck()
    {
        if (climbing) {
            direction.y = Input.GetAxis("Vertical") * moveSpeed;   //If the player is on stairs, it will move upwards. 
        } else if (grounded && Input.GetKeyDown(KeyCode.Space)) {
            direction = Vector2.up * jumpStrength;   //when on ground and pscae key pressed, the player will jump. 
        } else {
            direction += Physics2D.gravity * Time.deltaTime;
        }

        direction.x = Input.GetAxis("Horizontal") * moveSpeed;   //Horizontal input

        // Prevent gravity from building up or compounding
        if (grounded) {
            direction.y = Mathf.Max(direction.y, -1f);
        }

        //change the way player looks (left or right )
        if (direction.x > 0f) {
            transform.eulerAngles = Vector3.zero;   //if right, that is defualt so no need to change 
        } else if (direction.x < 0f) {
            transform.eulerAngles = new Vector3(0f, 180f, 0f); // if left, the player needs to turn 180
        }
    }
    

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * Time.fixedDeltaTime); //MovePosition to move the player every frame
    }
}

