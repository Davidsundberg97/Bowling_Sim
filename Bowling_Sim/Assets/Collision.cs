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
    public float vy_efter;
    public float vy2_efter;
    public float x1; //stenen närmast
    public float x2; //stenen längst ifrån
    public float y1;
    public float y2;
    public float v1;
    public float v2;
    public float v1x;
    public float v2x;
    public float v1y;
    public float v2y;
    public int counter = 0;

    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("Collision Detected");
        if(other.gameObject.GetComponent<Curling>().px > 1 && counter == 0)
        {

            counter = counter + 1;

            if (other.gameObject.GetComponent<Curling>().px > gameObject.GetComponent<Curling>().px)
            {
                x2 = other.gameObject.GetComponent<Curling>().px;
                x1 = gameObject.GetComponent<Curling>().px;
                y1 = other.gameObject.GetComponent<Curling>().py;
                y2 = gameObject.GetComponent<Curling>().py;

                v2x = other.gameObject.GetComponent<Curling>().vx;
                v2y = other.gameObject.GetComponent<Curling>().vy;
                v1x = gameObject.GetComponent<Curling>().vx;
                v1y = gameObject.GetComponent<Curling>().vy;


                float dy = y2 - y1;
                float dx = x2 - x1;

                float angle = Mathf.Atan2(dy, dx);
                float inAngle = gameObject.GetComponent<Curling>().inAngle;

                v1 = Mathf.Sqrt(v1x * v1x + v1y * v1y);
                v2 = Mathf.Sqrt(v2x * v2x + v2y * v2y);



                Debug.Log(v1 + " v1 " + v2 + " v2 ");


                float angle2 = 0;

                vx_efter = v2 * Mathf.Cos(angle2 - angle) * Mathf.Cos(angle) + (v1 * Mathf.Sin(inAngle - angle) * Mathf.Cos(angle + (Mathf.PI / 2)));
                vy_efter = v2 * Mathf.Cos(angle2 - angle) * Mathf.Sin(angle) + (v1 * Mathf.Sin(inAngle - angle) * Mathf.Sin(angle + (Mathf.PI / 2)));



                vx2_efter = v1 * Mathf.Cos(0) * Mathf.Cos(0) + v2 * Mathf.Sin(0) * Mathf.Cos(0 + Mathf.PI / 2);
                vy2_efter = v1 * Mathf.Cos(angle2 - angle) * Mathf.Sin(angle) + v2 * Mathf.Sin(inAngle - angle) * Mathf.Sin(angle + Mathf.PI / 2);







                other.gameObject.GetComponent<Curling>().vx = vx_efter;
                other.gameObject.GetComponent<Curling>().vy = vy_efter;

                gameObject.GetComponent<Curling>().vx = vx2_efter;
                gameObject.GetComponent<Curling>().vy = vy2_efter;



                Debug.Log("Furthest away is " +x2);
                Debug.Log("closest is " + x1);

            }
            else
            {
                //this is the correct one
                x1 = other.gameObject.GetComponent<Curling>().px;
                x2 = gameObject.GetComponent<Curling>().px;
                y1 = other.gameObject.GetComponent<Curling>().py;
                y2 = gameObject.GetComponent<Curling>().py;

                //Debug.Log("Furthest away is " + x2);
                //Debug.Log("We are here");
                //Debug.Log("closest is " + x1);

                Debug.Log("Hej");



                v2x = gameObject.GetComponent<Curling>().vx;
                v2y = gameObject.GetComponent<Curling>().vy;
                v1x = other.gameObject.GetComponent<Curling>().vx;
                v1y = other.gameObject.GetComponent<Curling>().vy;


                float dy = y2 - y1;
                float dx = x2 - x1;

                float angle = Mathf.Atan2(dy, dx);
                float inAngle = gameObject.GetComponent<Curling>().inAngle;

                v1 = Mathf.Sqrt(v1x * v1x + v1y * v1y);
                v2 = Mathf.Sqrt(v2x * v2x + v2y * v2y);

              

                Debug.Log(v1 + " v1 " + v2 + " v2 ");
                

                float angle2 = 0;

                vx_efter = v2 * Mathf.Cos(angle2- angle) * Mathf.Cos(angle) + (v1 * Mathf.Sin(inAngle-angle) * Mathf.Cos(angle + (Mathf.PI / 2)));
                vy_efter = v2 * Mathf.Cos(angle2 - angle) * Mathf.Sin(angle) + (v1 * Mathf.Sin(inAngle - angle) * Mathf.Sin(angle + (Mathf.PI / 2)));



                vx2_efter = v1 * Mathf.Cos(0) * Mathf.Cos(0) + v2 * Mathf.Sin(0) * Mathf.Cos(0 + Mathf.PI / 2);
                vy2_efter = v1 * Mathf.Cos(angle2 - angle) * Mathf.Sin(angle) + v2 * Mathf.Sin(inAngle - angle) * Mathf.Sin(angle + Mathf.PI / 2);



           



                other.gameObject.GetComponent<Curling>().vx = vx_efter;
                other.gameObject.GetComponent<Curling>().vy = vy_efter;

                gameObject.GetComponent<Curling>().vx = vx2_efter;
                gameObject.GetComponent<Curling>().vy = vy2_efter;

            }
          

            // other.gameObject.GetComponent<Curling>().vx = 1f; //the Stone that is Sent DET HÄR FUNKAR
            // gameObject.GetComponent<Curling>().vx = 0.5f; // the object that gets collided with DET HÄR FUNKAR
            // gameObject.GetComponent<Curling>().collided = true;
            // other.gameObject.GetComponent<Curling>().collided = true;
            // other2 = other.gameObject;

            //float pos1 =  gameObject.GetComponent<Curling>().x1;
            //float pos2 = gameObject.GetComponent<Curling>().x2;

           




        }
       // other.gameObject.GetComponent<Curling>().vx = 10f;
    }

    private void OnTriggerExit(Collider other)
    {
        counter = 0;
    }


}
