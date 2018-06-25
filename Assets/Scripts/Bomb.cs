using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable
{
    protected override void OnRabitHit(HeroRabbit rabbit)
    {
        if (rabbit.hasMushroom)
        {
            rabbit.transform.localScale = new Vector3(1, 1, 1);
            rabbit.hasMushroom = false;
        }
        else
        {
            // DIE MF DIE (c) Dope
            Animator animator = rabbit.GetComponent<Animator>();
            animator.SetTrigger("death");
            rabbit.isDead = true;
        }
        this.CollectedHide();
    }
}
