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
    public Transform Groundcheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    public float Force = 5f;
    public float Gravity = -9.82f;
    public float mass;
    private float Friction;
    private float radius = 0.105f;

    //Get the friction from the floor
    private static float Fric_cof;

    //used to check if the simulation should start
    private bool holdingBall = true;
    

    Vector3 Velocity;

    Vector3 Rotation;

    public static float w = 0f;
    public static float alpha = 0f;

    // Start is called before the first frame update
    void Start()
    {
        BallCamera.enabled = true;
        SideCamera.enabled = false;

       
        //Friction
        Fric_cof = Floor_Script.Friction;
         Debug.Log(Fric_cof);

        //Calculate friction
        Friction = mass * Gravity * Fric_cof;
        Debug.Log(Friction);

        //Calculate alpha
        alpha = (5 / 2) * (Fric_cof * -Gravity) / radius;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Check if grounded
        isGrounded = Physics.CheckSphere(Groundcheck.position, groundDistance, groundMask);

        //Debug.Log(isGrounded);
        //Make it so that you have to press Left mousebutton to start.

        if(holdingBall == false)
        {
            //Gravity
            Velocity.y += Gravity * Time.fixedDeltaTime;
            controller.Move(Velocity * Time.fixedDeltaTime);


            if (isGrounded && Force > 0)
            {
                //Force
                Force = Force + Friction * 0.01f; //test to see if the deacceleration works (It does)
                // Debug.Log(Force);
                // Moves the ball on z-axis
                transform.Translate(0, 0, Force * Time.fixedDeltaTime, Space.World);

                //Rotates the ball around the x axis
                w = w + alpha * Time.fixedDeltaTime;
                Rotation.x = w;
                transform.Rotate(Rotation);


                



                //OTHER WAY TO MOVE THE BALL (ROTATION MESS IT UP)//
                // Vector3 move = transform.forward * Force; // add friction here
                //controller.Move(move * Time.deltaTime);






                //Debug.Log(move * Time.deltaTime);
            }
            else
            {
                //Force = Force + Friction * 0.01f; //test to see if the deacceleration works (It does)
                //Debug.Log(Force);

                transform.Translate(0, 0, Force * Time.fixedDeltaTime, Space.World);

                //Debug.Log(move * Time.deltaTime);
            }

            //v = v0 - Fric_cof * g * t;


            //Force


            //force


        }






        if (Input.GetKeyDown(KeyCode.C))
        {
            BallCamera.enabled = !BallCamera.enabled;
            SideCamera.enabled = !SideCamera.enabled;
        }

    }
//Funktioner som hanterar Settingsmenyn
    public void AdjustForce(float newForce){
      Force = newForce;
    }
    public void StartSimulation(bool Start){
      holdingBall = Start;
    }

}
