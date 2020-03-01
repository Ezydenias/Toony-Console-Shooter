using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingCursor : MonoBehaviour
{
    public Transform gunCursorTargeter;
    public GameObject PlayerEmpty;
    public float maxDistance = 20;

    private List<GameObject> enemyList = new List<GameObject>();

    private GameObject Player;
    // Start is called before the first frame update


    void Start()
    {
        if (!PlayerEmpty)
            PlayerEmpty = GameObject.Find("Player Empty");
        Player = PlayerEmpty.GetComponent<CharacterChanger>().getCurrentCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Player)
        {
            Player = PlayerEmpty.GetComponent<CharacterChanger>().getCurrentCharacter();
        }

        if (!Player.GetComponent<PlayerController>().getSwimming() &&
            !Player.GetComponent<PlayerController>().getOnLadder())
        {
            enemyTarget(gunCursor());
        }
        else
        {
            gunCursorTargeter.position = new Vector3(0, -100000, 0);
        }

        // Debug.Log(enemyList.Count);
    }

    private void OnDisable()
    {
        // Debug.Log("what?");
        enemyList.Clear();
    }

    void OnTriggerEnter(Collider other)
    {
        // Filter by using specific layers for this object and "others" instead of using tags
        //Debug.Log("here");
        if (other.tag == "Enemy")
            enemyList.Add(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("and gone");
        // Filter by using specific layers for this object and "others" instead of using tags
        if (other.tag == "Enemy")
            enemyList.Remove(other.gameObject);
    }

    void enemyTarget(float cursorPosition)
    {
        float enemyDistance = 200;
        float oldDistance = 200;
        Vector3 closestPosition = new Vector3();


        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].gameObject)
            {
                enemyDistance = Vector3.Distance(transform.position, enemyList[i].transform.position);

                if (enemyDistance < oldDistance)
                {
                    closestPosition = enemyList[i].gameObject.transform.position;

                    oldDistance = enemyDistance;
                }
            }
            else
            {
                enemyList.Remove(enemyList[i].gameObject);
            }
        }

        if (enemyList.Count != 0)
        {
            if (oldDistance <= cursorPosition)
            {
                RaycastHit[] hits;
                hits = Physics.RaycastAll(transform.position, closestPosition - transform.position, oldDistance + 1);

                if (hits.Length != 0)
                {
                    gunCursorTargeter.GetComponent<MeshRenderer>().material.color = Color.red;
                    gunCursorTargeter.rotation = Quaternion.LookRotation(hits[0].normal);
                    gunCursorTargeter.transform.position = hits[0].point;
                }
            }
        }
    }

    float gunCursor()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, maxDistance);
//        Debug.DrawRay(transform.position, transform.forward, Color.green);

        float oldDistance = 100;
        float newDistance = 100;

        gunCursorTargeter.GetComponent<MeshRenderer>().material.color = Color.green;
        gunCursorTargeter.transform.position = transform.forward * 100;

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.layer != 8 && hits[i].collider.gameObject.layer!=2)
            {
                if (hits[i].collider.gameObject.tag != "Enemy" || enemyList.Count == 0)
                {
                    newDistance = hits[i].distance;
                    //Aligns cursor with the hit surface, this gets replaced with every next hit that is closer
                    //testing showed that the array of hits isn't built in a logical order
                    if (newDistance < oldDistance)
                    {
                        gunCursorTargeter.rotation = Quaternion.LookRotation(hits[i].normal);
                        gunCursorTargeter.transform.position = hits[i].point;
                        if (hits[i].collider.gameObject.layer == 9)
                        {
                            gunCursorTargeter.GetComponent<MeshRenderer>().material.color = Color.red;
                        }
                        else
                        {
                            gunCursorTargeter.GetComponent<MeshRenderer>().material.color = Color.green;
                        }

                        oldDistance = newDistance;
                    }
                }
            }
        }

        return oldDistance;
    }
}