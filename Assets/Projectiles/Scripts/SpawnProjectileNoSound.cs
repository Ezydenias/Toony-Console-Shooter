using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectileNoSound : MonoBehaviour
{
    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();
    public float fireOffset;
    public Transform targeterCursor;
    public bool player = false;
    public float targetingError = .1f;

    private GameObject effectToSpawn;
    private float timeToFire = 0;

    // Start is called before the first frame update
    void Start()
    {
        effectToSpawn = vfx[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            if (Input.GetButton("Fire1") && Time.time >= timeToFire)
            {
                int i = Random.Range(0, vfx.Count);
                effectToSpawn = vfx[i];
                timeToFire = Time.time + 1 / effectToSpawn.GetComponent<ProjectileMove>().fireRate;
                SpawnVFX(effectToSpawn);
            }
        }
    }

    private void SpawnVFX(GameObject effectToSpawn)
    {
        GameObject vfx;
        //GameObject vfxSound;

        if (firePoint != null)
        {
            float Error = targetingError *
                          (Vector3.Distance(firePoint.transform.position, targeterCursor.position) / 10);
            vfx = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
            vfx.transform.LookAt(targeterCursor.position + (new Vector3(Random.Range(-Error, Error),
                                     Random.Range(-Error, Error), Random.Range(-Error, Error))));
        }
        else
        {
            Debug.Log("No Fire Point");
        }
    }
}