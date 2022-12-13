using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainPowerUp : MonoBehaviour
{

    [Header ("Ball")]
    public Rigidbody2D Ball;

    private bool p1Obtained;
    private GameController gameControllerScript;  
    private GameObject ballObject; 
    
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        //Debug.Log("Potato");
        if(gameControllerObject != null)
        {
            gameControllerScript = gameControllerObject.GetComponent<GameController>();
            //Debug.Log("Here");
        }
        if(gameControllerScript == null)
        {
            Debug.Log("Cannot find game controller script on GameController object");
        }

        ballObject = GameObject.FindWithTag("Ball"); //ballObject is equal to the ball object
    }

    
    void Update()
    {
        if(ballObject.GetComponent<Rigidbody2D>().velocity.x > 0) //if the ball is heading towards p2 goal
        //take the ball object that is referencing ball in code, and then grab specifcally the Rigidbody2D component and find it's velocity
        {
            p1Obtained = true; //then the powerup will go to p1
        }
        else if(ballObject.GetComponent<Rigidbody2D>().velocity.x < 0) //if the ball is heading towards p1 goal
        {
            p1Obtained = false; // then the powerup will go to p2
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
        {
            
            if(p1Obtained) //if p1 obtained the powerup
            {
                Debug.Log("Player 1 got the powerup"); 
                Destroy(this.gameObject); //destroy the powerup, since it's a trigger, it won't interfere with the balls path
                gameControllerScript.powerUp(p1Obtained);
            }
            else if(p1Obtained == false)//if p2 obtained the powerup
            {
                Debug.Log("Player 2 got the powerup");
                Destroy(this.gameObject); //destroy the powerup, since it's a trigger, it won't interfere with the balls path
                gameControllerScript.powerUp(p1Obtained);
                
            }
        }
    
}
