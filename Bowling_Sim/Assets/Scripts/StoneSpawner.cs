using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    ObjectPooler objectpooler;

    private void Start()
    {
        objectpooler = ObjectPooler.Instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R))
        {
            objectpooler.SpawnFromPool("Stone", transform.position, Quaternion.identity);
        }
        

    }
}
