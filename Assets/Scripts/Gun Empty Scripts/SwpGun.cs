using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using GameEnumSpace;
using UnityEngine;

public class SwpGun : MonoBehaviour
{
    public GameObject gun1;
    public bool gun1Active = true;
    public GameObject string1;
    public bool string1Active = true;
    public GameObject blaster1;
    public bool blaster1Active = true;
    public GameObject bubbler1;
    public bool bubbler1Active = true;

    // Start is called before the first frame update

    private float lastButton = 0;
    private PlayerWeapons gun;

    private PlayerWeapons lastgun;
    // Update is called once per frame

    void Start()
    {
        lastgun = (PlayerWeapons) Enum.GetNames(typeof(PlayerWeapons)).Length - 1;
        Debug.Log(lastgun);
    }

    void Update()
    {
        float temp = Input.GetAxis("NextGun");

        if (temp != lastButton)
        {
            lastButton = temp;
            while (temp != 0)
            {
                {
                    gun += (int) temp;
                    if (gun < 0)
                    {
                        gun = lastgun;
                    }

                    if (gun > lastgun)
                    {
                        gun = 0;
                    }
                }
                gun1.SetActive(false);
                string1.SetActive(false);
                blaster1.SetActive(false);
                bubbler1.SetActive(false);
                switch (gun)
                {
                    case PlayerWeapons.Guns:
                        if (gun1Active)
                        {
                            gun1.SetActive(true);
                            temp = 0;
                        }

                        break;
                    case PlayerWeapons.Strings:
                        if (string1Active)
                        {
                            string1.SetActive(true);
                            temp = 0;
                        }

                        break;
                    case PlayerWeapons.Blaster:
                        if (blaster1Active)
                        {
                            blaster1.SetActive(true);
                            temp = 0;
                        }

                        break;

                    case PlayerWeapons.Bubblers:
                        if (bubbler1Active)
                        {
                            bubbler1.SetActive(true);
                            temp = 0;
                        }

                        break;
                }
            }
        }
    }
}