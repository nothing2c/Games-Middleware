using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class AnimControl : MonoBehaviour
{
    public Animator Bandit;
    public Animator Robo;
    public Animator Teddy;

    private readonly string INCREASE_SPEED = "e";
    private readonly string DECREASE_SPEED = "q";
    private readonly string WALK_KEY = "w";
    private readonly string ATTACK_KEY = "f";
    private readonly string REACH_KEY = "r";
    private readonly string GRAB_KEY = "g";

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(WALK_KEY))
        {
            Bandit.SetBool("isWalking", true);
            Robo.SetBool("isWalking", true);
            Teddy.SetBool("isWalking", true);
        }
        else
        {
            Bandit.SetBool("isWalking", false);
            Robo.SetBool("isWalking", false);
            Teddy.SetBool("isWalking", false);
        }

        if(Input.GetKeyDown(INCREASE_SPEED))
        {
            Robo.SetFloat("movementBlend", Robo.GetFloat("movementBlend") + 0.2f);
        }

        if (Input.GetKeyDown(DECREASE_SPEED))
        {
            Robo.SetFloat("movementBlend", Robo.GetFloat("movementBlend") - 0.2f);
        }

        if (Input.GetKeyDown(ATTACK_KEY))
        {
            Robo.SetTrigger("Attack");
            Teddy.SetTrigger("Attack");
        }

        if(Input.GetKeyDown(REACH_KEY))
        {
            if(Bandit.gameObject.GetComponent<IKControl>().ikActive)
                Bandit.gameObject.GetComponent<IKControl>().ikActive = false;
            else
                Bandit.gameObject.GetComponent<IKControl>().ikActive = true;
        }

        if(Input.GetKeyDown(GRAB_KEY))
        {
            Transform[] bones = Bandit.gameObject.GetComponentsInChildren<Transform>();
            Transform leftHand = null;

            foreach(Transform bone in bones)
            {
                if (bone.name == "Hand.R")
                    leftHand = bone;
            }

            GameObject Target = GameObject.FindGameObjectWithTag("Target");
            Target.transform.position = leftHand.position;
            Target.transform.SetParent(leftHand);
        }
    }
}
