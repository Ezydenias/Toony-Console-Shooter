using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;
    public float rotateSpeed;
    private CharacterController Controller;
    private Vector3 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        moveDirection = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        var yStore = moveDirection.y;
        moveDirection = transform.forward * Input.GetAxis("Vertical") +
                        transform.right * Input.GetAxis("Horizontal");

        moveDirection *= moveSpeed;
        moveDirection.y = yStore;

        if (Controller.isGrounded)
        {
            moveDirection.y = 0f;
            if (Input.GetButtonDown("Jump"))
                moveDirection.y = jumpForce;
        }


        moveDirection.y = moveDirection.y + Physics.gravity.y * Time.deltaTime * gravityScale;
        Controller.Move(moveDirection * Time.deltaTime);
    }
}
