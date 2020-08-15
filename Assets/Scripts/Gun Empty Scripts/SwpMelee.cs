using System.Collections;
using System.Collections.Generic;
using GameEnumSpace;
using UnityEngine;

public class SwpMelee : MonoBehaviour
{
    public CharacterChanger PlayerEmpty;
    [Space(10)]
    public bool active = false;
    [Space(10)] public GameObject EzyMelee;
    public GameObject BeekoMelee;
    public GameObject AlunMelee;
    public GameObject NaluMelee;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerEmpty)
            PlayerEmpty = GameObject.Find("Player Empty").GetComponent<CharacterChanger>();

        if (active)
            ActivateMelee();
        else
            DeactivateMelee();
    }



    public void DeactivateMelee()
    {
        active = false;
        EzyMelee.SetActive(false);
        BeekoMelee.SetActive(false);
        AlunMelee.SetActive(false);
        NaluMelee.SetActive(false);
    }

    public void ActivateMelee()
    {
        active = true;
        switch (PlayerEmpty.activePlayer)
        {
            case PlayerCharacter.Ezy:
                EzyMelee.SetActive(true);
                break;
            case PlayerCharacter.Beeko:
                BeekoMelee.SetActive(true);
                break;
            case PlayerCharacter.Alun:
                AlunMelee.SetActive(true);
                break;
            case PlayerCharacter.Nalu:
                NaluMelee.SetActive(true);
                break;
        }
    }

    public void swapPlayer()
    {
        DeactivateMelee();
        ActivateMelee();
    }
}