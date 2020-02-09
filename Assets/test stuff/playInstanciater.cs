using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playInstanciater : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject myPrefab1;
    public GameObject myPrefab2;

    void Start()
    {
        Instantiate(myPrefab1, new Vector3(0, 0, 0), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("here");
        if (other.tag == "CharacterSwitch")
        {
            Debug.Log("here");
            Destroy(myPrefab1);
        }
    }
}
