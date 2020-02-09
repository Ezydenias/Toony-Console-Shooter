using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActivator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject CharacterOne;
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("here");
        if (other.tag == "CharacterSwitch")
        {
           CharacterOne.SetActive(false);
        }
    }
}
