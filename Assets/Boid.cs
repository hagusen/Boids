using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boid
{

    public Vector2 position;
    public Vector2 velocity;
    public Vector2 acceleration;


    public int flockmates = 0;
    public Vector2 avgFlockVelocity;
    public Vector2 avgFlockCentre;
    public Vector2 avgFlockSeparation;

    // settings
    Settings data;
    public Boid(Vector2 velocity, Vector2 position, Settings data) {

        this.velocity = velocity;
        this.position = position;
        this.acceleration = Vector2.zero;
        this.data = data;

        avgFlockVelocity = Vector2.zero;
        avgFlockCentre = Vector2.zero;
        avgFlockSeparation = Vector2.zero;


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
    public void CheckVision(Boid[] boids) {

        Vector2 flockVelocity = Vector2.zero;
        Vector2 flockCentre = Vector2.zero;
        Vector2 flockSeparation = Vector2.zero;
        flockmates = 0;

        foreach (var boid in boids) {
            if (boid != this) {
                Vector2 delta = boid.position - this.position;
                float sqrDist = Vector2.SqrMagnitude(delta); //sqr is cheaper

                if (sqrDist < data.visionDist * data.visionDist) {

                    flockmates++;
                    flockVelocity += boid.velocity.normalized;//sdf dfe
                    flockCentre += boid.position;

                    // Add avoidance
                    if (sqrDist < data.avoidanceDist * data.avoidanceDist) {
                        flockSeparation -= delta / sqrDist;
                    }

                }
            }
        }
        if (flockmates != 0) {
            // change values
            avgFlockVelocity = flockVelocity / flockmates;
            avgFlockCentre = flockCentre / flockmates;
            avgFlockSeparation = flockSeparation / flockmates;
        }
    }

    public void UpdateBoid(float timestep) {
        acceleration = Vector2.zero;

        CheckOutofBounds(data.arenaRadius);

        acceleration += SteerTowards(avgFlockVelocity);             //Alignment
        acceleration += SteerTowards(avgFlockCentre - position);    //Cohesion
        acceleration += SteerTowards(avgFlockSeparation) * 2;           //Avoidance



        velocity += acceleration * timestep;

        float magnitude = velocity.magnitude;
        Vector3 dir = velocity.normalized;
        magnitude = Mathf.Clamp(magnitude, data.minSpeed, data.maxSpeed);

        velocity = dir * magnitude;
        position += velocity * timestep;




        //acceleration += Align().normalized;
        //transform.Translate(velocity * timestep);
        // transform.right = velocity.normalized;
    }



}
