using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed=7f;
    public float jumpForce=10f;
    public float ladderJumpOff = 3f;
    public float gravityScale=1.5f;
    public float rotateSpeed=50;
    public float playerHeight = 1;
//    public float floatingSpeed = .5f;
    public float sinkLevel = -.5f;
    public float swimsinklevel = .7f;
    public float climbSpeed = 7;
    public float ledgeGrabBounds = .5f;
    public float ledgeJumpHeightY = -.2f;
    public float ledgeJumpTravel = 1f;
    public float ledgeJumpSpeed = 3f;
    [Range(1.01f, 2)] public float waterDamper = 2;
    [Range(1.01f, 4)] public float DampingMultiplyer = 4;

    private bool grounded = true;
    protected CharacterController Controller;
    protected Vector3 moveDirection = new Vector3(0f, 0f, 0f);
    private Vector3 ledgePosition = new Vector3(-1000, -1000, -1000);
    private float waterTable = -100;

    private int ledgeMode = 0;
    protected bool inWater = false;
    protected bool onLadder = false;
    protected bool swimming = false;
    private bool onLedge = false;
    protected bool ledgeGrab = false;

    private Quaternion ladderOrientation = new Quaternion();

    // Start is called before the first frame update
    void Start()
    {

        Controller = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        float yStore = 0;
        standardUpdate(ref yStore);


        if (ledgeGrab)
        {
            ledgeGrabMethod();
        }
        else if (onLadder)
        {
            ladderMethod(yStore);
        }
        else if (inWater && swimming)
        {
            swimmingMethode(yStore);
        }
        else
        {
            walkingMethode(yStore);
        }
    }

    protected float standardUpdate(ref float yStore)
    {
        grounded = isGrounded();
        yStore = moveDirection.y;
        moveDirection = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");

        checkSwimming();

        checkOnLedge();

        return yStore;
    }

    protected void checkOnLedge()
    {
        if (onLedge && moveDirection.z <= .3f && (ledgePosition.y - ledgeGrabBounds) < transform.position.y &&
            transform.position.y < (ledgePosition.y + ledgeGrabBounds))
        {
            ledgeGrab = true;
        }
    }

    protected void checkSwimming()
    {
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
    }

    public bool getGrounded()
    {
        return grounded;
    }

    public bool getSwimming()
    {
        return swimming;
    }

    public bool getOnLadder()
    {
        return onLadder;
    }

    public bool getOnLedge()
    {
        return onLedge;
    }

    public bool getLedgeGrab()
    {
        return ledgeGrab;
    }

    public Quaternion getLadderOrientation()
    {
        return ladderOrientation;
    }

    bool isGrounded()
    {
        RaycastHit[] ground;
        ground = Physics.RaycastAll(transform.position, new Vector3(0, -1, 0), playerHeight);
        //Debug.Log("here");

        if (ground.Length >= 1)
            return true;
        else
            return false;
    }

    void OnTriggerEnter(Collider other)
    {
        standardOnTriggerrEnter(other);
    }

    protected void standardOnTriggerrEnter(Collider other)
    {
        // Filter by using specific layers for this object and "others" instead of using tags
        if (other.tag == "Water")
        {
            waterTable = other.gameObject.transform.position.y;

            inWater = true;
        }

        if (other.tag == "Ladder")
        {
            ladderOrientation = other.gameObject.transform.rotation;
            onLadder = true;
        }

        if (other.tag == "Ledge")
        {
            ledgePosition = other.gameObject.transform.position;
            onLedge = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
     standardOnTriggerrExit(other);   
    }

    protected void standardOnTriggerrExit(Collider other)
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

        if (other.tag == "Ledge")
        {
            onLedge = false;
        }
    }

    public void triggerJump()
    {
        moveDirection.y = jumpForce;
    }

    protected void ledgeGrabMethod()
    {
        if (transform.position.y < (ledgePosition.y + Controller.height + ledgeJumpHeightY) && ledgeMode == 0)
        {
            moveDirection.y = ledgeJumpSpeed;
            Controller.Move(moveDirection * Time.deltaTime);
        }
        else
        {
            moveDirection.y = 0;
            ledgeMode = 1;
        }

        if (Mathf.Abs(transform.position.z - ledgePosition.z) < ledgeJumpTravel)
        {
            if (ledgeMode == 1)
            {
                moveDirection = transform.forward * ledgeJumpSpeed;
                moveDirection.y = moveDirection.y + Physics.gravity.y * Time.deltaTime * gravityScale;
                Controller.Move(moveDirection * Time.deltaTime);
            }
        }
        else
        {
            ledgeMode = 2;
        }


        if (ledgeMode == 2)
        {
            ledgeMode = 0;
            ledgeGrab = false;
        }
    }

    protected void ladderMethod(float yStore)
    {
        moveDirection *= climbSpeed;
        if (-moveDirection.z < 0 && Controller.isGrounded)
        {
            onLadder = false;
        }
        else
        {
            Controller.Move(new Vector3(0, -moveDirection.z, 0) * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump"))
        {
            moveDirection.y = jumpForce;
            Controller.Move(new Vector3(0, moveDirection.y, ladderJumpOff) * Time.deltaTime);
        }
        else if (Input.GetButton("Jump"))
        {
            moveDirection.y = yStore;
        }
    }

    protected void swimmingMethode(float yStore)
    {
        moveDirection *= moveSpeed;
        moveDirection.y = yStore;
        if (waterTable + sinkLevel > transform.position.y)
        {
            moveDirection.y =
                moveDirection.y - DampingMultiplyer * Time.deltaTime * (moveDirection.y / waterDamper);
            moveDirection.y = moveDirection.y - Physics.gravity.y * Time.deltaTime *
                              (Mathf.Abs(waterTable - (transform.position.y - swimsinklevel)));
        }
        else
        {
            moveDirection.y =
                moveDirection.y - DampingMultiplyer * Time.deltaTime * (moveDirection.y / waterDamper);
            moveDirection.y = moveDirection.y + Physics.gravity.y * Time.deltaTime *
                              (Mathf.Abs(waterTable - (transform.position.y - swimsinklevel)));
        }

        Controller.Move(moveDirection * Time.deltaTime);
    }

    protected void walkingMethode(float yStore)
    {
        moveDirection *= moveSpeed;
        moveDirection.y = yStore;

        jumpMethode();

        moveDirection.y = moveDirection.y + Physics.gravity.y * Time.deltaTime * gravityScale;
        Controller.Move(moveDirection * Time.deltaTime);
    }

    protected virtual void jumpMethode()
    {
        if (Controller.isGrounded)
        {
            //moveDirection.y = 0f;
            if (Input.GetButtonDown("Jump"))
                moveDirection.y = jumpForce;
        }
    }

}