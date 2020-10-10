using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid
{

    public Vector2 position;
    public Vector2 velocity;
    public Vector2 acceleration;

    public float visionDist;

    public int flockmates = 0;
    public Vector2 avgFlockVelocity;
    public Vector2 avgFlockCentre;

    // settings
    Settings data;

    Boid(Vector2 velocity, float visionDist) {

        this.velocity = velocity;
        this.visionDist = visionDist;
        this.position = Vector2.zero;
        this.acceleration = Vector2.zero;
    }

    Vector2 SteerTowards(Vector2 vector) {
        //Set it to max speed 
        Vector2 vec = vector.normalized * data.maxSpeed - velocity;
        return Vector2.ClampMagnitude(vec, data.maxSteer);
    }

    void CheckOutofBounds(float arenaRadius) {

        if (position.magnitude > arenaRadius)
            acceleration += SteerTowards(-position);
        //Go to the middle again
    }

    //simple o(n) vision approach * boids
    void CheckVision(Boid[] boids) {

        Vector2 flockVelocity = Vector2.zero;
        Vector2 flockCentre = Vector2.zero;
        Vector2 avoid = Vector2.zero;
        flockmates = 0;

        foreach (var boid in boids) {
            if (boid != this) {

                float sqrDist = Vector2.SqrMagnitude(boid.position - this.position); //sqr is cheaper

                if (sqrDist < visionDist * visionDist) {

                    flockmates++;


                    flockVelocity += boid.velocity.normalized;//sdf dfe
                    flockCentre += boid.position;

                    // Add avoidance
                    if (true) {

                    }

                }
            }
        }
        if (boids.Length != 0) {
            // change values
            avgFlockVelocity = flockVelocity / flockmates;
            avgFlockCentre = flockCentre / flockmates;
        }
    }

    void UpdateBoid(float timestep) {
        acceleration = Vector2.zero;


        acceleration += SteerTowards(avgFlockVelocity);







        velocity += acceleration * timestep;


        //acceleration += Align().normalized;

        //transform.Translate(velocity * timestep);

        // transform.right = velocity.normalized;

    }



}
