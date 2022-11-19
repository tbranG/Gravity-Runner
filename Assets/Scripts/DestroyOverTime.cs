using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float seconds;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("kill", seconds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void kill()
    {
        Destroy(gameObject);
    }
}
