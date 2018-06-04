using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public static LevelController current;
    Vector3 startingPosition;

    private int coins = 0;
    private int fruits = 0;
    private int crystals = 0;

    void Awake()
    {
        current = this;
    }

    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }

    public void onRabitDeath(HeroRabbit rabit)
    {
        //При смерті кролика повертаємо на початкову позицію
        rabit.transform.position = this.startingPosition;
    }

    public void addCoins(int coin)
    {
        this.coins += coin;
    }

    public void addFruits()
    {
        this.fruits++;
    }

    public void addCrystal()
    {
        this.crystals++;
    }

    public void addMushroom()
    {

    }
}
