using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingCollision : MonoBehaviour
{
    public float health = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col) {
        health--;
        Debug.Log(health);
    }
}
