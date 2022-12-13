using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rBody;
    public Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
       Launch(); 
    }

    public void Update()
    {
        //Debug.Log(rBody.velocity.x);
    }

    public void Reset()
    {
        rBody.velocity = Vector3.zero;
        transform.position = startPosition;
        Launch();
    }

    private void Launch()
    {
        float x = Random.Range(0,2) ==0 ? -1:1;
        float y = Random.Range(0,2) ==0 ? -1:1;
        rBody.velocity= new Vector2(speed * x, speed * y);
    }
}
