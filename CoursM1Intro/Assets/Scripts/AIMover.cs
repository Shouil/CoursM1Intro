using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMover : MonoBehaviour
{

    [Tooltip("Vitesse de déplacement"), Range(0, 15)]
    public float lunearSpeed = 6;
    [Tooltip("Vitesse de rotation"), Range(1, 5)]
    public float angularSpeed = 1;

    private Transform player;

    public Vector3 dirPlayer;

    public float life = 100;

    // Start is called before the first frame update
    public void Start()
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
        if (rb != null)
        {
            dirPlayer = player.position - transform.position;
            dirPlayer = dirPlayer.normalized;

            float angle = Vector3.SignedAngle(dirPlayer, transform.forward, transform.up);

            if (angle > 4)
                rb.AddTorque(transform.up * -5);

            else if (angle < -4)
                rb.AddTorque(transform.up * 5);


            if (Mathf.Abs(angle) < 10 && rb.velocity.magnitude < 3)
            {
                rb.AddForce(transform.forward * 45);
            }
            Debug.Log(dirPlayer);

            Animator anim = GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetFloat("Speed", rb.velocity.magnitude);
            }
        }
        if (life <= 0)
            Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(transform.position)
    }
}
public class EnemyAttack : MonoBehaviour
{

    public float enemyCooldown = 1;
    public float damage = 6;

    private bool playerInRange = false;
    private bool canAttack = true;

    private void Update()
    {
        if (playerInRange && canAttack)
        {
            GameObject.Find("Player").GetComponent<PlayerMove>().currentHealth -= damage;
            StartCoroutine(AttackCooldown());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(enemyCooldown);
        canAttack = true;
    }
}
