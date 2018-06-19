using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcGreen : MonoBehaviour {
    public float speed;

    public Transform transformA;
    public Transform transformB;

    Rigidbody2D orcBody;
    SpriteRenderer orcSprite;

    Vector3 rabbit_position;

    public enum Mode
    {
        GoToA,
        GoToB,
        Attack
    }

    Mode mode;

    void Awake()
    {
        orcBody = GetComponent<Rigidbody2D>();
        orcSprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {

        rabbit_position = HeroRabbit.lastRabbit.transform.position;
        mode = Mode.GoToA;
    }

    void FixedUpdate()
    {
        orcSprite.flipX = mode == Mode.GoToB ? true : false;
        if (gameObject.transform.position.x < transformA.position.x)
        {
            mode = Mode.GoToB;
        }
        else if (gameObject.transform.position.x > transformB.position.x)
        {
            mode = Mode.GoToA;
        }

        if (mode == Mode.GoToB)
        {
            orcBody.velocity = new Vector2(speed, orcBody.velocity.y);
        }else
        {
            orcBody.velocity = new Vector2(-speed, orcBody.velocity.y);
        }
    }

}