using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public bool active;

    public GameObject[] objsToSpawn;
    public List<GameObject> objsLoaded;

    public int min_time;
    public int max_time;

    private bool canSpawn;
    private GameObject spawnObj;

    public float offset_strength;

    // Start is called before the first frame update
    void Awake()
    {
        canSpawn = true;
        objsLoaded = new List<GameObject>();

        InitialSpawn(6);
        spawnObj = GetSpawnObj();

        SpawnObj();
    }

    void InitialSpawn(int quant)
    {
        for(int i = 0; i < objsToSpawn.Length; i++)
        {         
            for(int x = 0; x < quant; x++)
            {
                GameObject instance = Instantiate(objsToSpawn[i], transform.position, Quaternion.identity);
                instance.SetActive(false);
                objsLoaded.Add(instance);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (active)
        //{
        //    if (canSpawn)
        //    {
        //        SpawnObj();
        //        canSpawn = false;
        //    }
        //}
    }

    int GetRandomTime() 
    {
        int x;
        x = Random.Range(min_time, max_time);

        return x;
    }

    GameObject GetRandomObj()
    {
        GameObject obj;
        
        int list_index;
        list_index = Random.Range(0, objsLoaded.Count);
     
        obj = objsLoaded[list_index];

        while (obj == null)
        {
            objsLoaded.Remove(obj);
            
            int new_list_index;
            new_list_index = Random.Range(0, objsLoaded.Count);

            obj = objsLoaded[new_list_index];
        }

        while (obj.activeSelf)
        {
            int new_list_index;
            new_list_index = Random.Range(0, objsLoaded.Count);

            obj = objsLoaded[new_list_index];
        }

        return obj;
    }

    GameObject GetSpawnObj()
    {
        GameObject obj_to_spawn = GetRandomObj();
        return obj_to_spawn;
    }

    private void SpawnObj()
    {

        spawnObj.transform.position = new Vector3(transform.position.x + Time.deltaTime * offset_strength, transform.position.y, 0f);
        spawnObj.SetActive(true);
        spawnObj.GetComponent<MovingObject>().can_adjust = true;
        spawnObj.GetComponent<MovingObject>().FixPlatformPos();

        StartCoroutine(SpawnCooldown(GetRandomTime()));
        spawnObj = GetSpawnObj();
    }

    IEnumerator SpawnCooldown(int time_to_spawn)
    {
        canSpawn = false;
        yield return new WaitForSeconds(time_to_spawn);        
        canSpawn = true;
        SpawnObj();
    }

}
