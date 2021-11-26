using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Transform playerCam;
    public Transform objectToThrow;
    public bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        if (playerCam == null)
        {
            playerCam = transform.GetComponentInChildren<Camera>().transform;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Sauve la rotation
        Quaternion lastRotation = playerCam.rotation;

        // Baisse / leve la tete
        float rot = 0;
        rot = Input.GetAxis("Mouse Y") * -3;
        Quaternion q = Quaternion.AngleAxis(rot, playerCam.right);
        playerCam.rotation = q * playerCam.rotation;

        // Est ce qu'on a la tete à l'envers ?
        Vector3 forwardCam = playerCam.forward;
        Vector3 forwardPlayer = transform.forward;
        float regardeDevant = Vector3.Dot(forwardCam, forwardPlayer);
        if (regardeDevant < 0.0f)
        {
            playerCam.rotation = lastRotation;
        }

        // Tourner gauche droite
        rot = Input.GetAxis("Mouse X") * 3;
        q = Quaternion.AngleAxis(rot, transform.up);
        transform.rotation = q * transform.rotation;

        if(Input.GetButtonDown("Fire1"))
        {
            Transform obj = GameObject.Instantiate<Transform>(objectToThrow);
            obj.position = playerCam.position + playerCam.forward;
            obj.GetComponent<Rigidbody>().AddForce(playerCam.forward * 10, ForceMode.Impulse);
        }

    }

    // Update is called once per frame
    void FixedUpdate() // pour faire la physique
    {
        Rigidbody rb;
        rb = GetComponent<Rigidbody>();

        float vert = Input.GetAxis("Vertical");
        float hori = Input.GetAxis("Horizontal");

        Vector3 horizontalVelocity = Vector3.zero;
        horizontalVelocity += vert * transform.forward * 5;
        horizontalVelocity += hori * transform.right * 5;
        rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);

        // Tomber plus vite
        if (rb.velocity.y < 0)
            rb.AddForce(-transform.up * 20);

        //rb.AddForce(vert * transform.forward * 30);
        //rb.AddForce(hori * transform.right * 30);

        // Est ce qu'on touche le sol ?
        isGrounded = false;
        RaycastHit infos;
        bool trouve = Physics.SphereCast(transform.position + 0.1f * transform.up, 0.05f, -transform.up, out infos, 2);
        Debug.Log(trouve);
        if (trouve && infos.distance < 0.3)
            isGrounded = true;

        if (Input.GetButton("Jump"))
        {
            if (isGrounded)
            {
                rb.AddForce(transform.up * 5, ForceMode.Impulse); // hauteur de saut
                isGrounded = false;
            }
            else
            {
                if (rb.velocity.y < 3)
                {
                    rb.AddForce(transform.up * 3);
                }
                /*else
                    rb.velocity = new Vector3(rb.velocity.x, 3, rb.velocity.z);*/
            }
        }
        //rb.AddForce(transform.up*1.5f, ForceMode.Impulse); 
        //isGrounded = false;

    }
}
