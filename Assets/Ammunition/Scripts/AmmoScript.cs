using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmmoScript : MonoBehaviour
{
    public AmmoTypes ammunition;

    public int clipSize;

    public bool dieOnPickUp = false;
    public AnimationCurve myCurve;

    private float floaty;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, 1);
        transform.position = new Vector3(transform.position.x, myCurve.Evaluate((Time.time % myCurve.length)),
            transform.position.z);
    }

    public void pickUp()
    {
        GetComponent<PopSound>().pop();
        if (dieOnPickUp)
            Destroy(this);
        gameObject.active = false;
    }
}