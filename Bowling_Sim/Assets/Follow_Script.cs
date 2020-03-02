using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Script : MonoBehaviour
{
    public GameObject Ball;
    Vector3 offset;
    //Camera
    public Camera BallCamera;
    public Camera SideCamera;
    // Start is called before the first frame update

    void Start()
    {
        offset = transform.position - Ball.transform.position;
        BallCamera.enabled = true;
        SideCamera.enabled = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float newXPosition = Ball.transform.position.x + offset.x;
        float newyPosition = Ball.transform.position.y + offset.y;
        float newzPosition = Ball.transform.position.z + offset.z;

        transform.position = new Vector3(newXPosition, newyPosition, newzPosition);
        if (Input.GetKeyDown(KeyCode.C))
        {
            BallCamera.enabled = !BallCamera.enabled;
            SideCamera.enabled = !SideCamera.enabled;
        }

    }
}
