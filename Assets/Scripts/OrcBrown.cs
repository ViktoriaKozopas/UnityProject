using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcBrown : MonoBehaviour
{
    private float currentSpeed;
    public float speed;

    public Transform transformA;
    public Transform transformB;
    public HeroRabbit rabbit;
    public Transform transformCarrot;

    Rigidbody2D orcBody;
    SpriteRenderer orcSprite;

    public AnimationClip deathAnimation;
    private float deathTimer;

    public GameObject prefabCarrot;
    public float carrotInterval;
    public float currentInterval;

    private bool die = false;

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

        currentInterval = carrotInterval;
    }

    void FixedUpdate()
    {
        if (currentInterval > 0)
        {
            currentInterval -= Time.deltaTime;
        }

        //orc move from point A to point B
        //orcSprite.flipX = mode == Mode.GoToB ? true : false; //bad variant
        transform.localScale = mode == Mode.GoToB ? new Vector3(-1, transform.localScale.y, transform.localScale.z) :
                                                    new Vector3(1, transform.localScale.y, transform.localScale.z);
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
        else if (mode == Mode.GoToA)
        {
            orcBody.velocity = new Vector2(-currentSpeed, orcBody.velocity.y);
        }

        // if rabbit in orc zone
        if (HeroRabbit.lastRabbit.transform.position.x > Mathf.Min(transformA.position.x, transformB.position.x) &&
            HeroRabbit.lastRabbit.transform.position.x < Mathf.Max(transformA.position.x, transformB.position.x))
        {
            mode = Mode.Attack;
        }
        else if (mode == Mode.Attack)
        {
            mode = Mode.GoToA;
            currentSpeed = speed;
        }

        if (mode == Mode.Attack)
        {
            currentSpeed = 0;
            Animator animatorOrc = GetComponent<Animator>();
            animatorOrc.SetBool("attack", true);

            // wait for interval
            if (currentInterval <= 0)
            {
                this.launchCarrot(1);
                currentInterval = carrotInterval;
                // TODO: set direction depending on rabbit position
            }
        }
        else
        {
            Animator animatorOrc = GetComponent<Animator>();
            animatorOrc.SetBool("attack", false);
        }

    }

    /** starts carrot
    */
    void launchCarrot(float direction)
    {
        GameObject obj = GameObject.Instantiate(this.prefabCarrot);
        obj.transform.position = transformCarrot.position;
        Carrot carrot = obj.GetComponent<Carrot>();
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
            else if (!die)
            {
                Animator animator = rabbit.GetComponent<Animator>();
                animator.SetTrigger("death");
                rabbit.isDead = true;
            }
        }
    }


    public void Hurt()
    { 
        Destroy(this.gameObject);
    }
}
