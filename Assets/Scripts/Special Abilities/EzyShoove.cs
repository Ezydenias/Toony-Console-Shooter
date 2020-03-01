using System.Collections;
using System.Collections.Generic;
using GameEnumSpace;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class EzyShoove : MonoBehaviour
{
    public GameObject playerEmpty;
    public PlayerCharacter character;

    // Start is called before the first frame update
    private CharacterController controller;
    private bool pushing = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (!playerEmpty)
            playerEmpty = GameObject.Find("Player Empty");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerEmpty.GetComponent<CharacterChanger>().activePlayer == character && pushing == true)
        {
            Debug.Log("is pushing");
            Vector3 velocity = playerEmpty.GetComponent<CharacterChanger>().getCurrentCharacter()
                .GetComponent<CharacterController>()
                .velocity;
            Debug.Log(velocity);
            velocity.Set(velocity.x, controller.velocity.y, velocity.z);
            controller.Move(velocity * Time.deltaTime);
            //            transform.position.Set(velocity.y*Time.deltaTime,transform.position.y);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (playerEmpty.GetComponent<CharacterChanger>().activePlayer == character && other.tag == "Player")
            pushing = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (playerEmpty.GetComponent<CharacterChanger>().activePlayer == character && other.tag == "Player")
            pushing = false;
    }
}