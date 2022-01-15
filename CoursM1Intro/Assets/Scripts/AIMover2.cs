using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMover2 : MonoBehaviour
{
    Transform playerTransform;
    UnityEngine.AI.NavMeshAgent myNavmesh;
    public float checkRate = 0.01f;
    float nextCheck;
    public float life = 100;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player").activeInHierarchy)
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        myNavmesh = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            FollowPlayer();
        }
        if (life <= 0)
            Destroy(gameObject);
    }
    void FollowPlayer()
    {
        myNavmesh.transform.LookAt(playerTransform);
        myNavmesh.destination = playerTransform.position;
    }
}
