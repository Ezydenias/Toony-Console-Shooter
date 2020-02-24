using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;



public class enumtest : MonoBehaviour
{
    // Start is called before the first frame update
    public class PlayerList
    {
        public List<GameObject> vfx;
    }
    public List<PlayerList> assd = new List<PlayerList>();
    public AmmoTypes type;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Enum.GetNames(typeof(AmmoTypes)).Length);
        
        
    }
}


