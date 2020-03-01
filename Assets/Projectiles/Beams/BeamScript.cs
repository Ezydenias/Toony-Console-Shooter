using System.Collections;
using System.Collections.Generic;
using GameEnumSpace;
using UnityEngine;

[RequireComponent(typeof(LineAlignment))]
public class BeamScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int reflections;
    public float maxLength;

    public Transform targeterCursor;
    public float damagePerSecond = 100;
    public float energyPerSecond = 5;
    public GameObject Player;
    public GameObject playerEmpty;

    public GameObject gunEmpty;
    public List<GameObject> emptyMag = new List<GameObject>();
    public AmmoTypes Ammo;

    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;
    private float timeElapsed = 0f;
    private GameObject effectToClick;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        if (!playerEmpty)
            playerEmpty = GameObject.Find("Player Empty");
        energyPerSecond = 1 / energyPerSecond;
        if (gunEmpty == null)
            gunEmpty = GameObject.Find("Gun Empty");
        if (emptyMag.Count > 0)
            effectToClick = emptyMag[0];
        Player = playerEmpty.GetComponent<CharacterChanger>().getCurrentCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Player.activeSelf)
        {
            Player = playerEmpty.GetComponent<CharacterChanger>().getCurrentCharacter();
        }
        if (!Player.GetComponent<PlayerController>().getSwimming() &&
            !Player.GetComponent<PlayerController>().getOnLadder())
        {
            if (Input.GetButton("Fire1") && gunEmpty.GetComponent<AmmunitionInventory>().magEmpty(Ammo))
            {
                lineRenderer.enabled = true;
                transform.LookAt(targeterCursor.position);

                ray = new Ray(transform.position, transform.forward);

                lineRenderer.positionCount = 1;
                lineRenderer.SetPosition(0, transform.position);
                float remainingLength = maxLength;

                for (int i = 0; i < reflections; i++)
                {
                    if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
                    {
                        lineRenderer.positionCount++;
                        lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                        remainingLength -= Vector3.Distance(ray.origin, hit.point);


                        if (hit.collider.gameObject.layer == 8)
                        {
                            ray = new Ray(hit.point, ray.direction);
                        }
                        else
                        {
                            ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                            if (hit.collider.tag != "Reflective")
                            {
                                if (hit.collider.tag == "Enemy")
                                {
                                    hit.collider.gameObject.GetComponent<EnemyStatus>().life -=
                                        damagePerSecond * Time.deltaTime;
                                    if (hit.collider.GetComponent<EnemyStatus>().life < 0)
                                        Destroy(hit.collider.gameObject);
                                }

                                break;
                            }
                        }
                    }
                    else
                    {
                        lineRenderer.positionCount++;
                        lineRenderer.SetPosition(lineRenderer.positionCount - 1,
                            ray.origin + ray.direction * remainingLength);
                    }
                }

                timeElapsed += Time.deltaTime;
                if (timeElapsed >= energyPerSecond)
                {
                    gunEmpty.GetComponent<AmmunitionInventory>().subAmmunition(Ammo);
                    timeElapsed = 0;
                }

            }
            else
            {
                lineRenderer.enabled = false;
                if (Input.GetButtonDown("Fire1"))
                    clickVFX();
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    private void clickVFX()
    {
        GameObject vfxClick;

        vfxClick = Instantiate(effectToClick, transform.position, Quaternion.identity);
    }
}