using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SpawnArrow : MonoBehaviour
{
    public GameObject firePoint;
    public List<GameObject> Arrows = new List<GameObject>();
    public List<GameObject> DrawSound = new List<GameObject>();
    public List<GameObject> ShotSound = new List<GameObject>();
    public Transform targeterCursor;
    public bool player = false;
    public GameObject Character;
    public GameObject playerEmpty;
    public float drawSpeed = 1;

    private float fireSpeed;
    private GameObject effectToSpawn;
    private GameObject SoundToDraw;
    private GameObject SoundToShot;
    private bool loaded = false;
    private GameObject currentArrow;
    private SkinnedMeshRenderer blendShape;
    private float size = .0f;
    private bool shooting = false;


    // Start is called before the first frame update
    void Start()
    {
        if (!playerEmpty)
            playerEmpty = GameObject.Find("Player Empty");
        if (!Character && player == true)
            Character = playerEmpty.GetComponent<CharacterChanger>().getCurrentCharacter();
        effectToSpawn = Arrows[0];
        SoundToShot = ShotSound[0];
        SoundToDraw = DrawSound[0];
        blendShape = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Character.activeSelf)
        {
            Character = playerEmpty.GetComponent<CharacterChanger>().getCurrentCharacter();
        }
        if (shooting)
        {
            Shoot();
        }
        else if (!loaded)
        {
            Spawn();
        }
        else if (player && !Character.GetComponent<PlayerController>().getSwimming() &&
                 !Character.GetComponent<PlayerController>().getOnLadder())
        {
            if (Input.GetButton("Fire1") && loaded)
            {
                ShootArrow();
            }
        }
    }

    private void Spawn()
    {
        if (size >= 100)
        {
            currentArrow = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
            currentArrow.transform.parent = transform;
            currentArrow.transform.LookAt(targeterCursor.position);
            loaded = true;
        }
        else
        {
            size = size + drawSpeed * Time.deltaTime;
            blendShape.SetBlendShapeWeight(0, size);
        }
    }

    private void ShootArrow()
    {
        if (firePoint != null)
        {
            if (currentArrow)
            {
                Instantiate(SoundToShot, firePoint.transform.position, Quaternion.identity);
                currentArrow.transform.parent = null;
                currentArrow.GetComponent<DestroyByTime>().enabled = true;
                currentArrow.GetComponent<ProjectileMove>().enabled = true;
                fireSpeed = currentArrow.GetComponent<ProjectileMove>().speed;
                shooting = true;
            }
            else
            {
                loaded = false;
            }
        }
        else
        {
            Debug.Log("No Fire Point");
        }
    }

    private void Shoot()
    {
        if (size > 0)
        {
            size = size - fireSpeed * Time.deltaTime * 100;
            blendShape.SetBlendShapeWeight(0, size);
        }
        else
        {
            shooting = false;
            loaded = false;
            Instantiate(SoundToDraw, firePoint.transform.position, Quaternion.identity);
        }
    }
}