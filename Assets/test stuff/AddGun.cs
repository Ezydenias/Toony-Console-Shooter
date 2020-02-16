using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGun : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject EzyHand;
    public GameObject Gun;
    void Start()
    {
        GameObject Gun = GameObject.Find("Da Gun");
        Gun.transform.position = EzyHand.transform.position;
        
        Gun.transform.rotation = EzyHand.transform.rotation;
        Gun.transform.parent = EzyHand.transform;
        Gun.transform.Translate(-.1f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
