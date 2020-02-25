using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playTentDoorMovement : MonoBehaviour
{
    public float speed = 2.0f;
    private float size = 100.0f;

    private SkinnedMeshRenderer skinny;
    private bool maxValue;
    private float time;

    void Start()
    {
        speed *= 100;
        skinny = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (maxValue)
        {
            skinny.SetBlendShapeWeight(0, size -= speed*Time.deltaTime);
            if (size <= 0)
                maxValue = false;
        }
        else
        {
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, size += speed*Time.deltaTime);
            if (size >= 100)
            {
                maxValue = true;
            }
        }
    }
}