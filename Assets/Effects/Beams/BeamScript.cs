﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineAlignment))]
public class BeamScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int reflections;
    public float maxLength;

    public Transform targeterCursor;
    public float damagePerSecond = 100;
    public GameObject Player;


    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!Player.GetComponent<PlayerController>().getSwimming() &&
            !Player.GetComponent<PlayerController>().getOnLadder())
        {
            if (Input.GetButton("Fire1"))
            {
                lineRenderer.enabled = true;
                transform.LookAt(targeterCursor.position);

                ray = new Ray(transform.position, transform.forward);

                lineRenderer.positionCount = 1;
                lineRenderer.SetPosition(0, transform.position);
                float remainingLength = maxLength;

                for (int i = 0; i < reflections; i++)
                {
                    if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
                    {
                        lineRenderer.positionCount++;
                        lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                        remainingLength -= Vector3.Distance(ray.origin, hit.point);


                        if (hit.collider.gameObject.layer == 8)
                        {
                            ray = new Ray(hit.point, ray.direction);
                        }
                        else
                        {
                            ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                            if (hit.collider.tag != "Reflective")
                            {
                                if (hit.collider.tag == "Enemy")
                                {
                                    hit.collider.gameObject.GetComponent<EnemyStatus>().life -=
                                        damagePerSecond * Time.deltaTime;
                                    if (hit.collider.GetComponent<EnemyStatus>().life < 0)
                                        Destroy(hit.collider.gameObject);
                                }

                                break;
                            }
                        }
                    }
                    else
                    {
                        lineRenderer.positionCount++;
                        lineRenderer.SetPosition(lineRenderer.positionCount - 1,
                            ray.origin + ray.direction * remainingLength);
                    }
                }
            }
            else
            {
                lineRenderer.enabled = false;
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
}