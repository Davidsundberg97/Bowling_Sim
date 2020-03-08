using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public float vx_off_current_stone;
    public float vx_off_collided_stone;
    public bool collided = false;
    public GameObject other2;
    public float vx_efter;
    public float vx2_efter;

    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("Collision Detected");
        if(other.gameObject.GetComponent<Curling>().px > 1)
        {

            Debug.Log(gameObject.GetComponent<Curling>().px);



            // other.gameObject.GetComponent<Curling>().vx = 1f; //the Stone that is Sent DET HÄR FUNKAR
            // gameObject.GetComponent<Curling>().vx = 0.5f; // the object that gets collided with DET HÄR FUNKAR
            // gameObject.GetComponent<Curling>().collided = true;
            // other.gameObject.GetComponent<Curling>().collided = true;
            // other2 = other.gameObject;

            //float pos1 =  gameObject.GetComponent<Curling>().x1;
            //float pos2 = gameObject.GetComponent<Curling>().x2;

            gameObject.GetComponent<Curling>().vx = vx_efter;
            other.gameObject.GetComponent<Curling>().vx = vx2_efter;




        }
       // other.gameObject.GetComponent<Curling>().vx = 10f;
    }

  
}
