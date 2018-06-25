using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Collectable {
    public float carrotSpeed;

    void Start()
    {
        StartCoroutine(destroyLater());
    }

    void FixedUpdate()
    {
        this.transform.Translate(Vector3.left * carrotSpeed);
    }

    IEnumerator destroyLater()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

    protected override void OnRabitHit(HeroRabbit rabbit)
    {
        Animator animator = rabbit.GetComponent<Animator>();
        animator.SetTrigger("death");
        rabbit.isDead = true;

        //this.CollectedHide();

        Destroy(this.gameObject);
    }
}
