using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {

    public float rabbitSpeed = 1;
    Rigidbody2D rabbitBody = null;
    float value = 0;


    // Use this for initialization
    void Start () {
        rabbitBody = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        value = Input.GetAxis("Horizontal");
        // animation controller
        Animator animator = GetComponent<Animator>();
        if (Mathf.Abs(value) > 0)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }

        // move
        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = rabbitBody.velocity;
            vel.x = value * rabbitSpeed;
            rabbitBody.velocity = vel;
        }

        // flip
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (value < 0)
        {
            sr.flipX = true;
        }
        else if (value > 0)
        {
            sr.flipX = false;
        }
    }

    // Update is called once per frame
 //   void Update () {
		
	//}
}