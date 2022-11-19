using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIntersection : MonoBehaviour
{
    public Vector3 offset;
    public Vector2 dir;
    public float dist;

    public LayerMask target_layers;

    private void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position + offset, dir, dist, target_layers);
        
        if (ray.collider != null)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 display_dir = new Vector3(dir.x, dir.y, 0f);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + offset, transform.position + display_dir * dist + offset);
    }
}
