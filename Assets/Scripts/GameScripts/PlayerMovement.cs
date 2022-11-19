using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bool iGravity;

    [SerializeField] private bool can_dash;
    private bool dashing;
    private Rigidbody2D body;

    [Header("Dash Control")]
    public AudioSource dashAudio;
    public AudioSource invertAudio;
    public float dash_time;
    public float dash_cooldown;
    public GameObject dash_effect;

    private float current_grav;

    [Header("Animations")]
    public Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        iGravity = false;
        can_dash = true;
        dashing = false;
        
        body = GetComponent<Rigidbody2D>();
        current_grav = body.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {       

        if (Input.GetKeyDown(KeyCode.Space) && !dashing)
        {
            InvertGravity();
        }else if(Input.GetKeyDown(KeyCode.Space) && dashing)
        {
            InvertGravityDash();
        }

        // mecânica do dash
        if (Input.GetKeyDown(KeyCode.D) && can_dash)
        {
            StartCoroutine(Dashing());
        }
    }

    void SpriteCorrection()
    {
        if(anim != null)
        {
            bool rotate = iGravity == true ? true : false;
            //transform.rotation = rotate ? Quaternion.Euler(180f, 0f, 0f) : Quaternion.Euler(0f, 0f, 0f);

            string anim_name = rotate == true ? "FlipUp" : "FlipDown";
            anim.Play(anim_name);
        }   
    }

    public void InvertGravity()
    {
        iGravity = !iGravity;

        body.velocity = Vector2.zero;
        body.gravityScale *= -1f;
        current_grav = body.gravityScale;

        SpriteCorrection();

        invertAudio.Play();
    }

    public void InvertGravityDash()
    {
        iGravity = !iGravity;
        current_grav *= -1f;

        SpriteCorrection();
    }

    public IEnumerator Dashing()
    {
        can_dash = false;

        body.gravityScale = 0f;
        body.velocity = Vector2.zero;
        dashing = true;

        dashAudio.Play();

        GameObject trail = Instantiate(dash_effect, transform.position, Quaternion.identity);
        trail.transform.parent = gameObject.transform;

        if (iGravity)
            trail.GetComponent<ParticleSystemRenderer>().flip = new Vector3(0f, 1f);

        if (GameManager.gameManager.loadUi)
        {
            UIManager.uiManager.dash_slider.value = 0f;
        }

        yield return new WaitForSeconds(dash_time);

        dashing = false;
        body.gravityScale = current_grav;

        StartCoroutine(DashCoolDown());
    }

    IEnumerator DashCoolDown()
    {
        yield return new WaitForSeconds(dash_cooldown);
        can_dash = true;
    }
}
