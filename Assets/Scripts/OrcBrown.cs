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

    private float last_carrot = 0;

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
        else if(mode == Mode.GoToA)
        {
            orcBody.velocity = new Vector2(-currentSpeed, orcBody.velocity.y);
        }

        // if rabbit in orc zone
        if (HeroRabbit.lastRabbit.transform.position.x > Mathf.Min(transformA.position.x, transformB.position.x) &&
            HeroRabbit.lastRabbit.transform.position.x < Mathf.Max(transformA.position.x, transformB.position.x))
        {
            mode = Mode.Attack;
        } else if (mode == Mode.Attack)
        {
            mode = Mode.GoToA;
            currentSpeed = speed;
        }

        if (mode == Mode.Attack)
        {
            //TODO: kill rabbit with carrot
            currentSpeed = 0;
            //this.launchCarrot(1);

            // wait for interval
            if (currentInterval <= 0)
            {
                this.launchCarrot(1);
                currentInterval = carrotInterval;
                // TODO: set direction depending on rabbit position
            }
        }

    }

    /** starts carrot
    */
    void launchCarrot(float direction)
    {
        //Створюємо копію Prefab
        GameObject obj = GameObject.Instantiate(this.prefabCarrot);
        //Розміщуємо в просторі
        //obj.transform.position = this.transform.position;
        obj.transform.position = transformCarrot.position;
        //Запускаємо в рух
        Carrot carrot = obj.GetComponent<Carrot>();
        //carrot.launch(direction);
    }
}
