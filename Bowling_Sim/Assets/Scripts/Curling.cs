using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public static float Force = 28f; //initial force
    public float Gravity =  9.82f;
    public float mass = 20f; //Kg
    private float Friction;
    private float counter = 0;
    private float radius = 0.105f;

    private float Time_step = 0.02f;
    private float ax = 0.00000f;
    private float ay = 0.0f;
    private float vx = 0.0f;
    private float vy = 0.0f;
    private float px=0.0f;
    private float py = 0.0f;
    private float v = 0.0f;
    private float p = 0.0f;
    private static float inAngle = 0*5*0.0174532925f;




   private float iforcex = Force* Mathf.Cos(inAngle);
    private float iforcey = Force* Mathf.Sin(inAngle);

   private float  anglex = Mathf.Cos(inAngle);
    private float angley = Mathf.Sin(inAngle);

    private float frictionx ;
    private float frictiony;


    //Get the friction from the floor
    private static float Fric_cof = 0.02f;

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
        //Fric_cof = Floor_Script.Friction;
        //Debug.Log(Fric_cof);

        

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
        if (px>2)
        {
            iforcex = 0;
            iforcey = 0;
        }






        if (Input.GetKey(KeyCode.B))
        {
            Fric_cof = 0.01f;
        }
        else
        {
            Fric_cof = 0.02f;
        }




        if (holdingBall == false)
        {



           frictionx = anglex * (mass * Gravity * Fric_cof);
            frictiony = angley * (mass * Gravity * Fric_cof);
            counter = counter + Time_step;
         

            //EULER//
            //a = (1 / mass) * (Force - Mathf.Abs(mass * Gravity * Fric_cof));


            ax = (1 / mass) * (iforcex + frictionx);
            ay = (1 / mass) * (iforcey - frictiony);
            Debug.Log(((ax)));
            

            vx = vx + Time_step * ax;
            vy = vy + Time_step * ay;

            if (vx < 0){
                vx = 0;
            }
                 
          

            px = px + Time_step * vx;
            //Debug.Log(px);
            py = py + Time_step * vy;

          
            


         




            //Debug.Log(a);


            //Force
            //  Force = Force + Friction * 0.01f; //test to see if the deacceleration works (It does)
            // Debug.Log(Force);
            // Moves the ball on z-axis
            //  transform.Translate(0, 0, Force * Time.fixedDeltaTime, Space.World);
            transform.position = new Vector3(py, 0.11f, px);


            //v = v0 - Fric_cof * g * t;


            //Force


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
