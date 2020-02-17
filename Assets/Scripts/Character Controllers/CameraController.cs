using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public float rotationSpeed;

    public Transform pivot;

    //public Transform gunCursorTargeter;
    //public Vector3 cursorOffset;
    //public float maxCursorDistance = 10;
    public float maxViewAngle;
    public float minViewAngle;
    public bool invertX;
    public bool invertY;
    public float cameraDistance = -10;
    public float smoothTime = 1;
    public float smoothTimeCammeraRecenter = .5f;
    public float cameraHitsOffsetX = .5f;
    public float cameraHitsOffsetY = .5f;
    public float verticalPlayerFollowDeadzone = 4f;
    public float CameraPlayerFollowDeadzone = .1f;
    public float playerHeight = 1;
    public float ladderFacingSmoothing = 0.5f;

    //private Transform target;
    private Vector3 offset;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //target = gameObject.transform;
    }

    //finds the next surface to position of the targeting cursor for the gun to show
    //private void gunCursor()
    //{
    //    RaycastHit[] hits;
    //    hits = Physics.RaycastAll(transform.position + cursorOffset, transform.forward, maxCursorDistance);


    //    float oldDistance = 100;
    //    float newDistance = 100;

    //    float cameraDistance = Vector3.Distance(transform.position, target.position);

    //    for (int i = 0; i < hits.Length; i++)
    //    {
    //        if (hits[i].collider.tag != "Player" && hits[i].collider.tag != "Cursor")
    //        {
    //            newDistance = hits[i].distance;
    //            //Aligns cursor with the hit surface, this gets replaced with every next hit that is closer
    //            //testing showed that the array of hits isn't built in a logical order
    //            if (newDistance < oldDistance && newDistance > cameraDistance)
    //            {
    //                gunCursorTargeter.rotation = Quaternion.LookRotation(hits[i].normal);
    //                gunCursorTargeter.transform.position = hits[i].point;
    //                oldDistance = newDistance;
    //            }

    //            //break;
    //        }
    //    }
    //}

    private void cameraCollsion()
    {
        Vector3 HitsOffsetX = new Vector3(cameraHitsOffsetX, 0, 0);
        Vector3 HitsOffsetY = new Vector3(0, cameraHitsOffsetY, 0);

        RaycastHit[] hitsOne;
        RaycastHit[] hitsTwo;
        RaycastHit[] hitsThree;
        RaycastHit[] hitsFour;
        hitsOne = Physics.RaycastAll(pivot.transform.position + HitsOffsetX + HitsOffsetY, transform.forward * (-1),
            -(cameraDistance * 2));
        hitsTwo = Physics.RaycastAll(pivot.transform.position - HitsOffsetX + HitsOffsetY, transform.forward * (-1),
            -(cameraDistance * 2));
        hitsThree = Physics.RaycastAll(pivot.transform.position + HitsOffsetX - HitsOffsetY, transform.forward * (-1),
            -(cameraDistance * 2));
        hitsFour = Physics.RaycastAll(pivot.transform.position - HitsOffsetX - HitsOffsetY, transform.forward * (-1),
            -(cameraDistance * 2));

        float oldDistance = cameraDistance;


        oldDistance = cameraCollisionHit(hitsOne, oldDistance);
        oldDistance = cameraCollisionHit(hitsTwo, oldDistance);
        oldDistance = cameraCollisionHit(hitsThree, oldDistance);
        oldDistance = cameraCollisionHit(hitsFour, oldDistance);


        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, oldDistance), smoothTime);
        //transform.localPosition=new Vector3(0, 0, oldDistance);
    }

    private float cameraCollisionHit(RaycastHit[] hits, float oldDistance)
    {
        float newDistance = cameraDistance;

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.layer != 8)
            {
                //Debug.Log(hits[i].collider.name);
                //Debug.DrawRay(transform.position, hits[i].point, Color.green);
                newDistance = hits[i].distance * (-1);
                if (newDistance > oldDistance && newDistance < 0)
                {
                    oldDistance = newDistance;
                }
            }
        }

        return oldDistance;
    }

    private void allignWithGround()
    {
        RaycastHit[] ground;
        ground = Physics.RaycastAll(target.transform.position, new Vector3(0, -1, 0), playerHeight);
        //Debug.Log("here");

        if (ground.Length >= 1)
        {
            // 

            //Debug.Log("the eagle as landed");
//            pivot.transform.localRotation=Quaternion.Euler(Vector3.Lerp(pivot.transform.localRotation.eulerAngles, new Vector3(0, 0, 0), smoothTimeCammeraRecenter));
//            pivot.transform.localRotation=Quaternion.Euler(new Vector3(0, 0, 0));
//            pivot.transform.localRotation=Quaternion.Lerp(pivot.localRotation, Quaternion.Euler(new Vector3(0, 0, 0)), smoothTimeCammeraRecenter);
            pivot.transform.localRotation = Quaternion.Lerp(pivot.localRotation, Quaternion.Euler(ground[0].normal),
                smoothTimeCammeraRecenter);
            //Debug.Log(ground[0].normal);
            // Debug.DrawRay(target.position, new Vector3(0, -1, 0), Color.green);
        }
    }


    private void LateUpdate()
    {
        var horizontal = Input.GetAxis("Mouse X") * rotationSpeed;
        var vertical = Input.GetAxis("Mouse Y") * rotationSpeed;
        var verticalPlayer = Input.GetAxis("Vertical") * rotationSpeed;


        //If Camera Stick isn't in used but a forward or backward motion ins in place it will allign with ground
        if (((-.1f * CameraPlayerFollowDeadzone) < vertical && (.1f * CameraPlayerFollowDeadzone) > vertical) &&
            ((-.1f * CameraPlayerFollowDeadzone) < horizontal && (.1f * CameraPlayerFollowDeadzone) > horizontal))
        {
            if ((-.1f * verticalPlayerFollowDeadzone) > verticalPlayer ||
                (.1f * verticalPlayerFollowDeadzone) < verticalPlayer)
            {
                allignWithGround();
            }
        }

        if (!target.GetComponent<PlayerController>().getOnLadder())
        {
            if (invertX)
                target.transform.Rotate(0, -horizontal* Time.deltaTime, 0);
            else
                target.transform.Rotate(0, horizontal* Time.deltaTime, 0);
        }
        else
        {
            target.transform.rotation = Quaternion.Lerp(target.transform.rotation,
                Quaternion.Euler(0, target.GetComponent<PlayerController>().getLadderOrientation().eulerAngles.y, 0),
                ladderFacingSmoothing);
        }

        if (invertY)
            pivot.Rotate(vertical* Time.deltaTime, 0, 0);
        else
            pivot.Rotate(-vertical* Time.deltaTime, 0, 0);

        ////Limit up/down camera rotation
        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180)
            pivot.localRotation = Quaternion.Euler(maxViewAngle, 0, 0);

        if (pivot.rotation.eulerAngles.x > 180 && pivot.rotation.eulerAngles.x < 360f - minViewAngle)
            pivot.localRotation = Quaternion.Euler(360f - minViewAngle, 0, 0);


        //var desiredYAngle = pivot.eulerAngles.y;
        var desiredXAngle = pivot.eulerAngles.x;
        var rotation = Quaternion.Euler(desiredXAngle, 0, 0);

        //transform.localPosition = new Vector3(0, 0, cameraDistance);

        cameraCollsion();
        // gunCursor();

        //if (transform.position.y < pivot.position.y)
        //    transform.position = new Vector3(transform.position.x, pivot.position.y - .5f, transform.position.z);


        transform.LookAt(pivot);
    }
}