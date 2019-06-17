using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5.0f;
   /* public Vector2 forcex_1;
    public Vector2 forcex_2;
    public Vector2 forcey_3;
    public Vector2 forcey_4;*/
    public Vector2 pom;

    // Update is called once per frame
    void FixedUpdate()
    {
        check();
        rb.velocity = pom;
    }
    void check()
    {
        if (Input.GetKey("a"))
        {

            pom = speed* Vector2.left; // rb.AddForce(forcex_1);//, ForceMode2D.VelocityChange);
        }
        if (Input.GetKey("d"))
        {
            pom = speed * Vector2.right;
            //            rb.AddForce(forcex_2);//, ForceMode2D.VelocityChange);
        }
        if (Input.GetKey("w"))
        {
            pom = speed * Vector2.up;
            //            rb.AddForce(forcey_3);//, ForceMode2D.VelocityChange);
        }
        if (Input.GetKey("s"))
        {
            pom = speed * Vector2.down;
            //            rb.AddForce(forcey_4);//, ForceMode2D.VelocityChange);
        }
    }
    public Vector2 help()
    {
        return rb.velocity;
    }
}
