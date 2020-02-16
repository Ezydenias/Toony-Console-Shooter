using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopSound : MonoBehaviour
{
    public List<GameObject> vfxSound = new List<GameObject>();
    private GameObject effectToSound;
    public void pop()
    {
        if (vfxSound.Count > 0)
        {
            int i = Random.Range(0, vfxSound.Count);
            effectToSound = vfxSound[i];
            Instantiate(effectToSound, transform.position, Quaternion.identity);
        }
    }
}
