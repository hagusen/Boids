using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Accessibility;

public class Boid2 : MonoBehaviour
{
    public Vector2 acceleration;
    public Vector2 velocity;

    public float visionDist;

    public Collider2D[] seenObjects;
    public LayerMask boids;

    public float mainCircleSpaceRadius;


    void Start()
    {
        
    }


    Vector2 SteerTowards(Vector2 v) {

        return v;
    }


    void Update()
    {
        acceleration = Vector2.zero;


        //Update Vision
        seenObjects = Physics2D.OverlapCircleAll(transform.position, visionDist);

        //If outside of the arena
        if (transform.position.magnitude > Controller.ins.arenaRadius) {
            //Go to the middle again
            acceleration += SteerTowards(-transform.position);

        }








        velocity += acceleration * Time.deltaTime;
        acceleration += Align().normalized;

        transform.Translate(velocity * Time.deltaTime);

        transform.right = velocity.normalized;

    }



    Vector2 Align() {
        if (seenObjects.Length <= 1) {
            return Vector2.zero;
        }

        Vector2 avgVelocity = Vector2.zero;

        foreach (var boid in seenObjects) {
            if (boid != this) {
                avgVelocity += boid.GetComponent<Boid2>().velocity;
            }
        }

        avgVelocity = avgVelocity / (seenObjects.Length - 1);

        return avgVelocity - velocity;
    }

    private void OnDrawGizmos() {

        //Gizmos.DrawWireSphere(transform.position, visionDist);
        UnityEditor.Handles.color = Color.black * 0.3f;
        UnityEditor.Handles.DrawWireArc(transform.position, Vector3.back, Vector3.up, 360, visionDist);

        foreach (var obj in seenObjects) {
            Gizmos.color = Color.green;
            //Gizmos.DrawRay(transform.position,transform.position - obj.transform.position.normalized);
            Gizmos.DrawLine(transform.position, obj.transform.position);
            UnityEditor.Handles.color = Color.black * 0.3f;
        }
    }



}
