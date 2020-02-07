using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchAudioRandomly : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxPitch = 4;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(-maxPitch, maxPitch);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
