using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerBeeko : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject Ezy;
    public GameObject Beeko;
    public GameObject BeekoHand;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("here");
        if (other.tag == "Player")
        {
            Beeko.SetActive(true);
            GameObject Gun = GameObject.Find("Da Gun");
            Gun.transform.position = BeekoHand.transform.position;

            Gun.transform.rotation = BeekoHand.transform.rotation;
            Gun.transform.parent = BeekoHand.transform;
            Gun.transform.Rotate(0,90,0);
            Ezy.SetActive(false);
          

//            Gun.transform.Translate(-.1f, 0, 0);

        }
    }
}
