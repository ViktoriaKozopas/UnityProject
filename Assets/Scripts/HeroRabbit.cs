using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {

    public float rabbitSpeed = 1;
    Rigidbody2D rabbitBody = null;
  

	// Use this for initialization
	void Start () {
        rabbitBody = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float value = Input.GetAxis("Horizontal");
        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = rabbitBody.velocity;
            vel.x = value * rabbitSpeed;
            rabbitBody.velocity = vel;
        }

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
    void Update () {
		
	}
}