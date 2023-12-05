using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class KenController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float height;

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
            animator.SetBool("jumping", true);
        }
        if (Input.GetKeyDown("w") && grounded)
        {
            rb.velocity = new Vector2(0, height);
            animator.SetBool("jumping", true);
        }
        if (Input.GetKey("s") && grounded)
        {
            crouch = true;
        }
        else
        {
            crouch = false;
        }

    }
}
