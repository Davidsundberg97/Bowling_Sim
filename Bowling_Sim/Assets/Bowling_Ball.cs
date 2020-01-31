using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowling_Ball : MonoBehaviour
{
    public CharacterController controller;

    public float Force = 5f;
    public float Gravity = -9.82f;

    public GameObject Ball;

    Vector3 Velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Gravity
        Velocity.y += Gravity * Time.deltaTime;
        controller.Move(Velocity * Time.deltaTime);


        //Force
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * z;
        controller.Move(move * Force * Time.deltaTime);

   
       
    }


}
