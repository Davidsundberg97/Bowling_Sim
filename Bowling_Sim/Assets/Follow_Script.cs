using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Script : MonoBehaviour
{
    public GameObject Ball;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - Ball.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float newXPosition = Ball.transform.position.x + offset.x;
        float newyPosition = Ball.transform.position.y + offset.y;
        float newzPosition = Ball.transform.position.z + offset.z;

        transform.position = new Vector3(newXPosition, newyPosition, newzPosition);

    }
}
