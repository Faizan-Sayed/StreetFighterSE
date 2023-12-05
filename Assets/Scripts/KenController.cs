using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class KenController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float height;

    public Transform Mid;
    [SerializeField] private float rangemid;
    public Transform High;
    [SerializeField] private float rangehigh;
    public Transform Low;
    [SerializeField] private float rangelow;

    public LayerMask ryulayer;

    public Rigidbody2D rb;

    public Animator animator;

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
        animator.SetFloat("speed", 0);
        animator.SetBool("jumping", !grounded);
        animator.SetBool("crouching", crouch);

        if (Input.GetKey("a") && grounded)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            animator.SetFloat("speed", -1);
        }
        if (Input.GetKey("d") && grounded)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            animator.SetFloat("speed", 1);
        }
        if (Input.GetKeyDown("w") && (Input.GetKey("d") || Input.GetKey("a")) && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, height);
        }
        if (Input.GetKeyDown("w") && grounded)
        {
            rb.velocity = new Vector2(0, height);
        }
        if (Input.GetKey("s") && grounded && !(Input.GetKey("d") || Input.GetKeyDown("w") || Input.GetKey("a")))
        {
            crouch = true;
        }
        else
        {
            crouch = false;
        }

        // attacks time!



        if (Input.GetKeyDown("r") && grounded)
        {
            animator.SetTrigger("mid");
            Invoke("SpawnHitbox", 1);

        }
        if (Input.GetKeyDown("t") && grounded)
        {
            animator.SetTrigger("midh");
            Invoke("SpawnHitbox", 2);

        }
        if (Input.GetKeyDown("f") && grounded)
        {
            Invoke("SpawnHigh", 2);

        }
        if (Input.GetKeyDown("g") && grounded)
        {
            Invoke("SpawnLow", 1);

        }

    }

    void SpawnHitbox()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(Mid.position, rangemid, ryulayer);
    }

    void SpawnLow()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(Low.position, rangelow, ryulayer);
        animator.SetTrigger("low");
    }

    void SpawnHigh()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(High.position, rangehigh, ryulayer);
        animator.SetTrigger("high");
    }
}
