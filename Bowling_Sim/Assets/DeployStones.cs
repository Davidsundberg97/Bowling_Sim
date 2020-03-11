using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployStones : MonoBehaviour
{
    public GameObject StonePrefab;
    public float respawn = 1.0f;
    public bool Spawner = false;

    GameObject referenceObject;
    Follow_Script referenceScript;

    GameObject referenceObject2;
    Brush_Follow referenceScript2;

    // Start is called before the first frame update
    void Start()
    {
       // StartCoroutine(StartStone());
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
          
            Spawner = true;
        }
        else
        {
            Spawner = false;
        }
        StartCoroutine(StartStone());
    }
    private void spawnStone()
    {
        GameObject a = Instantiate(StonePrefab) as GameObject;
        a.transform.position = new Vector3(0, 2, 0);
        Debug.Log("hello");

        //Ändrar kameran så den följer den nya stenen
        referenceObject = GameObject.FindGameObjectWithTag("MainCamera");
        referenceScript = referenceObject.GetComponent<Follow_Script>();
        referenceScript.Ball = a;

        referenceObject2 = GameObject.FindGameObjectWithTag("BrushMover");
        referenceScript2 = referenceObject2.GetComponent<Brush_Follow>();
        referenceScript2.Ball = a;

        a.layer = 9;

    }
    // Update is called once per frame
   IEnumerator StartStone()
    {
        while (Spawner)
        {
            yield return new WaitForSeconds(respawn);
            spawnStone();

        }
           
        }
    }

