using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Curling : MonoBehaviour
{

    public GameObject StonePrefab;
    public float respawn = 1.0f;
    public bool Spawner = false;


    //Camera


    public CharacterController controller;
    public Transform Groundcheck;
    public GameObject Brush;
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
    public float speed_check = 0.0f;
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
    private static bool holdingBall = true;

    GameObject referenceObject;
    Follow_Script referenceScript;



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
        //Check if grounded
        isGrounded = Physics.CheckSphere(Groundcheck.position, groundDistance, groundMask);


        if (Input.GetKeyDown(KeyCode.R))
        {

            Spawner = true;
        }
        else
        {
            Spawner = false;
        }
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



           frictionx = anglex * (mass * Gravity * Fric_cof);
            frictiony = angley * (mass * Gravity * Fric_cof);
            counter = counter + Time_step;
         

            //EULER//


            ax = (1 / mass) * (iforcex + frictionx);
            ay = (1 / mass) * (iforcey - frictiony);
            //Debug.Log(((ax)));
            

            vx = vx + Time_step * ax;
            vy = vy + Time_step * ay;

            speed_check = vx;
           // Debug.Log(((speed_check)));

            if (vx < 0){
                vx = 0;
            }
                 

            px = px + Time_step * vx;
            py = py + Time_step * vy;


            transform.position = new Vector3(py, 0.11f, px);


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

    //Lägger till en ny sten
    private void spawnStone()
    {
        //Skapar en klon av stenen
        GameObject a = Instantiate(StonePrefab) as GameObject;
        a.transform.position = new Vector3(0, 0, 0);
        Debug.Log("Ny sten skapad");
        //Ändrar kameran så den följer den nya stenen
        referenceObject = GameObject.FindGameObjectWithTag("MainCamera");
        referenceScript = referenceObject.GetComponent<Follow_Script>();
        referenceScript.Ball = a;


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




