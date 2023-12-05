using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyuController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float height;
    public Rigidbody2D rb;
    private bool grounded;
    public bool crouch;

    public Transform Mid;
    [SerializeField] private float rangemid;
    public Transform High;
    [SerializeField] private float rangehigh;
    public Transform Low;
    [SerializeField] private float rangelow;

    public LayerMask kenlayer;
    public Animator animate;
    private bool highblock;
    private bool lowblock;
    
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar2 healthBar;
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
        } else if (other.gameObject.CompareTag("Player")) { // colliding with the opponent's attack
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
    public float ryuSpeed = 5f;
    void Update()
    {
        animate.SetFloat("speed", 0);
        animate.SetBool("jumping", !grounded);
        animate.SetBool("crouching", crouch);
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
        if (Input.GetKey("left") && grounded)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            animate.SetFloat("speed", -1);
        }
        if (Input.GetKey("right") && grounded)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            animate.SetFloat("speed", 1);
            if (Input.GetKey("down"))
            {
                lowblock = true; 
            }
            else
            {
                highblock = true;
            }
        }
        if (Input.GetKeyDown("up") && (Input.GetKey("right") || Input.GetKey("left")) && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, height);
        }
        if (Input.GetKeyDown("up") && grounded)
        {
            rb.velocity = new Vector2(0, height);
        }
        if (Input.GetKey("down") && grounded)
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
        if (Input.GetKeyDown("o") && grounded)
        {
            animate.SetTrigger("HP");
            Invoke("SpawnHitbox", 1);
            rb.velocity = new Vector2(0, rb.velocity.y);

        }
        if (Input.GetKeyDown(";") && grounded)
        {
            animate.SetTrigger("MK");
            Invoke("SpawnHitbox", 1);
            rb.velocity = new Vector2(0, rb.velocity.y);

        }
        if (Input.GetKeyDown("l") && grounded)
        {
            Invoke("SpawnHigh", 1);
            rb.velocity = new Vector2(0, rb.velocity.y);

        }
        if (Input.GetKeyDown("p") && grounded)
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
        Collider2D[] hit = Physics2D.OverlapCircleAll(Mid.position, rangemid, kenlayer);
        foreach (Collider2D player in hit)
        {
            if (player.GetComponent<KenController>().rb.velocity.x >= 0)
            {
                player.GetComponent<KenController>().Damage(15);
            }
        }
    }

    void SpawnLow()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(Low.position, rangelow, kenlayer);
        animate.SetTrigger("HK");
        foreach (Collider2D player in hit)
        {
            if (player.GetComponent<KenController>().rb.velocity.x >= 0 && player.GetComponent<KenController>().crouch == false)
            {
                player.GetComponent<KenController>().Damage(20);
            }
        }
    }

    void SpawnHigh()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(High.position, rangehigh, kenlayer);
        animate.SetTrigger("MP");
        foreach (Collider2D player in hit)
        {
            if (player.GetComponent<KenController>().rb.velocity.x >= 0 && player.GetComponent<KenController>().crouch == true)
            {
                player.GetComponent<KenController>().Damage(30);
            }
        }
    }
}
