using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using GameEnumSpace;
using UnityEngine;

public class SwpGun : MonoBehaviour
{
    public SwpMelee MeeleEmpty;
    [Space(10)]
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
    public PlayerWeapons gun;
    private PlayerWeapons lastgun;
    // Update is called once per frame

    void Start()
    {
        lastgun = (PlayerWeapons) Enum.GetNames(typeof(PlayerWeapons)).Length - 1;
        if (!MeeleEmpty)
            MeeleEmpty = GameObject.Find("Player Empty").GetComponent<SwpMelee>();
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
                
                deactivateAll();
                Debug.Log(gun);
                switch (gun)
                {
                    case PlayerWeapons.Hand:
                        MeeleEmpty.ActivateMelee();
                        temp = 0;
                        break;
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

    private void deactivateAll()
    {
        MeeleEmpty.DeactivateMelee();
        gun1.SetActive(false);
        string1.SetActive(false);
        blaster1.SetActive(false);
        bubbler1.SetActive(false);
    }

    public PlayerWeapons getCurrentGun()
    {
        return gun;
    }
}