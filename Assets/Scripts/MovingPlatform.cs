using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    //public Vector3 MoveBy;
    Vector3 pointA;
    Vector3 pointB;
    Vector3 nextPosition;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform childTransform;
    [SerializeField]
    private Transform transformB;

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
        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPosition, speed*Time.deltaTime);

        if (Vector3.Distance(childTransform.localPosition, nextPosition) < 0.02f)
        {
            changeDestination();
        }
    }

    private void changeDestination()
    {
        nextPosition = nextPosition == pointA ? pointB : pointA;
    }

    //bool isArrived(Vector3 pos, Vector3 target)
    //{
    //    pos.z = 0;
    //    target.z = 0;
    //    return Vector3.Distance(pos, target) < 0.02f;
    //}
}
