using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject CameraHolder;
    [SerializeField] Transform ThrowPoint;
    [SerializeField] float MouseSensitivity, SprintSpeed, WalkSpeed, SmoothTime;

    bool Grounded;
    Vector3 SmoothMoveVelocity;
    Vector3 MoveAmount;

    Rigidbody rb;
    Animator anim;
    PhotonView PV;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
        }
           
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
            return;

        Look();
        Move();

        if (Input.GetKeyDown("e"))
        {
            Throw();
        }
    }

    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;

        rb.MovePosition(rb.position + transform.TransformDirection(MoveAmount) * Time.fixedDeltaTime);
    }

    void Move()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            anim.SetBool("isWalking", true);
        else
            anim.SetBool("isWalking", false);

        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveDir *= SprintSpeed;
            anim.SetFloat("movementBlend", 1);
        }
        else
        {
            moveDir *= WalkSpeed;
            anim.SetFloat("movementBlend", 0);
        }

        MoveAmount = Vector3.SmoothDamp(MoveAmount, moveDir, ref SmoothMoveVelocity, SmoothTime);
    }

    void Look()
    {
        transform.Rotate(Vector3.up, Input.GetAxisRaw("Mouse X") * MouseSensitivity);
    }

    public void Throw()
    {
        anim.SetTrigger("Attack");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Ball"), ThrowPoint.position + transform.forward, transform.rotation);
    }

    public void GetHit()
    {
        anim.SetTrigger("Hit");
    }
}
