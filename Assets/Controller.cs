using System.Collections;
using System.Collections.Generic;
using UnityEngine;
static class c
{
    public const float arenaRadius = 85;

}
public class Controller : MonoBehaviour
{


    public static Controller ins;

    private void Awake() {
        ins = this;
    }



    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnDrawGizmos() {


        UnityEditor.Handles.color = Color.black * 1f;

       // UnityEditor.Handles.DrawWireArc(transform.position, Vector3.back, Vector3.up, 360, arenaRadius);

    }


}
