using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public CharacterController controller;
    public float jumpForce;
    public float gravityScale;

    private Vector3 moveDirection;
    public Animator anim;
    public Transform pivot;
    public float rotateSpeed;
    public GameObject playerModel;
 
    public float knockBackForce;
    public float knockBackTime;
    private float KnockBackCounter;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
      
    }

    
    // Update is called once per frame
    void Update()
    {

        ControlMovementPLayer();

        //Move the Player in different directions based on cameraDirection

        if (moveDirection.x != 0 || moveDirection.z != 0)
        {
            transform.rotation = (Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f));
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x,0f,moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }
    }


     public void KnockBack(Vector3 direction)
    {
        KnockBackCounter = knockBackTime;
        moveDirection = direction * knockBackForce;
        moveDirection.y = knockBackForce;
     }


    void ControlMovementPLayer() {

        if (KnockBackCounter <= 0)
        {
            CheckInputForRunning();


            CheckForJumping();


        }
        else
        {
            KnockBackCounter -= Time.deltaTime;
        }


        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);


    }


    void CheckInputForRunning() {
        controller.minMoveDistance = 0;
        anim.SetBool("isGrounded", controller.isGrounded);
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxisRaw("Vertical")) + Mathf.Abs(Input.GetAxisRaw("Horizontal"))));


        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);


        float yStore = moveDirection.y;


        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) +
                        (transform.right * Input.GetAxisRaw("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;


    }


    void CheckForJumping() {
        if (controller.isGrounded == true)
        {
            moveDirection.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {

                moveDirection.y = jumpForce;

            }

        }

    }


}
