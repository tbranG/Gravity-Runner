using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public bool godmode;
    public GameObject death_effect;
   
    public void kill()
    {
        if (!godmode)
        {
            GameObject part = Instantiate(death_effect, transform.position, Quaternion.identity);
            part.GetComponentInChildren<AudioSource>().Play();
            Destroy(gameObject);
        }      
    }
}
