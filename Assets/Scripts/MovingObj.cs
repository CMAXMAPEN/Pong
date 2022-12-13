using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObj : MonoBehaviour
{
    public Vector3 startPosition;

    public float frequency;
    public float magnitude;
    public float offset;


    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position = startPosition + transform.up * Mathf.Sin(Time.time * frequency + offset) * magnitude;
    }

        
    
}
