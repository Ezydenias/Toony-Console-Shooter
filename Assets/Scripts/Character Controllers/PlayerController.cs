using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;
    public float rotateSpeed;
    public float playerHeight = 1;
    public float floatingSpeed = 3;
    public float sinkLevel = .5f;
    public float swimsinklevel = .5f;
    [Range(1.01f, 2)] public float waterDamper=2;
    [Range(1.01f, 4)] public float DampingMultiplyer=2;

    private bool grounded = true;
    private CharacterController Controller;
    private Vector3 moveDirection;
    private float waterTable = -100;
   

    private bool inWater = false;
    private bool onLadder = false;
    private bool swimming = false;

    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        moveDirection = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        grounded = isGrounded();
        var yStore = moveDirection.y;
        moveDirection = transform.forward * Input.GetAxis("Vertical") +
                        transform.right * Input.GetAxis("Horizontal");

        moveDirection *= moveSpeed;
        moveDirection.y = yStore;

        if (inWater)
        {
            if (waterTable > transform.position.y)
            {
                swimming = true;
            }
            else
            {

                swimming = false;
            }
        }
        else
        {
            swimming = false;
        }


        if (onLadder)
        {
        }
        else if (inWater && swimming)
        {
            if (waterTable+ sinkLevel > transform.position.y)
            {
                moveDirection.y = moveDirection.y - DampingMultiplyer*Time.deltaTime*(moveDirection.y / waterDamper);
                moveDirection.y = moveDirection.y - Physics.gravity.y * Time.deltaTime *(Mathf.Abs(waterTable - (transform.position.y - swimsinklevel)));
            }
            else
            {
                moveDirection.y = moveDirection.y - DampingMultiplyer*Time.deltaTime * (moveDirection.y / waterDamper);
                moveDirection.y = moveDirection.y + Physics.gravity.y * Time.deltaTime * (Mathf.Abs(waterTable - (transform.position.y - swimsinklevel)));

            }

            Controller.Move(moveDirection * Time.deltaTime);
        }
        else
        {
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

    public bool getGrounded()
    {
        return grounded;
    }

    bool isGrounded()
    {
        RaycastHit[] ground;
        ground = Physics.RaycastAll(transform.position, new Vector3(0, -1, 0), playerHeight);
        //Debug.Log("here");

        if (ground.Length >= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        // Filter by using specific layers for this object and "others" instead of using tags
        if (other.tag == "Water")
        {
            waterTable = other.gameObject.transform.position.y - sinkLevel;

            inWater = true;
        }

        if (other.tag == "Ladder")
        {
            onLadder = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("and gone");
        // Filter by using specific layers for this object and "others" instead of using tags
//        Debug.Log("left");
        if (other.tag == "Water")
        {
            inWater = false;
            waterTable = -100;
        }

        if (other.tag == "Ladder")
        {
            onLadder = false;
        }
    }
}