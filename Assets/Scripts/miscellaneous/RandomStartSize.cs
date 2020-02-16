using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStartSize : MonoBehaviour
{
    [Range(.01f, 2)] public float Minimum = .01f;
    [Range(.01f, 2)] public float Maximum = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        float i = Random.Range(Minimum, Maximum);
        transform.localScale = new Vector3(i, i, i);
    }
}