using System.Collections;
using System.Collections.Generic;
using GameEnumSpace;
using UnityEngine;

public class CharacterChanger : MonoBehaviour
{
    public PlayerCharacter activePlayer = PlayerCharacter.Ezy;
    public Themes currentTheme = Themes.Default;
    [Space(20)]
    public GameObject gunEmpty;
    [Space(40)]
    public GameObject EzyHand;
    public GameObject EzyPlain;
    public GameObject EzyEgyptian;
    [Space(20)]
    public GameObject BeekoHand;
    public GameObject BeekoPlain;
    public GameObject BeekoEgyptian;
    // Start is called before the first frame update
    void Start()
    {
        if (!gunEmpty)
        {
            gunEmpty=GameObject.Find("Gun Empty");
        }
        changeCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeCharacter()
    {
        gunEmpty.transform.parent = transform;

        //Dissable all Characters
        //Ezy
        EzyPlain.active = false;
//        EzyEgyptian.active = false;
        //Beeko
        BeekoPlain.active = false;
//        BeekoEgyptian.active = false;

        switch (activePlayer)
        {
            case PlayerCharacter.Ezy:
                switch (currentTheme)
                {
                    case Themes.Default:
                        EzyPlain.active = true;
                        break;
                    case Themes.Egyptian:
                        break;
                }
                gunEmpty.transform.position = EzyHand.transform.position;
                gunEmpty.transform.rotation = EzyHand.transform.rotation;
                gunEmpty.transform.parent = EzyHand.transform;
                gunEmpty.transform.Translate(-.1f, 0, 0);
                gunEmpty.transform.Rotate(0, 0, -3);
                break;
            case PlayerCharacter.Beeko:
                switch (currentTheme)
                {
                    case Themes.Default:
                        BeekoPlain.active = true;
                        break;
                    case Themes.Egyptian:
                        break;
                }
                gunEmpty.transform.position = BeekoHand.transform.position;
                gunEmpty.transform.rotation = BeekoHand.transform.rotation;
                gunEmpty.transform.parent = BeekoHand.transform;
                gunEmpty.transform.Translate(-.1f, 0, 0);
                gunEmpty.transform.Rotate(0, 90, -3);
                break;
        }


    }
}
