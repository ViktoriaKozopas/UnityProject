using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkGreen : MonoBehaviour {
    public float speed;
    //public float delay;
    //private float platformTimer;

    public Transform transformA;
    public Transform transformB;

    Rigidbody2D orcBody;
    SpriteRenderer orcSprite;

    bool onRight;

    void Awake()
    {
        orcBody = GetComponent<Rigidbody2D>();
        orcSprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        orcSprite.flipX = onRight;
        if (gameObject.transform.position.x < transformA.position.x)
        {
            onRight = true;
        }
        else if (gameObject.transform.position.x > transformB.position.x)
        {
            onRight = false;
        }

        if (onRight)
        {
            orcBody.velocity = new Vector2(speed, orcBody.velocity.y);
        }else
        {
            orcBody.velocity = new Vector2(-speed, orcBody.velocity.y);
        }
    }

}