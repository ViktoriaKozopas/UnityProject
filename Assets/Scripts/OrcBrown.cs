using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcBrown : MonoBehaviour {
    private float currentSpeed;
    public float speed;

    public Transform transformA;
    public Transform transformB;
    public Transform rabbit;

    Rigidbody2D orcBody;
    SpriteRenderer orcSprite;

    public AnimationClip deathAnimation;
    private float deathTimer;

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
        currentSpeed = speed;
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
            orcBody.velocity = new Vector2(currentSpeed, orcBody.velocity.y);
        }
        else
        {
            orcBody.velocity = new Vector2(-currentSpeed, orcBody.velocity.y);
        }

        // if rabbit in orc zone
        if (HeroRabbit.lastRabbit.transform.position.x > Mathf.Min(transformA.position.x, transformB.position.x) &&
            HeroRabbit.lastRabbit.transform.position.x < Mathf.Max(transformA.position.x, transformB.position.x))
        {
            mode = Mode.Attack;
        }

        if (mode == Mode.Attack)
        {
            //TODO: kill rabbit with carrot
            currentSpeed = 0;

            if (HeroRabbit.lastRabbit.transform.position.x < Mathf.Min(transformA.position.x, transformB.position.x) ||
                HeroRabbit.lastRabbit.transform.position.x > Mathf.Max(transformA.position.x, transformB.position.x))
            {
                //TODO: if rabbit out of zone - return orc idle animation return
                currentSpeed = speed;
            }
        }

    }
}
