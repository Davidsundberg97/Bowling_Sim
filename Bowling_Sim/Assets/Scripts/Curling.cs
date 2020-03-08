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
    private float radius = 0.105f;
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

      

        
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        inAngle = Angle * 0.0174532925f;
        //Check if grounded
        isGrounded = Physics.CheckSphere(Groundcheck.position, groundDistance, groundMask);

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


        if (px>2)
        {
            iforcex = 0;
            iforcey = 0;
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
            //------------------------Collision----------------------------------//
            if (collided == true) //kollar om utträkningar för kollision ska göras
            {
             //   Debug.Log("Collision check");


             //   float frictionx1 = (mass * Gravity * Fric_cof);

             //   float v1 = vx; //v1=sqrt(v1x^2+v1y^2);

             //   float v2 = 0f;

             //   float v1x_efter = v2 * Mathf.Cos(0) * Mathf.Cos(0) + (v1 * Mathf.Sin(inAngle) * Mathf.Cos(0 + (Mathf.PI / 2)));

             //   float v2x_efter = v1 * Mathf.Cos(0) * Mathf.Cos(0) + v2 * Mathf.Sin(0) * Mathf.Cos(Mathf.PI / 2);

             //  float a1x = (1 / mass) * (-frictionx1 * v1x_efter);
                

             //float   a2x = (1 / mass) * (-frictionx1 * v2x_efter);



             //   v1x_efter = v1x_efter + Time_step * a1x;
                
             //   v2x_efter = v2x_efter + Time_step * a2x;

             //   x1 = px + Time_step * v1x_efter;
                

             //   x2 = x2 + Time_step * v2x_efter;

             //   transform.position = new Vector3(0, 0.11f, x1);
             //   GameObject stone2 = GetComponent<Collision>().other2; //kan bli error pågrund av Fixedupdate skre förre OnTrigger i Collision
             //   stone2.transform.position = new Vector3(0, 0.11f, x2);





             //   //räkna ut v1x efter och v2x (Kan göras i Collision ?) använd dem 1 gång
             //   //a1x = (1/mass)*(-frictionx*v1x) och a2x

             //   //v1x = v1x +Time_step * a1x;
             //   //Kolla så den att kraften inte gör hastigheten ändrar sig (if(vx<0) vx=0)

             //   //px1 = px1 + Time_Step * v1;
             //   //px2 = px2 + Time_Step * v2;

             //   //send px1 and px2 to collision and add them to other.transform.position(px2,py2,0);
            }
            else //-------------------------Before Collision-------------------//
            {

                
            }


            frictionx = anglex * (mass * Gravity * Fric_cof);
            frictiony = angley * (mass * Gravity * Fric_cof);
            counter = counter + Time_step;


            //EULER//


            ax = (1 / mass) * (iforcex + frictionx);
            ay = (1 / mass) * (iforcey + frictiony);

            



            vx = vx + Time_step * ax;
            vy = vy + Time_step * ay;


            if (vx < 0)
            {
                vx = 0;
                vy = 0;
            }


            px = px + Time_step * vx;
            py = py + Time_step * vy;
            //Debug.Log(px);
            //Debug.Log(py);


            transform.position = new Vector3(py, 0.11f, px);






        }

      

    }

    //Funktioner som hanterar Settingsmenyn
    public void AdjustForce(float newForce)
    {
        Force = newForce;
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




