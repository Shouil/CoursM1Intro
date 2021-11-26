using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouleMagique : MonoBehaviour
{
    public float puissance = 30;
    public void OnCollisionEnter(Collision collision)
    {
        AIMover other = collision.gameObject.GetComponent<AIMover>();
        if (other != null)
        {
            other.life -= puissance;
        }
    }
}
