using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public Vector2 acceleration;
    public Vector2 velocity;

    void Start()
    {
        
    }



    void Update()
    {
        transform.Translate(velocity * Time.deltaTime);




    }
}
