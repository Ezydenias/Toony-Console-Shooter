﻿using System.Collections;
using System.Collections.Generic;
using GameEnumSpace;
using JetBrains.Annotations;
using UnityEngine;

public class PlaySoundOnMouseClick : MonoBehaviour
{
    public List<AudioSource> Sources;
    public GameObject Player;
    public GameObject playerEmpty;
    public GameObject gunEmpty;
    public AmmoTypes Ammo = AmmoTypes.Battery;

    // Start is called before the first frame update
    void Start()
    {
        if (!playerEmpty)
            playerEmpty = GameObject.Find("Player Empty");
        if (gunEmpty == null)
            gunEmpty = GameObject.Find("Gun Empty");
        Player = playerEmpty.GetComponent<CharacterChanger>().getCurrentCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Player)
        {
            Player = playerEmpty.GetComponent<CharacterChanger>().getCurrentCharacter();
        }
        if (!Player.GetComponent<PlayerController>().getSwimming() &&
            !Player.GetComponent<PlayerController>().getOnLadder())
        {
            if (Input.GetButtonDown("Fire1"))
            {
                foreach (var t in Sources)
                    t.Play();
            }
        }

        if (Input.GetButtonUp("Fire1")||!gunEmpty.GetComponent<AmmunitionInventory>().magEmpty(Ammo))
        {
            foreach (var t in Sources)
                t.Stop();
        }
    }
}