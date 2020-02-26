using System;
using System.Collections;
using System.Collections.Generic;
using GameEnumSpace;
using UnityEngine;

public class tentChangerCharacter : MonoBehaviour
{
    public PlayerCharacter charakter;
    public bool enabled = true;
    [Range(1.01f, 10)] public float shrink = 1f;
    [Space(20)]
    public GameObject Character;
    public GameObject Tent;

    private bool shrinking = false;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (shrinking)
            changeAnimation();
    }

    void changeAnimation()
    {
        float newShrink= shrink*Time.deltaTime;
        transform.localScale -= new Vector3(newShrink, newShrink, newShrink);
        if (transform.localScale.y <= 0)
        {
            Tent.active = false;
            shrinking = false;
        }
    }

    void reset()
    {
        transform.localScale.Set(1, 1, 1);
        Character.active = true;
        Tent.active = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (enabled = true && other.gameObject.tag == "Player")
        {

            try
            {
                other.GetComponent<PlayerController>().triggerJump();
                other.GetComponent<CharacterChanger>().activePlayer = PlayerCharacter.Beeko;
                other.GetComponent<CharacterChanger>().changeCharacter();
                Debug.Log("player");
                Character.active = false;
                shrinking = true;
                enabled = false;
            }
            catch (DivideByZeroException e)
            {
                Character.active = true;
                shrinking = false;
                enabled = true;
                Debug.Log("not player");

            }
        }
    }
}