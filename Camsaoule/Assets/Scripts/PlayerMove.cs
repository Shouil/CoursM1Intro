using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Transform PlayerCam;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerCam == null) 
        {
            Camera cam = transform.GetComponentInChildren<Camera>();
            PlayerCam = cam.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float rot = 0;
        rot = Input.GetAxis("Mouse Y") * 10;
        Quaternion q = Quaternion.AngleAxis(rot, PlayerCam.right);
        PlayerCam.rotation = q * PlayerCam.rotation;
    }

    private void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        float vert = Input.GetAxis("Vertical");
        float hori = Input.GetAxis("Horizontal");

        rb.AddForce(vert * transform.forward * 30);
        rb.AddForce(hori * transform.right * 30);

        float rot = 0;

        rot = Input.GetAxis("Mouse X") * 10;



        rb.AddTorque(Vector3.up * rot);

        /*if (rb != null)
        {

            if (Input.GetAxis("Horizontal") > 0)
            {
                rb.AddForce(transform.right * 10);
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                rb.AddForce(transform.right * -10);
            }
            if (Input.GetAxis("Vertical") > 0)
            {
                rb.AddForce(transform.forward * 10);
            }
            if (Input.GetAxis("Vertical") < 0)
            {
                rb.AddForce(transform.forward * -10);
            }
        }*/
    }
}

