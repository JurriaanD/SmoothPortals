using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class portalgunleft : MonoBehaviour
{
    public GameObject corner;
    public GameObject othecorner;
    private LineRenderer lineRenderer = null;
    public Portalgunshootleft controls;
    private bool buttondown;
    public Transform playZoneOrigin;
    public float playzoneX;
    public float playZoneZ;

    // Start is called before the first frame update
    void Start()
    {
        corner.SetActive(false);
    }

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        controls = new Portalgunshootleft();

        controls.Player.ShootPortal.performed += ctx => buttondown = true;

        controls.Player.ShootPortal.canceled += ctx => Shoot();

    }

    void Shoot()
    {

        buttondown = false;

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.gameObject.tag == "Floor" && inPlayZone(hit.point))
            {
                
                float oldY = corner.transform.position.y;
                corner.transform.position = new Vector3(hit.point.x, oldY, hit.point.z);
                Vector3 toOrigin = playZoneOrigin.position - corner.transform.position;
                corner.transform.forward = Quaternion.Euler(0, 135, 0) * toOrigin.normalized;
                corner.SetActive(false);
                othecorner.SetActive(false);
            }


        }
    }


    // Update is called once per frame
    void Update()
    {
        if (buttondown)
        {
            lineRenderer.enabled = true;
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            lineRenderer.material.color = Color.red;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (inPlayZone(hit.point))
                {
                    lineRenderer.material.color = Color.green;
                } 
            }

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + 100 * transform.forward);

        }
        else
        {
            lineRenderer.enabled = false;
        }


    }

    bool inPlayZone(Vector3 point)
    {
        return Mathf.Abs(point.x - playZoneOrigin.position.x) < playzoneX && Mathf.Abs(point.z - playZoneOrigin.position.z) < playZoneZ;
    }


    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

}

