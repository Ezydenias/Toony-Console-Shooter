using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBow : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> Enable = new List<GameObject>();
    public List<GameObject> Disable = new List<GameObject>();
    void OnTriggerEnter(Collider other)
    {
        // Filter by using specific layers for this object and "others" instead of using tags
        if (other.name == "Ezy")
        {
            for (int i = 0; i < Enable.Count; i++)
            {
                Enable[i].SetActive(true);
            }

            for (int i = 0; i < Disable.Count; i++)
            {
                Disable[i].SetActive(false);
            }
        }
    }
}
