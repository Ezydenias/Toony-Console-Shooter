using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHITTYblasteraAnImAtIoNscript : MonoBehaviour
{

    public Animator animator;

    public CharacterController Controller;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Controller.velocity.magnitude);
    }
}
