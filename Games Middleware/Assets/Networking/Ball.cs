using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    PhotonView PV;
    float throwForce = 100;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.forward * throwForce);
        rb.mass = 0.2f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.GetComponent<PlayerController>())
        {
            collision.collider.gameObject.GetComponent<PlayerController>().GetHit();
            Destroy(this.gameObject);
        }
    }
}
