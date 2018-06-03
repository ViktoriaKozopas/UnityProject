using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {

    public float rabbitSpeed = 1;
    Rigidbody2D rabbitBody = null;
    float value = 0;

    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;

   // Transform heroParent = null;

    void Start () {
        rabbitBody = this.GetComponent<Rigidbody2D>();
        //Зберігаємо позицію кролика на початку
        LevelController.current.setStartPosition(transform.position);

        //Зберегти стандартний батьківський GameObject
       // this.heroParent = this.transform.parent;
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

        move();
        jump();
       // mPlatform();
    }
private void move()
    {
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
private void jump()
    {
        //JUMP
        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");

        //Перевіряємо чи проходить лінія через Collider з шаром Ground
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        if (hit)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        //Намалювати лінію (для розробника)
        Debug.DrawLine(from, to, Color.red);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            this.JumpActive = true;
        }
        if (this.JumpActive)
        {
            //Якщо кнопку ще тримають
            if (Input.GetButton("Jump"))
            {
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.MaxJumpTime)
                {
                    Vector2 vel = rabbitBody.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                    rabbitBody.velocity = vel;
                }
            }
            else
            {
                this.JumpActive = false;
                this.JumpTime = 0;
            }
        }

        Animator animator = GetComponent<Animator>();
        if (this.isGrounded)
        {
            animator.SetBool("jump", false);
        }
        else
        {
            animator.SetBool("jump", true);
        }
    }
    //private void mPlatform()
    //{
    //    Vector3 from = transform.position + Vector3.up * 0.3f;
    //    Vector3 to = transform.position + Vector3.down * 0.1f;
    //    int layer_id = 1 << LayerMask.NameToLayer("Ground");

    //    //Згадуємо ground check
    //    RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
    //    Debug.DrawLine(from, to, Color.red);
    //    if (hit)
    //    {
    //        //Перевіряємо чи ми опинились на платформі
    //        if (hit.transform != null
    //        && hit.transform.GetComponent<MovingPlatform>() != null)
    //        {
    //            //Приліпаємо до платформи
    //            SetNewParent(this.transform, hit.transform);
    //        }
    //    }
    //    else
    //    {
    //        //Ми в повітрі відліпаємо під платформи
    //        SetNewParent(this.transform, this.heroParent);
    //    }
    //}

    static void SetNewParent(Transform obj, Transform new_parent)
    {
        if (obj.transform.parent != new_parent)
        {
            //Засікаємо позицію у Глобальних координатах
            Vector3 pos = obj.transform.position;

            //Встановлюємо нового батька
            obj.transform.parent = new_parent;

            //Після зміни батька координати кролика зміняться
            //Оскільки вони тепер відносно іншого об’єкта
            //повертаємо кролика в ті самі глобальні координати
            obj.transform.position = pos;
        }
    }
}