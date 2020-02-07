using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SpawnProjectile : MonoBehaviour
{
    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();
    public List<GameObject> vfxSound = new List<GameObject>();
    public List<GameObject> vfxFlash = new List<GameObject>();
    public float fireOffset;
    public Transform targeterCursor;
    public bool player = false;
    public float targetingError = .1f;
    public float maxPitch = 4;
    public GameObject Character;

    private GameObject effectToSpawn;
    private GameObject effectToSound;
    private GameObject effectToFlash;
    private float timeToFire = 0;


    // Start is called before the first frame update
    void Start()
    {


        effectToFlash = vfxFlash[0];
        effectToSpawn = vfx[0];
        effectToSound = vfxSound[0];
    }
        // Update is called once per frame
    void Update()
    {
        if (player && !Character.GetComponent<PlayerController>().getSwimming() && !Character.GetComponent<PlayerController>().getOnLadder())
        {
            if (Input.GetButton("Fire1") && Time.time >= timeToFire)
            {
                int i = Random.Range(0, vfxSound.Count);
                effectToSound = vfxSound[i];
                i = Random.Range(0, vfxFlash.Count);
                effectToFlash = vfxFlash[i];
                timeToFire = Time.time + 1 / effectToSpawn.GetComponent<ProjectileMove>().fireRate;
                SpawnVFX();
            }
        }
    }

    private void SpawnVFX()
    {
        GameObject vfx;
        GameObject vfxSound;
        GameObject vfxFlash;

        if (firePoint != null)
        {
            float Error = targetingError*(Vector3.Distance(firePoint.transform.position,targeterCursor.position)/10);
            vfxSound = Instantiate(effectToSound, firePoint.transform.position, Quaternion.identity);
            vfxFlash = Instantiate(effectToFlash, firePoint.transform.position, Quaternion.identity);
            //vfxFlash.transform.localPosition=new Vector3(0,-.1f,0);
            vfx = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
            vfx.transform.LookAt(targeterCursor.position+(new Vector3(Random.Range(-Error, Error), Random.Range(-Error, Error), Random.Range(-Error, Error))));
            
            vfxFlash.transform.Rotate(Vector3.up, Random.Range(0, 360));
            vfxFlash.transform.parent = transform;
        }
        else
        {
            Debug.Log("No Fire Point");
        }
    }
}