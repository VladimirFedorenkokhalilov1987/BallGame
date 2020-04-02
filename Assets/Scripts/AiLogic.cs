using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiLogic : MonoBehaviour
{ 
    private GameObject ball;
    private Vector3 move = Vector3.zero;
    public float speed = 1.0f;

    private void Awake()
    {
        ball=GameObject.FindObjectOfType<BallLogic>().gameObject;
    }

    public void IncreaseSpeed()
    {
        speed += 1f;
    }

    void Update()
    {
       float distance =Vector3.Distance(transform.position, ball.transform.position);
       if (distance<6f)
       {
           float distance2 = ball.transform.position.x - transform.position.x;
           if (distance2 > 0)
           {
               move.x = speed * Mathf.Min(distance2, 10.0f);
           }

           if (distance2 < 0)
           {
               move.x = -(speed * Mathf.Min(-distance2, 10.0f));
           }
           transform.position += move * Time.deltaTime;
       }
     }
}
