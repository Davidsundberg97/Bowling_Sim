﻿using System;
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

    public Rigidbody rb;

    Vector3 Velocity;

    Vector3 Rotation;
    // Start is called before the first frame update
    void Start()
    {
        BallCamera.enabled = true;
        SideCamera.enabled = false;

        //Enable changing the mass
        rb = GetComponent<Rigidbody>();
        rb.mass = mass;

        //Friction
        Fric_cof = Floor_Script.Friction;
         Debug.Log(Fric_cof);

        //Calculate friction
        Friction = mass * Gravity * Fric_cof;
        Debug.Log(Friction);
    }

    // Update is called once per frame
    void Update()
    {

        //Check if grounded
        isGrounded = Physics.CheckSphere(Groundcheck.position, groundDistance, groundMask);

        //Debug.Log(isGrounded);

        //startar Simulationen, holdingBall variabeln sätts till false i en funktion längst ner i koden
        if(holdingBall == false)
        {
            //Gravity
            Velocity.y += Gravity * Time.deltaTime;
            controller.Move(Velocity * Time.deltaTime);

            if (isGrounded)
            {
                //Force
                Force = Force + Friction * 0.01f; //test to see if the deacceleration works (It does)
                Debug.Log(Force);
                Vector3 move = transform.forward * Force; // add friction here
                controller.Move(move * Time.deltaTime);

                Rotation.x += radius * 2;

                transform.Rotate(Rotation);






                //Debug.Log(move * Time.deltaTime);
            }
            else
            {
                //Force = Force + Friction * 0.01f; //test to see if the deacceleration works (It does)
                //Debug.Log(Force);
                Vector3 move = transform.forward * Force; // add friction here
                controller.Move(move * Time.deltaTime);

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


//funktioner som tillhör SettingsEditor
// Eventuellt lägga in i eget script?
    }
    //forceSlider
    public void AdjustForce(float newForce){
      Force = newForce;
    }
    //startButton
    public void StartSimulation(bool Start){
      holdingBall = false;

    }
}
