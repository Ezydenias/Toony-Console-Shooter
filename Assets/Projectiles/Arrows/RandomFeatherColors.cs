using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFeatherColors : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        switch ((int) Random.Range(0, 10))
        {
            case 1:
                GetComponent<MeshRenderer>().materials[3].color = Color.blue;
                break;
            case 2:
                GetComponent<MeshRenderer>().materials[3].color = Color.black;
                break;
            case 3:
                GetComponent<MeshRenderer>().materials[3].color = Color.cyan;
                break;
            case 4:
                GetComponent<MeshRenderer>().materials[3].color = Color.gray;
                break;
            case 5:
                GetComponent<MeshRenderer>().materials[3].color = Color.green;
                break;
            case 6:
                GetComponent<MeshRenderer>().materials[3].color = Color.magenta;
                break;
            case 7:
                GetComponent<MeshRenderer>().materials[3].color = Color.red;
                break;
            case 8:
                GetComponent<MeshRenderer>().materials[3].color = Color.white;
                break;
            case 9:
                GetComponent<MeshRenderer>().materials[3].color = Color.yellow;
                break;
        }
    }

    // Update is called once per frame
}