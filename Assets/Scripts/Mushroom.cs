using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Collectable
{
    protected override void OnRabitHit(HeroRabbit rabbit)
    {
        if(!rabbit.hasMushroom)
        {
            rabbit.transform.localScale = new Vector3(rabbit.transform.localScale.x * LevelController.mushroomMax,
                                                      rabbit.transform.localScale.y * LevelController.mushroomMax,
                                                      rabbit.transform.localScale.z * LevelController.mushroomMax);
            rabbit.hasMushroom = true;
        }
        this.CollectedHide();
    }
}
