using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    public bool hideAnimation; // ????????????????????

    protected virtual void OnRabitHit(HeroRabbit rabit)
    {
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!this.hideAnimation)
        {
            HeroRabbit rabit = collider.GetComponent<HeroRabbit>();
            if (rabit != null)
            {
                this.OnRabitHit(rabit);
            }
        }
    }

    public void CollectedHide()
    {
        Destroy(this.gameObject);
    }
}
