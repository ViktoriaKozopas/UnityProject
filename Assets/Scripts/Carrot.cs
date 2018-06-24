using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Collectable {

    protected override void OnRabitHit(HeroRabbit rabbit)
    {
        Animator animator = rabbit.GetComponent<Animator>();
        animator.SetTrigger("death");
        rabbit.isDead = true;
        this.CollectedHide();
    }
}
