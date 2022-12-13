using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header ("Ball")]
     public GameObject ball; //so I can add ball to the controller

    [Header ("Player 1")] //so I can add the p1 paddle and goal to the controler
    public GameObject player1Paddle; 
    public GameObject player1Goal;


    [Header ("Player 2")] //so I can add the p2 paddle and goal to the controlelr 
    public GameObject player2Paddle; 
    public GameObject player2Goal;

    [Header ("Score UI")] //so I can add the scores for p1 and p2 to the controller
    public GameObject Player1Text;
    public GameObject Player2Text;

    [Header ("Text UI")] //The win texts and restart texts
    public GameObject Player1WinText;
    public GameObject Player2WinText;
    public GameObject restartTextObj;

    [Header ("Timing Settings")]
    public float freezeWearOff; //how long till freeze wears off
    public float turboWearOff; //how long till turbo wears off
    public float powerRespawn; //how long between each powerup in a wave
    public float firstPowerup; //how long till the first powerup spawns
    public float waveWait; //how long before each wave 

    [Header ("Powerup settings")]
    public GameObject powerup; //what are we spawning
    public Vector2 spawnValue; //where are we spawning
    public int powerupCount; //how many hazards are we spawning per wave

    private int Player1Score; //p1 score
    private int Player2Score; //p2 score
    private bool restart = false; //so I can initiate restarting 
    private int amount = 0;
    
    
    void Start()
    {
         
        StartCoroutine(spawnPowerups());
    }

    void Update() //used to check if you can restart the game 
    {
      //check if we need to restart
      if(restart) //if restart is true, if game is over
      {
        if(Input.GetKeyDown(KeyCode.R)) //if you press R
        {
            SceneManager.LoadScene("Pong"); //reload the game
        }
      }  
      
    }


    public void Player1Scored() //if p1 scores
    {
        Player1Score++; //increase p1 score by 1
        Player1Text.GetComponent<TextMeshProUGUI>().text = Player1Score.ToString(); //change the score to the score p1 has
        if(Player1Score == 3) //if p1 has 3 points
        {
            p1Win(); //initiate player 1 winning
        }
        else
        {
        ResetPosition(); //otherwise, restart the positions and play another round
        }
    }

    public void Player2Scored() //if p2 scores
    {
        Player2Score++; //increase p2 score by 1
        Player2Text.GetComponent<TextMeshProUGUI>().text = Player2Score.ToString(); //update their score to their current score
        if(Player2Score ==3) //if they have 3 points
        {
            p2Win(); //initiate p2 winning
        }
        else{
        ResetPosition();//otherwise, restart the positions and play another round
        }
    }

    private void ResetPosition() //resetting everythings positions
    {
        ball.GetComponent<Ball>().Reset(); //reset the ball position and launch it again
        player1Paddle.GetComponent<Paddle>().ResetPosition(); //reset both players positions
        player2Paddle.GetComponent<PaddleAI>().ResetPosition();
    }
    
    public void p1Win() //if p1 wins
    {
        
        Player1WinText.SetActive(true); //activate their winning text

        restart = true; //restart is available 
        restartTextObj.SetActive(true); //activate the restart text to instruct them how to
    }

    public void p2Win() //if p2 wins
    {
        
        Player2WinText.SetActive(true); //activate p2 win text

        restart = true; //restart possible
        restartTextObj.SetActive(true); //activate the restart text to instruct them how to
    }

    public void powerUp(bool p1Obtained)
    {
        amount-=1; //if a powerup is acquired, remove 1 powerup from the counter
        int ran = Random.Range(0, 2);
            if(ran == 0)
            {
                freeze(p1Obtained);
            }
            else if((ran==1))
            {
                turbo(p1Obtained);
            }
        
        
    }

    public void freeze(bool p1Obtained)
    {
        if(p1Obtained) //if p1 got the powerup
        {
            player2Paddle.GetComponent<PaddleAI>().speed = 0; //p2 is frozen
        }
        else
        {
            player1Paddle.GetComponent<Paddle>().speed = 0; //p1 is frozen
        }

        StartCoroutine(wearOff(p1Obtained, true));
        Debug.Log("Freeze");
        Debug.Log(p1Obtained);
    }

    public void turbo(bool p1Obtained)
    {
        if(p1Obtained) //if p1 got the powerup
        {
            player2Paddle.GetComponent<PaddleAI>().speed= 16; //p2 is double spd!
        }
        else
        {
            player1Paddle.GetComponent<Paddle>().speed = 16; //p1 is double spd!
        }
        StartCoroutine(wearOff(p1Obtained, false));

        Debug.Log("Turbo");
        Debug.Log(p1Obtained);
    }



    IEnumerator wearOff(bool p1Obtained, bool freeze) //take who got the powerup and what kind of powerup it was
    {
        if(freeze) //if it was a freeze powerup
        {
            while(true)// go through the loop
        {
            yield return new WaitForSeconds(freezeWearOff); //wait till the effect should wear off
            if(p1Obtained) //if p1 got the powerup
            {
                player2Paddle.GetComponent<PaddleAI>().speed= 8; //p2 unfreezes
            }
            else //if p2 got the powerup
            {
                player1Paddle.GetComponent<Paddle>().speed= 8; //p1 unfreezes
            }
            break;//exit the loop
        }
        }
        else
        {
            while(true)
        {
            yield return new WaitForSeconds(turboWearOff); //wait till the effect should wear off
            if(p1Obtained) //if p1 got the powerup
            {
                player2Paddle.GetComponent<PaddleAI>().speed= 8; //p2 exits turbo
                Debug.Log("Unfrozen!");
            }
            else //if p2 got the powerup 
            {
                player1Paddle.GetComponent<Paddle>().speed= 8; //p1 exits turbo
            }
            break;
        }
        }
    }

    IEnumerator spawnPowerups()
    {
        
        yield return new WaitForSeconds(firstPowerup);
        
        //start spawning powerups
        while(true)
        {
            //spawn powerup
            
            for (int i =0; i < powerupCount; i++)
                {
                    Debug.Log("Amount of powerups: "+amount);
                //spawn a single powerup
                if(amount >=2){ //simple check if there is 2 powerups active
                    break; //if there is, don't make more
                }
                Vector2 spawnPosition = new Vector2(Random.Range(-spawnValue.x, spawnValue.x), Random.Range(-spawnValue.y, spawnValue.y));
                Instantiate(powerup, spawnPosition, Quaternion.identity); // gimbal lock (re: quaternion)
                amount +=1;
                
                //wait, then spawn the next powerup
                yield return new WaitForSeconds(powerRespawn);
                }
            

            // wait, spawn the next wave
            yield return new WaitForSeconds(waveWait);

            //check to see if the game is over
            /*if(gameover)
            {
                restart = true;
                restartTextObj.SetActive(true);
                break;
            }*/
        }
    }

}
