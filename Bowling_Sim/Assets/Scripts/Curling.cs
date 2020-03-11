using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Curling : MonoBehaviour
{

    public GameObject StonePrefab;
    public float respawn = 1.0f;
    public bool Spawner = false;

    //these 2 are only in use after collision
    public float x2 = 14; //position of second stone
    public float x1 = 0f;

    //Rotation variabler
    public float angAcc = 0f;
    public float angVel = 0f;
    public float theta = 0f;
    public float inertia = 0f;
    public float rotFriction = 0f;
    public float fw = 0.1f;
    public float torque;
    public float radToDegrees = 57.2957795f;
    public float maxAngVel = 0f;
    public float rotForce = 0f;



    //Camera


    public CharacterController controller;
    public Transform Groundcheck;
    public GameObject Brush;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool collided;
    bool isGrounded;

    public static float Force = 28f; //initial force
    public float Gravity =  9.82f;
    public float mass = 20f; //Kg
    private float Friction;
    private float counter = 0;
    private float radius = 0.16f;
    public static float Angle = 0;

    private float Time_step = 0.02f;
    private float ax = 0.00000f;
    private float ay = 0.0f;
    public float vx = 0.0f;
    public float vy = 0.0f;
    public float px=0.0f;
    public float py = 0.0f;
    public float speed_check = 0.0f;
    public  float inAngle;




    private float iforcex;
    private float iforcey;

    private float anglex;
    private float angley;

    private float frictionx ;
    private float frictiony;


    //Get the friction from the floor
    private static float Fric_cof = 0.02f;

    //used to check if the simulation should start
    private static bool holdingBall = true;

    GameObject referenceObject;
    Follow_Script referenceScript;

    GameObject referenceObject2;
    Brush_Follow referenceScript2;



    Vector3 Velocity;

    Vector3 Rotation;

    public static float w = 0f;
    public static float alpha = 0f;
    // Start is called before the first frame update
    void Start()
    {
  

        

        //Calculate alpha
        alpha = (5 / 2) * (Fric_cof * -Gravity) / radius;

        inertia = mass * (radius * radius);
        rotForce = Force;





    }


    // Update is called once per frame
    void FixedUpdate()
    {

        inAngle = Angle * 0.0174532925f;
        //Check if grounded
       // isGrounded = Physics.CheckSphere(Groundcheck.position, groundDistance, groundMask);

        iforcex = Force * Mathf.Cos(inAngle);
        iforcey = Force * Mathf.Sin(inAngle);

        

        anglex = Mathf.Cos(inAngle);
        angley = Mathf.Sin(inAngle);


        //if (Input.GetKeyDown(KeyCode.R))
        //{

        //    Spawner = true;
        //}
        //else
        //{
        //    Spawner = false;
        //}
        StartCoroutine(StartStone());


        if (px>5)
        {
            iforcex = 0;
            iforcey = 0;
            rotForce = 0;
        }



        //if (vx==0&& px > 2)
        //{
        //    Destroy(this.gameObject);
        //}






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
       

            //Debug.Log(px);

            frictionx = anglex * (mass * Gravity * Fric_cof);
            frictiony = angley * (mass * Gravity * Fric_cof);
            counter = counter + Time_step;


            //EULER//




            ax = (1 / mass) * (iforcex - frictionx);
            ay = (1 / mass) * (iforcey + frictiony + rotFriction); //

            Debug.Log(ax);





            vx = vx + Time_step * ax;
            vy = vy + Time_step * ay;


            if (vx <= 0.1 && px > 2)
            {
                vx = 0;
                vy = 0;
              //  angVel = 0;
            }


            px = px + Time_step * vx;
            py = py + Time_step * vy;
            // Debug.Log(px);
            //Debug.Log(py);

            torque = ((rotForce * radius) - (fw * angVel));

            angAcc = torque / inertia;
            angVel = angVel + angAcc * Time_step;
            theta = theta + angVel * Time_step;

            //Debug.Log(rotForce);
            //Debug.Log("Inertia" + inertia);
            //Debug.Log("Torque" + torque);
            //Debug.Log("Acc" + angAcc);
            //Debug.Log("Vel" + angVel);
            //Debug.Log("Angle" + theta);

            if (angVel / (2 * Mathf.PI) >= maxAngVel)
            {
                maxAngVel = angVel / (2 * Mathf.PI);
                //Debug.Log("maxAngVel " + maxAngVel);
            }

            if (px > 8 && vx > 0.5 && vx < 1.5)
            {
                rotFriction = -3 * maxAngVel * fw;
               // Debug.Log("Friction");
            }
            if (px > 8 && vx < 0.5)
            {
                rotFriction = 0;
                //Debug.Log("Ingen Friction");
            }

            transform.position = new Vector3(py, 0.11f, px);
            if (px <= 2)
            {
                transform.RotateAround(transform.position, transform.up, -0.5f);
            }
            if (px > 2)
            {
                transform.RotateAround(transform.position, transform.up, Time_step * angAcc * radToDegrees);
            }



            // transform.position = new Vector3(py, 0.11f, px);






        }



    }

    //Funktioner som hanterar Settingsmenyn
    public void AdjustForce(float newForce)
    {
        Force = newForce;

        rotForce = newForce;
    }
    public void AdjustAngle(float newAngle)
    {
        Angle = newAngle;
    }
    public void StartSimulation(bool Start)
    {
        holdingBall = Start;
    }

    //Lägger till en ny sten
    private void spawnStone()
    {
        //Skapar en klon av stenen
        GameObject a = Instantiate(StonePrefab) as GameObject;
        //GameObject a = new GameObject("Curling");
        a.transform.position = new Vector3(0, 0, 0);
        Debug.Log("Ny sten skapad");

        //Ändrar kameran så den följer den nya stenen
        referenceObject = GameObject.FindGameObjectWithTag("MainCamera");
        referenceScript = referenceObject.GetComponent<Follow_Script>();
        referenceScript.Ball = a;

        referenceObject2 = GameObject.FindGameObjectWithTag("Cylinder");
        referenceScript2 = referenceObject2.GetComponent<Brush_Follow>();
        referenceScript2.Ball = a;

        a.layer = 9; 


    }
    // Update is called once per frame
    IEnumerator StartStone()
    {
        while (Spawner)
        {
            //väntar "respawn" antal sekunder innan nya stenen skapas
            yield return new WaitForSeconds(respawn);
            spawnStone();

        }

    }


}




