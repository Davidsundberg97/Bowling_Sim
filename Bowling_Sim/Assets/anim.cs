using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour
{
    public Animator animator;
    public GameObject Brush;
   
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        ////måste sätta vx o px till static om de ska funka
        //position = Curling.px;
        //Debug.Log(speed);
    
        //If V = 0 animation should not be possible and object should be destroyed
        if (Input.GetKey(KeyCode.B))
        {
            animator.SetBool("Polishing", true);
            Debug.Log("BrushBrsuh");
            
        }
        else
        {
            animator.SetBool("Polishing", false);
        }
    }
}
