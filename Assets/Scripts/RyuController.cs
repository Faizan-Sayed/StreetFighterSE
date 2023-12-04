using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public float ryuSpeed = 5f;
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 1f, 1f);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 1f, 1f);
        }
    }
}
