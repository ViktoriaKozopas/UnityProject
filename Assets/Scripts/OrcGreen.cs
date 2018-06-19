using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcGreen : MonoBehaviour {
    public float speed;

    public Transform transformA;
    public Transform transformB;
    public Transform rabbit;

    Rigidbody2D orcBody;
    SpriteRenderer orcSprite;

    Vector3 rabbit_position; // for attack? if no use - delete

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

//        rabbit_position = HeroRabbit.lastRabbit.transform.position;
        mode = Mode.GoToA;
    }

    void FixedUpdate()
    {
        //orc move from point A to point B
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
        // if rabbit in green orc zone
        if (HeroRabbit.lastRabbit.transform.position.x > Mathf.Min(transformA.position.x, transformB.position.x) &&
            HeroRabbit.lastRabbit.transform.position.x < Mathf.Max(transformA.position.x, transformB.position.x))
        {
            mode = Mode.Attack;
        }

        if(mode == Mode.Attack)
        {
            Debug.Log("Attack");
            transform.position = Vector2.MoveTowards(gameObject.transform.position, rabbit.transform.position, speed * Time.deltaTime);
        }

    }
}