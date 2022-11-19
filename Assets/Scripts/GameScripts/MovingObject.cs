using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingObject : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 moveDir;

    private Rigidbody2D rb;
    private bool moving;
    [SerializeField] private GameObject cam_ref;
    [SerializeField] private float destroy_dist;

    public bool can_adjust;

    [Header("Raycast Config")]
    public bool useRay;
    
    public Vector3 offset;
    public Vector2 dir;
    public float dist;

    public LayerMask target_layers;

    private void Awake()
    {      
        rb = GetComponent<Rigidbody2D>();
        moving = true;
        can_adjust = true;
    }

    private void Start()
    {
        cam_ref = Camera.main.gameObject;
        rb.velocity = moveDir * moveSpeed;

        FixPlatformPos();
    }

    private void Update()
    {
        if(transform.position.x < cam_ref.transform.position.x - destroy_dist)
        {
            rb.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }

        if(rb.velocity == Vector2.zero && moving)
        {
            rb.velocity = moveDir * moveSpeed;
        }
    
        if(GameManager.gameManager.playerDead && moving)
        {
            moving = false;
            rb.velocity = Vector2.zero;
        }       
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 display_dir = new Vector3(dir.x, dir.y, 0f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + offset, transform.position + display_dir * dist + offset);
    }

    // função para corrigir o mal posicionamento das plataformas e armadilhas
    // infelizmente é uma gambiarra
    public void FixPlatformPos()
    {
        if (useRay)
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position + offset, dir, dist, target_layers);

            if (ray.collider != null)
            {
                if (ray.collider.gameObject.layer == LayerMask.NameToLayer("Scenario") || ray.collider.gameObject.layer == LayerMask.NameToLayer("Traps"))
                {
                    GameObject obj = ray.collider.gameObject;
                    if (obj.GetComponent<MovingObject>().can_adjust)
                    {
                        obj.transform.position = new Vector3(transform.position.x, obj.transform.position.y, 0f);
                        obj.GetComponent<MovingObject>().can_adjust = false;
                    }                   
                }
            }
        }
    }
}
