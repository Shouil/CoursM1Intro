using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMover : MonoBehaviour
{
    // Start is called before the first frame update
    [Tooltip("Vitesse de déplacement"), Range(1, 15)]
    public float linearSpeed = 6;
    [Tooltip("Vitesse de rotation"), Range(1, 15)]
    public float angularSpeed = 1;

    private Transform player;
    void Start()
    {
       GameObject goPlayer = GameObject.FindGameObjectWithTag("Player");
        player = goPlayer.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb != null)
        {
            //if(rb.velocity.magnitude < 5)
            /*if(Input.GetButton("Fire1") && rb.velocity.magnitude < 1)
            rb.AddForce(transform.forward*30);*/

            if (rb.angularVelocity.magnitude < angularSpeed)
            {
                rb.AddTorque(transform.up * 10);
            }

            Animator anim = GetComponent<Animator>();
            if(anim != null)
            {
                anim.SetFloat("Speed", rb.velocity.magnitude);
            }
        }
    }
}
