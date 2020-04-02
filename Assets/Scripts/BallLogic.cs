using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogic : MonoBehaviour
{
    public Rigidbody ballRigidbody;
    // Start is called before the first frame update
    void Awake()
    {
        if(ballRigidbody==null)
        ballRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "wall")
        {
            ballRigidbody.AddRelativeForce(new Vector3(UnityEngine.Random.Range(-10.0f, 10.0f), 0, UnityEngine.Random.Range(-10.0f, 10.0f)));
        }

        if (other.gameObject.tag=="target")
        {
            ballRigidbody.rotation = Quaternion.identity;
            ballRigidbody.velocity = Vector3.zero;
            FindObjectOfType<GameLogic>().count++;
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        
        if (other.gameObject.tag == "ai")
        {
            ballRigidbody.rotation = Quaternion.identity;
            ballRigidbody.velocity = Vector3.zero;
            other.transform.position = new Vector3(0, 1,4);
            gameObject.SetActive(false);
        }
    }
}
