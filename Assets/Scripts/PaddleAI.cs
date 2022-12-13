using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleAI : MonoBehaviour
{
    [Header ("Speed settings, spd must = speed")]
    public float speed;
    public float spd;

    public Rigidbody2D rBody;
    public Vector3 startPosition;
    public bool ifAbove;
    public Rigidbody2D bBody;
    public bool hasPlayer;
    

    private float movement;
    

    void Start()
    {
        startPosition = transform.position;
    }

    
    void Update()
    {
        if(hasPlayer) //if there is a player
        {
        movement = Input.GetAxisRaw("Vertical2"); //up and down arrow keys move up and down
        }

        else //if against algorithm
        {
            if(bBody.position.x < 6|| bBody.position.y == rBody.position.y) //if the ball is 8 x away or if they are the same height 
        {
            movement = 0; //movement is 0
        }
        else if(bBody.position.y > rBody.position.y) //if the ball is higher than the paddle and the ball is past x9
        {
            movement = spd; //move paddle up
        }
        else{ //if the ball is lower than the paddle and the ball is past x9
            movement = -spd; //move paddle down
        }



        if(bBody.position.x < 0 && rBody.position.y >0) //if the ball is less than 0 and the ball is above y0
        {
            movement = -spd; //make the paddle go down
        } 
        else if(bBody.position.x < 0 && rBody.position.y <0) // if the ball is below x0 and the paddle is below y0
        {
            movement = spd; //make the paddle go up
        }
        }

        rBody.velocity = new Vector2(0, movement * speed); //the paddle speed is no x value and the y is movement*speed
        
    }

    public void ResetPosition()
    {
        rBody.velocity = Vector2.zero;
        transform.position = startPosition;
    }


}
