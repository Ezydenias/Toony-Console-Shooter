using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Persistence;

public class EzyController : PlayerController
{
    /*[Range(0.01f, 4)] */
    //    public float shoveDistance = 1f;
    [Range(1f, 4f)] public float turnSpeed = 1f;

    // Start is called before the first frame update
    private bool shoving = false;
    private bool shovs = false;
    private GameObject heavyObject = null;
    private Vector3 offset;
    private Quaternion lookDirection;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HeavyShove" && heavyObject == null)
        {
            heavyObject = other.gameObject;
            shoving = true;
        }

        standardOnTriggerrEnter(other);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "HeavyShove")
        {
            heavyObject = null;
            shoving = false;
        }

        standardOnTriggerrExit(other);
    }

    protected override void jumpMethode()
    {
        if (Controller.isGrounded)
        {
            //moveDirection.y = 0f;
            if (!shoving)
            {
                if (Input.GetButtonDown("Jump"))
                    moveDirection.y = jumpForce;
            }
            else
            {
                if (heavyObject != null)
                {
                    if (Input.GetButtonDown("Jump"))
                    {
                        shovs = true;
                        lookDirection = Quaternion.LookRotation(
                            new Vector3(heavyObject.transform.position.x, transform.position.y,
                                heavyObject.transform.position.z) - transform.position);

//                        Controller.Move(distanceOffsetToObject());
                        heavyObject.GetComponent<EzyShoove>().shoveEmpty.GetComponent<Collider>().enabled = false;
                        offset = transform.position - heavyObject.transform.position;
                    }

                    if (Input.GetButton("Jump"))
                    {
                        heavyObject.transform.position = transform.position - offset;
                        transform.rotation =
                            Quaternion.Lerp(transform.rotation, lookDirection, Time.deltaTime * turnSpeed);
                    }

                    if (Input.GetButtonUp("Jump"))
                    {
                        shovs = false;
                        heavyObject.GetComponent<EzyShoove>().shoveEmpty.GetComponent<Collider>().enabled = true;
                    }
                }
                else
                {
                    shovs = false;
                    shoving = false;
                }
            }
        }
    }

    public bool getShoving()
    {
        return shovs;
    }

    //depricated try to make it manually instead of using the collider trick, which is probably more or less the same just low carb
//    protected Vector3 distanceOffsetToObject()
//    {
//        float distance = Vector3.Distance(transform.position, heavyObject.transform.position);
//        if (distance < shoveDistance)
//        {
//            RaycastHit[] hits;
//            hits = Physics.RaycastAll(transform.position, heavyObject.transform.position - transform.position,
//                distance);
//            float oldDistance = 100;
//            float newDistance = 100;
//            Vector3 distanceOffset = Vector3.zero;
//            for (int i = 0; i < hits.Length; i++)
//            {
//                Debug.DrawRay(transform.position, hits[i].point, Color.green);
//                newDistance = hits[i].distance;
//                if (newDistance < oldDistance)
//                {
//                    distanceOffset = hits[i].transform.position;
//                    oldDistance = newDistance;
//                }
//            }
//
//            distanceOffset.y = 0;
//            Vector3 tempPosition = transform.position;
//            tempPosition.y = 0;
//            float makeDistance = Vector3.Distance(tempPosition, distanceOffset);
//            Debug.Log(makeDistance);
//            Debug.Log(shoveDistance);
//            makeDistance = shoveDistance - makeDistance;
//            Debug.Log(makeDistance);
//            distanceOffset = distanceOffset - tempPosition;
//            Debug.Log(distanceOffset);
//            distanceOffset = distanceOffset.normalized * makeDistance;
//            Debug.Log(distanceOffset);
//
//            return distanceOffset;
//        }
//        else
//        {
//            return Vector3.zero;
//        }
//    }
}