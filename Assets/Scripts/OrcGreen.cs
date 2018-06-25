using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcGreen : MonoBehaviour
{
    public float speed;
    public float fastSpeed;

    bool die = false;
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
        fastSpeed = speed * 1.5f;
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
        }
        else if(mode == Mode.GoToA)
        {
            orcBody.velocity = new Vector2(-speed, orcBody.velocity.y);
        }

        // if rabbit in green orc zone
        if (HeroRabbit.lastRabbit.transform.position.x > Mathf.Min(transformA.position.x, transformB.position.x) &&
            HeroRabbit.lastRabbit.transform.position.x < Mathf.Max(transformA.position.x, transformB.position.x))
        {
            mode = Mode.Attack;
        }
        else if (mode == Mode.Attack)
        {
            mode = Mode.GoToA;
        }

        if (mode == Mode.Attack)
        {
            orcBody.velocity = new Vector2(fastSpeed*((gameObject.transform.position.x - rabbit.transform.position.x)>0 ? -1:1),
                orcBody.velocity.y);
            //transform.position = Vector2.MoveTowards(gameObject.transform.position, rabbit.transform.position, speed * Time.deltaTime);
            //TODO: set run animation
            Animator animator = GetComponent<Animator>();
            animator.SetBool("run", true);

            orcSprite.flipX = gameObject.transform.position.x - rabbit.transform.position.x > 0 ? false : true;
        }
        else
        {
            Animator animator = GetComponent<Animator>();
            animator.SetBool("run", false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HeroRabbit rabbit = collision.collider.GetComponent<HeroRabbit>();
        if (rabbit != null)
        {
            ContactPoint2D point = collision.contacts[0];
            Debug.Log(point.normal);
            Debug.DrawLine(point.point, point.point + point.normal, Color.red, 10);

            if (Mathf.Abs(point.point.y) >= 0.5f)
            {
                Animator animator = this.GetComponent<Animator>();
                animator.SetTrigger("death");
                die = true;
            }
            else if(!die)
            {
                Animator animatorOrc = GetComponent<Animator>();
                animatorOrc.SetTrigger("attack");

                Animator animator = rabbit.GetComponent<Animator>();
                animator.SetTrigger("death");
                rabbit.isDead = true;


            }

        }
    }

    public void Hurt()
    {
        //Debug.Log(animator);
        Destroy(this.gameObject);
    }
}