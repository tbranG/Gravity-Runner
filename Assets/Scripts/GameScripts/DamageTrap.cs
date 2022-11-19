using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrap : MonoBehaviour
{
    public string targetTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            player.GetComponent<PlayerBehavior>().kill();
        }        
        else if (collision.gameObject.CompareTag(targetTag))
        {
            Destroy(collision.gameObject);
        }    
    }
}
