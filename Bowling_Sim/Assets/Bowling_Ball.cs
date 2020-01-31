using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//If the ball moves on it´s own delete the second horizontal and vertical
public class Bowling_Ball : MonoBehaviour
{

    //Camera
    public Camera BallCamera;
    public Camera SideCamera;


    public CharacterController controller;

    public float Force = 5f;
    public float Gravity = -9.82f;

    //used to check if the simulation should start
    private bool holdingBall = true;

    public Transform Camera;

    Vector3 Velocity;
    // Start is called before the first frame update
    void Start()
    {
        BallCamera.enabled = true;
        SideCamera.enabled = false;
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            holdingBall = false;
        }

        if(holdingBall == false)
        {
            //Gravity
            Velocity.y += Gravity * Time.deltaTime;
            controller.Move(Velocity * Time.deltaTime);


            //Force
           
            Vector3 move = transform.forward * Force;
            controller.Move(move * Time.deltaTime);


        }






        if (Input.GetKeyDown(KeyCode.C))
        {
            BallCamera.enabled = !BallCamera.enabled;
            SideCamera.enabled = !SideCamera.enabled;
        }

    }


}
