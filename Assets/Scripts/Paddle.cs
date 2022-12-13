using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isP1;
    public float speed;
    public Rigidbody2D rBody;
    public Vector3 startPosition;

    private float movement;
    

    void Start()
    {
        startPosition = transform.position;
    }

   
    void Update()
    {
        if (isP1)
        {
            movement = Input.GetAxisRaw("Vertical");
        }
        else
        {
            movement = Input.GetAxisRaw("Vertical2");
        }
        rBody.velocity = new Vector2(0, movement * speed);
    }

    public void ResetPosition()
    {
        rBody.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}
