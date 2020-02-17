using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwpGun : MonoBehaviour
{
    public GameObject gun1;
    public GameObject gun2;
    public GameObject gun3;
    public GameObject gun4;

    public int gun = 0;
    // Start is called before the first frame update

    private float lastButton = 0;

    // Update is called once per frame
    void Update()
    {
        float temp = Input.GetAxis("NextGun");

        if (temp != lastButton)
        {
            lastButton = temp;
            if (temp != 0)
            {
                {
                    gun += (int) temp;
                    if (gun < 0)
                    {
                        gun = 4;
                    }
                    if (gun > 4)
                    {
                        gun = 0;
                    }
                }
                gun1.SetActive(false);
                gun2.SetActive(false);
                gun3.SetActive(false);
                gun4.SetActive(false);
                switch (gun)
                {
                    case 1:
                        gun1.SetActive(true);
                        break;
                    case 2:
                        gun2.SetActive(true);
                        break;
                    case 3:
                        gun3.SetActive(true);
                        break;
                    case 4:
                        gun4.SetActive(true);
                        break;
                }
            }
        }

        Debug.Log(gun);
    }
}