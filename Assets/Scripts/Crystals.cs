using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystals : Collectable
{
    protected override void OnRabitHit(HeroRabbit rabit)
    {
        LevelController.current.addCrystal();
        this.CollectedHide();
    }
}
