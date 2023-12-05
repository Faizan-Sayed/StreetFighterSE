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

    public Animator animator;

    public Rigidbody2D rb;

    private bool grounded;
    public bool crouch;
    private bool highblock;
    private bool lowblock;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar1 healthBar;
    public int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        StartCoroutine(CheckDeath(0.1f));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Grounded"))
        {
            grounded = true;
        }
        else if (other.gameObject.CompareTag("Player"))
        { // colliding with the opponent's attack
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
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
    public float kenSpeed = 5f;
    void Update()
    {
        animator.SetFloat("speed", 0);
        animator.SetBool("jumping", !grounded);
        animator.SetBool("crouching", crouch);
        highblock = false;
        lowblock = false;

        move();
        attacks();
    }

    public void Damage(int x)
    {
        currentHealth -= x;
        healthBar.SetHealth(currentHealth);
    }

    void move()
    {
        if (Input.GetKey("a") && grounded)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            animator.SetFloat("speed", -1);
            if (Input.GetKey("s"))
            {
                lowblock = true;
            }
            else
            {
                highblock = true;
            }
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
        if (Input.GetKey("s") && grounded)
        {
            crouch = true;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            crouch = false;
        }
    }

    void attacks()
    {
        if (Input.GetKeyDown("r") && grounded)
        {
            animator.SetTrigger("mid");
            Invoke("SpawnHitbox", 1);
            rb.velocity = new Vector2(0, rb.velocity.y);

        }
        if (Input.GetKeyDown("t") && grounded)
        {
            animator.SetTrigger("midh");
            Invoke("SpawnHitbox", 1);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (Input.GetKeyDown("f") && grounded)
        {
            Invoke("SpawnHigh", 1);
            rb.velocity = new Vector2(0, rb.velocity.y);

        }
        if (Input.GetKeyDown("g") && grounded)
        {
            Invoke("SpawnLow", 1);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    IEnumerator CheckDeath(float waitTime)
    {
        while(true) {
            if (currentHealth <= 0) 
            {
                Debug.Log("Game Over!");
            }
            yield return new WaitForSeconds(waitTime);
        }
    }

    void SpawnHitbox()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(Mid.position, rangemid, ryulayer);
        foreach(Collider2D player in hit)
        {
            if (player.GetComponent<RyuController>().rb.velocity.x >= 0)
            {
                player.GetComponent<RyuController>().Damage(15); 
            }
        }

    }

    void SpawnLow()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(Low.position, rangelow, ryulayer);
        animator.SetTrigger("low");
        foreach (Collider2D player in hit)
        {
            if (player.GetComponent<RyuController>().rb.velocity.x >= 0 && player.GetComponent<RyuController>().crouch == false)
            {
                player.GetComponent<RyuController>().Damage(20);
            }
        }
    }

    void SpawnHigh()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(High.position, rangehigh, ryulayer);
        animator.SetTrigger("high");
        foreach (Collider2D player in hit)
        {
            if (player.GetComponent<RyuController>().rb.velocity.x >= 0 && player.GetComponent<RyuController>().crouch == true)
            {
                player.GetComponent<RyuController>().Damage(30);
            }
        }
    }
}
