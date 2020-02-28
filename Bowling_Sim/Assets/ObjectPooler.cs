using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    #region Singleton

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> pooldic;
    // Start is called before the first frame update
    void Start()
    {
        pooldic = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            pooldic.Add(pool.tag, objectPool);
        }
    }

 public GameObject SpawnFromPool ( string tag, Vector3 position, Quaternion rotation)
    {
        if (!pooldic.ContainsKey(tag))
        {
            Debug.LogWarning(tag + "doesnt exist");
            return null;

        }

       

        GameObject objectToSpawn = pooldic[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        pooldic[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
