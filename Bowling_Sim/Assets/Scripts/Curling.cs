using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curling : MonoBehaviour
{


    //Camera
    public Camera BallCamera;
    public Camera SideCamera;


    public CharacterController controller;
    public Transform Groundcheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    public float Force = 10f;
    public float Gravity = -9.82f;
    public float mass = 8f;
    private float Friction;
    private float counter = 0;
    private float radius = 0.105f;

    private float Time_step = 0.02f;
    private float a = 0.0f;
    private float v = 0.0f;
    private float p = 0.0f;

    //Get the friction from the floor
    private static float Fric_cof;

    //used to check if the simulation should start
    private bool holdingBall = true;

    public Rigidbody rb;

    Vector3 Velocity;

    Vector3 Rotation;

    public static float w = 0f;
    public static float alpha = 0f;
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


        //counter = integer, timestep*50 = 1s, 5= 5s
        if (counter > Time_step * 50 * 5)
        {
            Force = 0;
        }

        
        


     




     

        if (holdingBall == false)
        {

            counter = counter + 1;

            //EULER//
            a = (1 / mass) * (Force - v * Fric_cof);

            v = v + Time_step * a;

            p = p + Time_step * v;

            if (a < 0.00001)
            {
                a = 0;
            }



            Debug.Log(a);


            //Force
            //  Force = Force + Friction * 0.01f; //test to see if the deacceleration works (It does)
            // Debug.Log(Force);
            // Moves the ball on z-axis
            //  transform.Translate(0, 0, Force * Time.fixedDeltaTime, Space.World);
            transform.position = new Vector3(0, 0.11f, p);


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
    public void AdjustForce(float newForce)
    {
        Force = newForce;
    }
    public void StartSimulation(bool Start)
    {
        holdingBall = Start;
    }

}
