using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    [Tooltip("Prefab à spawn")]
    public Transform PrefabAI;
    [Tooltip("Point de spawn des IA")]
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        //Transform ai = SpawnAI();
        //AddPichenette(ai, ai.forward * 5);
    }

    Transform SpawnAI()
    {
        Transform ai = GameObject.Instantiate<Transform>(PrefabAI);
        ai.position = spawnPoint.position;
        ai.rotation = spawnPoint.rotation;
        return ai;
    }

    void AddPichenette(Transform ai, Vector3 pichenette)
    {
        Rigidbody rb = ai.GetComponent<Rigidbody>();
        rb.AddForce(pichenette,ForceMode.Impulse);

    }
    public float time = 0;
    [Range(0, 15)]
    public float timeMax = 15;
    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;
        if(time > timeMax)
        {
            Transform ai = SpawnAI();
            Vector3 pichenette = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            AddPichenette(ai, pichenette);
            time = 0;
        }
    }

}
