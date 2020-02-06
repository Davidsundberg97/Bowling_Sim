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
    public float mass;
    private double Friction;

    //Get the friction from the floor
    private static double Fric_cof;

    //used to check if the simulation should start
    private bool holdingBall = true;

    public Rigidbody rb;

    Vector3 Velocity;
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
        //Make it so that you have to press Left mousebutton to start.
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
           
            Vector3 move = transform.forward * Force; // add friction here
            controller.Move(move * Time.deltaTime);


        }






        if (Input.GetKeyDown(KeyCode.C))
        {
            BallCamera.enabled = !BallCamera.enabled;
            SideCamera.enabled = !SideCamera.enabled;
        }

    }


}
