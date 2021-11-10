using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody rb;
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;

        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
