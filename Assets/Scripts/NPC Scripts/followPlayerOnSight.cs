using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class followPlayerOnSight : MonoBehaviour
{
    [Range(1f, 5f)] public float stopAtDistanceToPlayer = 1f;
    [Range(1f, 5f)] public float reactAtDistanceToPlayer = 1f;
    [Range(.01f, 10f)] public float moveSpeed = 1f;
    [Range(.01f, 3f)] public float reactionSpeed = .5f;
    [Range(1f, 20f)] public float rotationSpeed = 2f;

    protected List<GameObject> PlayerList = new List<GameObject>();
    private bool chasePlayer = false;
    protected bool closeToPlayer = false;
    private CharacterController Controller;
    private Vector3? knownPlayerPosition = null;
    protected Vector3 moveDirection = new Vector3();
    private Quaternion lookDirection = Quaternion.identity;
    private float timer = 0;
    protected GameObject currentTarget = null;
    protected GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        parentStartFunction();
    }

    protected void parentStartFunction()
    {
        parent = transform.parent.gameObject;
        Controller = parent.GetComponent<CharacterController>();
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
            //            moveDirection = (Vector3) (knownPlayerPosition - parrent.transform.position);
            moveDirection = parent.transform.forward;
            moveDirection.y = 0;
            if (Vector3.Distance(parent.transform.position, (Vector3) knownPlayerPosition) <= stopAtDistanceToPlayer)
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
        parent.transform.rotation =
            Quaternion.Lerp(parent.transform.rotation, lookDirection, Time.deltaTime * rotationSpeed);
    }

    private void setCharacterOrientation()
    {
        if (knownPlayerPosition != null)
        {
            lookDirection = Quaternion.LookRotation((Vector3) (knownPlayerPosition - parent.transform.position));
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
//            if (currentTarget == null)
//            {
//                currentTarget = other.gameObject;
//                chasePlayer = true;
//            }

            PlayerList.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            currentTarget = null;
            knownPlayerPosition = null;
            PlayerList.Remove(other.gameObject);
            if (PlayerList.Count <= 0)
                chasePlayer = false;
        }
    }


    protected void getPlayerPosition()
    {
        if (((timer += Time.deltaTime) >= reactionSpeed)/* && currentTarget != null*/)
        {
            timer = 0;


//            if (seesPlayer(currentTarget))
//            {
//                knownPlayerPosition = new Vector3(currentTarget.transform.position.x, parent.transform.position.y,
//                    currentTarget.transform.position.z);
//            }

            if (PlayerList.Count > 0)
            {
                float oldDistance = 1000f;
                foreach (GameObject Player in PlayerList)
                {
                    Debug.Log(PlayerList.Count);
                    if (!Player.activeSelf)
                    {
                        PlayerList.Remove(Player);
                        break;
                    }

                    if (seesPlayer(Player))
                    {
                        var newDistance = Vector3.Distance(parent.transform.position, Player.transform.position);
                        if (newDistance < oldDistance)
                        {
                            knownPlayerPosition = new Vector3(Player.transform.position.x, parent.transform.position.y,
                                Player.transform.position.z);
                            oldDistance = newDistance;
                            currentTarget = Player;
                        }
                    }
                }
            }
            else
            {
                currentTarget = null;
                knownPlayerPosition = null;
            }
        }

        if (knownPlayerPosition != null)
        {
            setCharacterOrientation();
            closeToPlayer = (Vector3.Distance((Vector3) knownPlayerPosition, parent.transform.position) <=
                             reactAtDistanceToPlayer)
                ? true
                : false;
        }
    }

    protected bool seesPlayer(GameObject Player)
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll((parent.transform.position), (Player.transform.position - parent.transform.position),
            Vector3.Distance(parent.transform.position,
                Player.transform.position));
        float oldDistance = 100;
        float newDistance;
        bool see = false;
        for (int i = 0; i < hits.Length; i++)
        {
            if ((newDistance = Vector3.Distance(parent.transform.position, hits[i].transform.position)) <= oldDistance)
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