using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayerOnSight : MonoBehaviour
{
    public GameObject Player;
    [Range(1f, 5f)] public float stopAtDistanceToPlayer = 1f;
    [Range(1f, 5f)] public float reactAtDistanceToPlayer = 1f;
    [Range(.01f, 10f)] public float moveSpeed = 1f;
    [Range(.01f, 3f)] public float reactionSpeed = .5f;
    [Range(1f, 20f)] public float rotationSpeed = 2f;

    private bool chasePlayer = false;
    protected bool closeToPlayer = false;
    private CharacterController Controller;
    private Vector3? knownPlayerPosition = null;
    protected Vector3 moveDirection = new Vector3();
    private Quaternion lookDirection = Quaternion.identity;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        parentStartFunction();
    }

    protected void parentStartFunction()
    {
        if (!Player)
            Player = GameObject.Find("Player");
        Controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        getPlayerPosition();
        chhasePlayerFunction();
        orientateCharacter();
        moveCharacter();
    }

    protected void chhasePlayerFunction()
    {
        if (knownPlayerPosition != null)
        {
//            moveDirection = (Vector3) (knownPlayerPosition - transform.position);
            moveDirection = transform.forward;
            moveDirection.y = 0;
            if (Vector3.Distance(transform.position, (Vector3) knownPlayerPosition) <= stopAtDistanceToPlayer)
            {
                knownPlayerPosition = null;
                moveDirection = Vector3.zero;
            }
        }
        else
        {
            moveDirection = Vector3.zero;
        }
    }

    protected void grabPlayerPosition()
    {
        if (chasePlayer)
        {
            getPlayerPosition();
        }
    }

    protected void orientateCharacter()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, lookDirection, Time.deltaTime * rotationSpeed);
    }

    private void setCharacterOrientation()
    {
        if (knownPlayerPosition != null)
        {
            lookDirection = Quaternion.LookRotation((Vector3) (knownPlayerPosition - transform.position));
        }
    }

    protected void moveCharacter()
    {
        moveDirection = moveDirection.normalized;
        Controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            chasePlayer = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            chasePlayer = false;
        }
    }

    protected void getPlayerPosition()
    {
        if (seesPlayer() && ((timer += Time.deltaTime) >= reactionSpeed))
        {
            timer = 0;
            setCharacterOrientation();
            knownPlayerPosition = new Vector3(Player.transform.position.x, transform.position.y,
                Player.transform.position.z);
        }


        if (knownPlayerPosition != null)
            closeToPlayer = (Vector3.Distance((Vector3) knownPlayerPosition, transform.position) <=
                             reactAtDistanceToPlayer)
                ? true
                : false;
    }

    protected bool seesPlayer()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll((transform.position), (Player.transform.position - transform.position),
            Vector3.Distance(transform.position,
                Player.transform.position));
        float oldDistance = 100;
        float newDistance;
        bool see = false;
        for (int i = 0; i < hits.Length; i++)
        {
            if ((newDistance = Vector3.Distance(transform.position, hits[i].transform.position)) <= oldDistance)
            {
                oldDistance = newDistance;
                see = (hits[i].collider.tag == "Player") ? true : false;
            }
        }

        return see;
    }

    void setknownPlayerPosition(Vector3 newPosition)
    {
        knownPlayerPosition = newPosition;
    }
}