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
            //Debug.Log("Attack");
            transform.position = Vector2.MoveTowards(gameObject.transform.position, rabbit.transform.position, speed * Time.deltaTime);
            //TODO: set run animation

            if (HeroRabbit.lastRabbit.transform.position.x < Mathf.Min(transformA.position.x, transformB.position.x) ||
                HeroRabbit.lastRabbit.transform.position.x > Mathf.Max(transformA.position.x, transformB.position.x))
            {
                //TODO: if rabbit out of zone - orc idle animation return
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HeroRabbit rabbit = collision.collider.GetComponent<HeroRabbit>();
        if(rabbit != null)
        {
           foreach(ContactPoint2D point in collision.contacts)
            {
                Debug.Log(point.normal);
                Debug.DrawLine(point.point, point.point + point.normal, Color.red, 10);

                if(Mathf.Abs(point.point.y) >= 0.6f)
                {
                    Hurt();
                }else
                {
                    Debug.Log("Not killed");
                }
            }
        }
    }

    public void Hurt()
    {
        Destroy(this.gameObject);
    }
}