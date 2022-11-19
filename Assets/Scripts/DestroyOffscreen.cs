using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffscreen : MonoBehaviour
{
    private GameObject cam;
    public float max_dist;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.gameObject;
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, cam.transform.position) >= max_dist)
        {
            if (gameObject.CompareTag("Player"))
            {
                gameObject.GetComponent<PlayerBehavior>().kill();
            } else
            {
                Destroy(gameObject);
            }        
        }
    }
}
