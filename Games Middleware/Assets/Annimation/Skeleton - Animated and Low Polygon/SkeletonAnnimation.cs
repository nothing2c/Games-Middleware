using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnnimation : MonoBehaviour
{
    Animator Anim;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("w"))
            Anim.SetBool("IsWalking", true);
        else
            Anim.SetBool("IsWalking", false);
    }
}
