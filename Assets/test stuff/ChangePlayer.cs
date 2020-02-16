using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Beeko;
    public GameObject Ezy;
    public GameObject EzyHand;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("here");
        if (other.tag == "Player")
        {
            
            Ezy.SetActive(true);
            GameObject Gun = GameObject.Find("Da Gun");
            Gun.transform.position = EzyHand.transform.position;

            Gun.transform.rotation = EzyHand.transform.rotation;
            Gun.transform.parent = EzyHand.transform;
            Gun.transform.Translate(-.1f, 0, 0);
            Beeko.SetActive(false);


        }
    }
}
