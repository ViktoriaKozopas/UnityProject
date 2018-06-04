using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : Collectable
{
    protected override void OnRabitHit(HeroRabbit rabit)
    {
        LevelController.current.addFruits();
        this.CollectedHide();
    }
}
