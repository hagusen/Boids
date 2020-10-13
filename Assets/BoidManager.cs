using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// obstacle avoidance??
// add my static fake cirle collisions?

// Raycast on every boid



public class BoidManager : MonoBehaviour
{
    public GameObject prefab;

    public Settings data;
    public int numBoids = 100;

    public Boid[] boids;
    public List<Transform> activeBoids = new List<Transform>();

    void Start() {

        boids = new Boid[numBoids];
        for (int i = 0; i < boids.Length; i++) {
            boids[i] = new Boid(new Vector2(Random.value - .5f, Random.value - .5f), new Vector2(Random.value-.5f, Random.value - .5f) * 60, data);
        }

        for (int i = 0; i < boids.Length; i++) {

            var b = Instantiate(prefab).transform;
            b.position = boids[i].position;
            b.right = boids[i].velocity.normalized;

            activeBoids.Add(b);
        }

    }


    void Update() {


        foreach (var boid in boids) {

            boid.CheckVision(boids);
            boid.UpdateBoid(Time.deltaTime);

        


        }

        for (int i = 0; i < boids.Length; i++) {


            activeBoids[i].position = boids[i].position;
            activeBoids[i].right = boids[i].velocity.normalized;


        }


    }

    private void OnDrawGizmos() {


        UnityEditor.Handles.color = Color.black * 1f;

        UnityEditor.Handles.DrawWireArc(transform.position, Vector3.back, Vector3.up, 360, data.arenaRadius);

    }



}
[System.Serializable]
public class Settings
{
    public float maxSpeed;
    public float minSpeed;
    public float maxSteer;


    public float visionDist;
    public float avoidanceDist;

    public float arenaRadius;
}
