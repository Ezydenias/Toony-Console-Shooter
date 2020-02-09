using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerDataBase;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("here");
        if (other.tag == "Player")
        {
            playerDataBase.GetComponent<PlayerActivator>().CharacterOne.SetActive(false);
        }
    }
}
