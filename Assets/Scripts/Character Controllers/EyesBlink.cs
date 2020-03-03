using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesBlink : MonoBehaviour
{
    public float speed = 2.0f;
    public float minBlinkTime = 2.0f;
    public float maxBlinkTime = 10.0f;
    public float doubleBlinkLikelihood = 30.0f;
    private float size = 100.0f;
    private bool open = true;
    private bool blinking = false;
    private float timer;
    private float time;

    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (blinking)
        {
            if (open)
            {
                GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, size -= speed);
                if (size <= 0)
                    open = false;
            }
            else
            {
                GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, size += speed);
                if (size >= 100)
                {
                    float likelihood = Random.Range(0, 100);
                   // Debug.Log(likelihood);
                    if (likelihood > doubleBlinkLikelihood)
                    {
                        timer = Random.Range(minBlinkTime, maxBlinkTime);
                       // Debug.Log(timer);
                        time = 0;
                        blinking = false;
                    }
                    open = true;
                    
                }
            }
        }
        else
        {
            GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, size);
            time = time + 1 * Time.deltaTime;
            if (time >= timer)
                blinking = true;
        }
    }
}