using System.Collections;
using System.Collections.Generic;
using GameEnumSpace;
using UnityEngine;

public class CharacterChanger : MonoBehaviour
{
    public PlayerCharacter activePlayer = PlayerCharacter.Ezy;
    public Themes currentTheme = Themes.Default;
    [Space(20)] public GameObject gunEmpty;
    [Space(40)] public GameObject EzyHand;
    public GameObject Ezy;
    public GameObject EzyPlain;
    public GameObject EzyEgyptian;
    [Space(20)] public GameObject BeekoHand;
    public GameObject Beeko;
    public GameObject BeekoPlain;
    public GameObject BeekoEgyptian;

//    private PlayerController playerController;
//    private CharacterController controller;
    private Vector3 lastPosition;

    private Quaternion lastRotation;

    // Start is called before the first frame update
    void Start()
    {
        if (!gunEmpty)
        {
            gunEmpty = GameObject.Find("Gun Empty");
        }

//        playerController = GetComponent<PlayerController>();
//        controller = GetComponent<CharacterController>();
        changeCharacter();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void changeCharacter()
    {
        gunEmpty.transform.parent = transform;
        getCharacterPosition();

        //Deaktivate All Characters
        Ezy.active = false;
        Beeko.active = false;


        switch (activePlayer)
        {
            case PlayerCharacter.Ezy:

                changeToEzy();
                changeTheme(EzyPlain);


                break;
            case PlayerCharacter.Beeko:

                changeToBeeko();
                changeTheme(BeekoPlain);


                break;
        }
    }

    //Always disable character controller while changing the position;
    private void changeToEzy()
    {
        Ezy.active = true;
        Ezy.GetComponent<CharacterController>().enabled = false;
        Ezy.transform.position = lastPosition;
        Ezy.transform.rotation = lastRotation;
        gunEmpty.transform.position = EzyHand.transform.position;
        gunEmpty.transform.rotation = EzyHand.transform.rotation;
        gunEmpty.transform.parent = EzyHand.transform;
        gunEmpty.transform.Translate(-.1f, 0, 0);
        gunEmpty.transform.Rotate(0, 0, -3);
        Ezy.GetComponent<PlayerController>().triggerJump();
        Ezy.GetComponent<CharacterController>().enabled = true;
    }

    private void changeToBeeko()
    {
        Beeko.active = true;
        Beeko.GetComponent<CharacterController>().enabled = false;
        Beeko.transform.position = lastPosition;
        Beeko.transform.rotation = lastRotation;
        gunEmpty.transform.position = BeekoHand.transform.position;
        gunEmpty.transform.rotation = BeekoHand.transform.rotation;
        gunEmpty.transform.parent = BeekoHand.transform;
        gunEmpty.transform.Translate(-.1f, 0, 0);
        gunEmpty.transform.Rotate(0, 90, -3);
        Beeko.GetComponent<PlayerController>().triggerJump();
        Beeko.GetComponent<CharacterController>().enabled = true;
    }

    private void changeTheme(GameObject Plain /*, GameObject Egypt*/)
    {
        //Deaktivate all Skins
        Plain.active = false;

        //Activate Current Skin
        switch (currentTheme)
        {
            case Themes.Default:
                Plain.active = true;
                break;
            case Themes.Egyptian:
                break;
        }
    }

    private void getCharacterPosition()
    {
        lastPosition = Vector3.zero;
        lastRotation = Quaternion.identity;
        GameObject temp = getCurrentCharacter();
        if (temp != null)
        {
            lastPosition = temp.transform.position;
            lastRotation = temp.transform.rotation;
        }

//        Debug.Log(temp);
//        Debug.Log(lastPosition);
//        Debug.Log(lastRotation);
    }

    public GameObject getCurrentCharacter()
    {
        if (Ezy.activeSelf)
            return Ezy;
        if (Beeko.activeSelf)
            return Beeko;
        return null;
    }

    public Vector3 getPosition()
    {
        return lastPosition;
    }

    public Quaternion getRotation()
    {
        return lastRotation;
    }

    public bool checkPlayerControllerStat()
    {
        switch (activePlayer)
        {
            case PlayerCharacter.Ezy:
                if (!Ezy.GetComponent<EzyController>().getSwimming() &&
                    !Ezy.GetComponent<EzyController>().getOnLadder() &&
                    !Ezy.GetComponent<EzyController>().getShoving())
                    return true;
                    break;
            case PlayerCharacter.Beeko:
                if (!Beeko.GetComponent<PlayerController>().getSwimming() &&
                    !Beeko.GetComponent<PlayerController>().getOnLadder())
                    return true;
                break;
        }

        return false;
    }
}