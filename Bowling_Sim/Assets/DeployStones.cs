using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployStones : MonoBehaviour
{
    public GameObject StonePrefab;
    public float respawn = 1.0f;
    public bool Spawner = false;
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
        a.transform.position = new Vector3(0, 0, 0);
        Debug.Log("hello");

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

