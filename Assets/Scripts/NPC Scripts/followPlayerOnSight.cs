using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayerOnSight : MonoBehaviour
{
    public GameObject Player;
    [Range(1f, 5f)] public float stopAtDistanceToPlayer = 1f;
    [Range(1f, 5f)] public float reactAtDistanceToPlayer = 1f;
    [Range(.01f, 10f)] public float moveSpeed = 1f;

    private bool chasePlayer = false;
    protected bool closeToPlayer = false;
    private CharacterController Controller;
    private Vector3? knownPlayerPosition = null;
    protected Vector3 moveDirection = new Vector3();
    private Transform lookDirection;

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
        lookDirection = transform;
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
            moveDirection = (Vector3) (knownPlayerPosition - transform.position);
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
        if (knownPlayerPosition != null)
        {
            lookDirection.LookAt((Vector3) knownPlayerPosition);
            lookDirection.rotation.eulerAngles.Set(0, lookDirection.rotation.eulerAngles.y,
                lookDirection.rotation.eulerAngles.z);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, lookDirection.rotation, 100 * Time.deltaTime);
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
        if (seesPlayer())
        {
            knownPlayerPosition = Player.transform.position;
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
        bool see=false;
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