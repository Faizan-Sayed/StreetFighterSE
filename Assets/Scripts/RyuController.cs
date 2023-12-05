using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyuController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float height;

    public Transform Mid;
    [SerializeField] private float rangemid;
    public Transform High;
    [SerializeField] private float rangehigh;
    public Transform Low;
    [SerializeField] private float rangelow;

    public LayerMask kenlayer;

    public Rigidbody2D rb;
    public Animator animate;

    private bool grounded;
    private bool crouch;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Grounded"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Grounded"))
        {
            grounded = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        animate.SetFloat("speed", 0);
        animate.SetBool("jumping", !grounded);
        animate.SetBool("crouching", crouch);

        if (Input.GetKey("left") && grounded)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            animate.SetFloat("speed", -1);
        }
        if (Input.GetKey("right") && grounded)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            animate.SetFloat("speed", 1);
        }
        if (Input.GetKeyDown("up") && (Input.GetKey("right") || Input.GetKey("left")) && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, height);
        }
        if (Input.GetKeyDown("up") && grounded)
        {
            rb.velocity = new Vector2(0, height);
        }
        if (Input.GetKey("down") && grounded  && !(Input.GetKey("left") || Input.GetKeyDown("up") || Input.GetKey("right")))
        {
            crouch = true;
        }
        else
        {
            crouch = false;
        }



        //buttons

        if (Input.GetKeyDown("o") && grounded)
        {
            animate.SetTrigger("HP");
            Invoke("SpawnHitbox", 1);

        }
        if (Input.GetKeyDown(";") && grounded)
        {
            animate.SetTrigger("MK");
            Invoke("SpawnHitbox", 1);

        }
        if (Input.GetKeyDown("l") && grounded)
        {
            Invoke("SpawnHigh", 1);

        }
        if (Input.GetKeyDown("p") && grounded)
        {
            Invoke("SpawnLow", 1);

        }
    }
    void SpawnHitbox()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(Mid.position, rangemid, kenlayer);
    }

    void SpawnLow()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(Low.position, rangelow, kenlayer);
        animate.SetTrigger("HK");
    }

    void SpawnHigh()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(High.position, rangehigh, kenlayer);
        animate.SetTrigger("MP");
    }
}
