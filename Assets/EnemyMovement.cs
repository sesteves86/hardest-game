using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum EnemyMovementTypes
    {
        UpAndDown,
        LeftAndRight,
        InCircles
    }

    public EnemyMovementTypes movementType = 0;
    public float thrust = 40;
    public float startingTime = 0;
    public float periodTime = 2;
    public Rigidbody2D rb;

    private float dt = 0;
    private float initialX = 0;
    private float initialY = 0;

    // Start is called before the first frame update
    void Start()
    {
        dt = startingTime;
        initialX = rb.transform.position.x;
        initialY = rb.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float x = 0;
        float y = 0;
        switch (movementType)
        {
            case EnemyMovementTypes.LeftAndRight:
                y = rb.transform.position.y;
                x = initialX + thrust * Mathf.Sin(dt );
                break;
            case EnemyMovementTypes.InCircles:
                y = initialY + thrust * Mathf.Sin(dt );
                x = initialX + thrust * Mathf.Cos(dt );
                break;
            case EnemyMovementTypes.UpAndDown:
                x = rb.transform.position.x;
                y = initialY + thrust * Mathf.Sin(dt);
                break;
            default:
                break;
        }

        //Debug.Log($"{dt}, {x} ,{y}");

        rb.transform.position = new Vector2(x, y);
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        dt += Time.deltaTime / (periodTime / 10);

        if (dt > Mathf.PI) {
            dt = -dt;
        }
    }
}


