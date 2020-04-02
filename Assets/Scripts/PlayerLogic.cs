using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    private Vector3 mOffset;
    private Vector3 StartPosition;
    private float mZCoord;
    private float distance;
    public RectTransform Direction;
    public Slider Power;
    public bool CanShot=false;

    private void Awake()
    {
        if (Direction == null)
            Direction = GameObject.Find("Direction").GetComponent<RectTransform>();
        if (Power == null)
            Power = GameObject.FindObjectOfType<Slider>();
    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (CanShot)
            {
                Power.gameObject.SetActive(true);
                StartPosition = gameObject.transform.position;
                mZCoord = Camera.main.WorldToScreenPoint(
                    gameObject.transform.position).z;
                // Store offset = gameobject world pos - mouse world pos
                mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
            }
        }
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;
        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    void OnMouseDrag()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (CanShot)
            {
                Direction.rotation = Quaternion.Euler(Direction.rotation.x, Direction.rotation.y,
                    gameObject.transform.position.x * 8);
                transform.position = GetMouseAsWorldPoint() + mOffset;
                distance = Vector3.Distance(transform.position, StartPosition);
            }
        }
    }
    void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Power.gameObject.SetActive(false);
            if (CanShot)
            {
                GameObject.FindObjectOfType<BallLogic>().ballRigidbody.AddForce(
                    new Vector3(-gameObject.transform.position.x, 0, -gameObject.transform.position.z) * distance * 3);
                gameObject.transform.position = StartPosition;
                Direction.rotation = Quaternion.identity;
                CanShot = false;

            }
        }
    }
    private void Update()
    {
        Power.value = (gameObject.transform.position.z/StartPosition.z)-1;
        
        if (gameObject.transform.position.x > 3)
        {
            gameObject.transform.position= new Vector3(3, 1, gameObject.transform.position.z);
        }

        if (gameObject.transform.position.x < -3)
        {
            gameObject.transform.position= new Vector3(-3, 1, gameObject.transform.position.z);
        }
        
        if (gameObject.transform.position.z < -9)
        {
            gameObject.transform.position= new Vector3(gameObject.transform.position.x, 1, -9);
        }
        
        if (gameObject.transform.position.z > -6)
        {
            gameObject.transform.position= new Vector3(gameObject.transform.position.x, 1, -6);
        }
        
        if (gameObject.transform.position.y>1 || gameObject.transform.position.y <1)
        {
            gameObject.transform.position= new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z);
        }
    }
}
