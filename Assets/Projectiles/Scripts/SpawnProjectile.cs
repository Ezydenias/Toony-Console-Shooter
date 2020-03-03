using System.Collections;
using System.Collections.Generic;
using GameEnumSpace;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();
    public List<GameObject> vfxSound = new List<GameObject>();
    public List<GameObject> vfxFlash = new List<GameObject>();
    public List<GameObject> emptyMag = new List<GameObject>();
    public float fireOffset;
    public Transform targeterCursor;
    public bool player = false;
    public float targetingError = .1f;
    public GameObject Character;
    public GameObject gunEmpty;
    public GameObject playerEmpty;
    public AmmoTypes ammo;

    private GameObject effectToSpawn;
    private GameObject effectToSound;
    private GameObject effectToFlash;
    private GameObject effectToClick;
    private float timeToFire = 0;


    // Start is called before the first frame update
    void Start()
    {
        if(!playerEmpty)
            playerEmpty=GameObject.Find("Player Empty");
        if (gunEmpty == null)
            gunEmpty = GameObject.Find("Gun Empty");
        if (!Character && player == true)
            Character = playerEmpty.GetComponent<CharacterChanger>().getCurrentCharacter();
        effectToFlash = null;
        effectToSound = null;
        effectToSpawn = vfx[0];
        if (vfxFlash.Count > 0)
            effectToFlash = vfxFlash[0];
        if (vfxSound.Count > 0)
            effectToSound = vfxSound[0];
        if (emptyMag.Count > 0)
            effectToClick = emptyMag[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!Character.activeSelf)
        {
            Character = playerEmpty.GetComponent<CharacterChanger>().getCurrentCharacter();
        }
//        Debug.Log(Character.name);
        if (player && playerEmpty.GetComponent<CharacterChanger>().checkPlayerControllerStat())
        {
            if (Input.GetButton("Fire1") && Time.time >= timeToFire && gunEmpty.GetComponent<AmmunitionInventory>().subAmmunition(ammo))
            {
                int i = 0;
                    if (vfxSound.Count > 0)
                    {
                        i = Random.Range(0, vfxSound.Count);
                        effectToSound = vfxSound[i];
                    }

                    if (vfxFlash.Count > 0)
                    {
                        i = Random.Range(0, vfxFlash.Count);
                        effectToFlash = vfxFlash[i];
                    }

                    timeToFire = Time.time + 1 / effectToSpawn.GetComponent<ProjectileMove>().fireRate;
                    SpawnVFX();

            }
            else if(Input.GetButtonDown("Fire1") && !gunEmpty.GetComponent<AmmunitionInventory>().magEmpty(ammo))
            {
                Debug.Log("now here");
                int i = 0;

                i = Random.Range(0, emptyMag.Count);
                effectToSound = emptyMag[i];

                clickVFX();
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
            float Error = targetingError *
                          (Vector3.Distance(firePoint.transform.position, targeterCursor.position) / 10);
            if (effectToSound != null)
                vfxSound = Instantiate(effectToSound, firePoint.transform.position, Quaternion.identity);

            //vfxFlash.transform.localPosition=new Vector3(0,-.1f,0);
            vfx = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
            vfx.transform.LookAt(targeterCursor.position + (new Vector3(Random.Range(-Error, Error),
                                     Random.Range(-Error, Error), Random.Range(-Error, Error))));

            if (effectToFlash != null)
            {
                vfxFlash = Instantiate(effectToFlash, firePoint.transform.position, Quaternion.identity);
                vfxFlash.transform.Rotate(Vector3.up, Random.Range(0, 360));
                vfxFlash.transform.parent = transform;
            }
        }
        else
        {
            Debug.Log("No Fire Point");
        }
    }

    private void clickVFX()
    {
        GameObject vfxClick;

        vfxClick = Instantiate(effectToClick, firePoint.transform.position, Quaternion.identity);
    }
}