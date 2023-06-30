using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour
{
    public Transform viz;
    public Transform headPos;
    private Vector3[] gravityDir = {Vector3.up, -Vector3.up, -Vector3.right, Vector3.right, Vector3.forward, -Vector3.forward};
    float[] sim = new float[6];
    void Update()
    {
        // Update the gravity in the direction determined by Arrow Keys
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            UpdateGravity(-transform.right);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            UpdateGravity(transform.right);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            UpdateGravity(transform.forward);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            UpdateGravity(-transform.forward);
        }
  
    }

    // Update the gravity in the given direction
    void UpdateGravity(Vector3 dir){

        // Identify the direction in world the player is currently facing
        for (int i = 0; i < 6;   i++)
        {
            sim[i] = Vector3.Dot(dir, gravityDir[i]);
        }

        // Find the new Gravity
        Vector3 newGravity = gravityDir[indexOfMax(sim)];

        // Rotate the player so that the feet are towared the ground 
        transform.rotation = Quaternion.LookRotation(newGravity, transform.up);
        transform.RotateAround(headPos.position, -transform.right, 90f);
        
        // Apply the New Gravity
        Physics.gravity = newGravity*9.8f;
    }

    // Find the Index of max element
    int indexOfMax(float[] arr){
        float max = arr[0];
        int index = 0;
        for (int i = 1; i < arr.Length; i++)
        {
            if (max < arr[i]){
                max = arr[i];
                index = i;
            }
        }
        return index;
    }
}
