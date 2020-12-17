using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKControl : MonoBehaviour
{
    Animator animator;
    public bool ikActive = false;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if(ikActive)
        {
            animator.SetLookAtWeight(1);
            animator.SetLookAtPosition(target.position);

            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKPosition(AvatarIKGoal.RightHand, target.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, target.rotation);
        }    
    }
    // Update is called once per frame
    void Update()
    {

    }
}
