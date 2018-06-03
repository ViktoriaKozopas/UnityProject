using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    //public Vector3 MoveBy;
    Vector3 pointA;
    Vector3 pointB;
    Vector3 nextPosition;

    public float speed;
    public float delay;
    private float platformTimer;

    public Transform childTransform;
    public Transform transformB;

    // Use this for initialization
    void Start()
    {
        this.pointA = childTransform.localPosition;
        this.pointB = transformB.localPosition;
        this.nextPosition = pointB;
    }

    void Update()
    {
        move();   
    }

    private void move()
    {
        if (Vector3.Distance(childTransform.localPosition, nextPosition) < 0.02f)
        {
            if (platformTimer >= delay)
            {
                changeDestination();
            }
            else
            {
                platformTimer += Time.deltaTime;
            }
            
        }
        else
        {
            childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPosition, speed * Time.deltaTime);
        }
    }

    private void changeDestination()
    {
        nextPosition = nextPosition == pointA ? pointB : pointA;
        platformTimer = 0;
    }
}
