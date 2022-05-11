using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class portalgun : MonoBehaviour
{
    public GameObject closeCorner;
    public GameObject farCorner;
    public GameObject boundingArea;
    private LineRenderer lineRenderer = null;
    public Portalgunshoot controls;
    private bool closebuttondown;
    private bool farbuttondown;
    private bool pointToTeleportButtonDown;
    public Transform playZoneOrigin;
    public Transform playerHead;
    public float playzoneX;
    public float playZoneZ;

    private bool farCornerWasShot = false;
    private bool closeCornerWasShot = false;

    public GameObject canvas;

    bool usePortals = true;

    public GameObject locationMarker;

    private AudioSource audioSource;
    public AudioClip portalShootClip;
    public AudioClip teleportWooshClip;

    void Start()
    {
        closeCorner.SetActive(false);
        farCorner.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        controls = new Portalgunshoot();

        controls.Player.SwitchControls.performed += ctx => switchControls();

        controls.Player.ShootClosePortal.performed += ctx => { if (usePortals) { closebuttondown = farbuttondown ^ true; } };

        controls.Player.ShootClosePortal.canceled += ctx => { if (usePortals) { ShootClose(); } } ;

        controls.Player.ShootFarPortal.performed += ctx => { if (usePortals) { farbuttondown = closebuttondown ^ true; } } ;

        controls.Player.ShootFarPortal.canceled += ctx => { if (usePortals) { ShootFar(); } };

        controls.Player.PointToTeleport.performed += ctx => { if (!usePortals) { pointToTeleportButtonDown = true; } };

        controls.Player.PointToTeleport.canceled += ctx => { if (!usePortals) { pointToTeleport(); } };

    }

    void switchControls()
    {
        usePortals = !usePortals;
        closeCorner.SetActive(false);
        farCorner.SetActive(false);
        locationMarker.SetActive(false);
        farCornerWasShot = false;
        closeCornerWasShot = false;

    }

    void pointToTeleport()
    {
        pointToTeleportButtonDown = false;
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.gameObject.tag == "Floor")
            {
                Vector3 relativePosition = playerHead.position - playZoneOrigin.position;
                relativePosition.y = 0;
                playZoneOrigin.transform.position = hit.point - relativePosition;
            }
        }

        Animator anim = canvas.GetComponent<Animator>();
        anim.Play("fade", 0, 0);

        audioSource.PlayOneShot(teleportWooshClip);
    }

    void ShootClose()
    {

        if(!closebuttondown)
        {
            return;
        }

        closebuttondown = false;

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, 100))
        {
            Collider[] collidingColliders = Physics.OverlapCapsule(new Vector3(hit.point.x, 0, hit.point.z), new Vector3(hit.point.x, 3, hit.point.z), 0.675f);
            bool obstructed = false;
            for (int a = 0; a < collidingColliders.Length; a = a + 1)
            {
                if (collidingColliders[a].transform.tag != "Player" && collidingColliders[a].transform.tag != "Floor")
                {
                    obstructed = true;
                    break;
                }
            }

            if (hit.collider.gameObject.tag == "Floor" && inPlayZone(hit.point) && !obstructed)
            {
                
                float oldY = closeCorner.transform.position.y;
                closeCorner.transform.position = new Vector3(hit.point.x, oldY, hit.point.z);
                Vector3 toOrigin = playZoneOrigin.position - closeCorner.transform.position;
                closeCorner.transform.forward = Quaternion.Euler(0, 135, 0) * toOrigin.normalized;
                farCorner.transform.rotation = closeCorner.transform.rotation;

                audioSource.PlayOneShot(portalShootClip);

                if (closeCorner.activeSelf && farCorner.activeSelf)
                {
                    closeCorner.SetActive(false);
                    farCorner.SetActive(false);
                    locationMarker.SetActive(true);
                    locationMarker.transform.position = closeCorner.transform.position;
                    closeCornerWasShot = true;
                    return;
                }
                
                if (!farCornerWasShot)
                {
                    closeCornerWasShot = true;

                    locationMarker.SetActive(true);
                    locationMarker.transform.position = closeCorner.transform.position;

                    return;
                }

                if (farCornerWasShot)
                {
                    closeCorner.SetActive(true);
                    farCorner.SetActive(true);
                    locationMarker.SetActive(false);
                    farCornerWasShot = false;
                    closeCornerWasShot = false;
                    return;
                }
            }


        }
    }

    void ShootFar()
    {

        if (!farbuttondown)
        {
            return;
        }

        farbuttondown = false;

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, 100))
        {
            Collider[] collidingColliders = Physics.OverlapCapsule(new Vector3(hit.point.x, 0, hit.point.z), new Vector3(hit.point.x, 3, hit.point.z), 0.675f);
            bool obstructed = false;
            for (int a = 0; a < collidingColliders.Length; a = a + 1)
            {
                if (collidingColliders[a].transform.tag != "Player" && collidingColliders[a].transform.tag != "Floor")
                {
                    obstructed = true;
                    break;
                }
            }
            if (hit.collider.gameObject.tag == "Floor" && inFarPlayZone(hit.point) && !obstructed)
            {

                float oldY = farCorner.transform.position.y;
                farCorner.transform.position = new Vector3(hit.point.x, oldY, hit.point.z);
                farCorner.transform.rotation = closeCorner.transform.rotation;

                audioSource.PlayOneShot(portalShootClip);

                if (farCorner.activeSelf && closeCorner.activeSelf)
                {
                    farCorner.SetActive(false);
                    closeCorner.SetActive(false);
                    locationMarker.SetActive(true);
                    locationMarker.transform.position = farCorner.transform.position;
                    farCornerWasShot = true;
                    return;
                }

                if (!closeCornerWasShot)
                {
                    farCornerWasShot = true;
                    locationMarker.SetActive(true);
                    locationMarker.transform.position = farCorner.transform.position;
                    return;
                }

                if (closeCornerWasShot)
                {
                    farCorner.SetActive(true);
                    closeCorner.SetActive(true);
                    locationMarker.SetActive(false);
                    farCornerWasShot = false;
                    closeCornerWasShot = false;
                    return;
                }

            }


        }
    }


    // Update is called once per frame
    void Update()
    {
        boundingArea.SetActive(false);
        if (!closebuttondown && !farbuttondown && !pointToTeleportButtonDown)
        {
            lineRenderer.enabled = false;
            return;
        }

        lineRenderer.enabled = true;
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        lineRenderer.material.color = Color.red;
        bool hitSomething = Physics.Raycast(ray, out hit, 100);

        if (closebuttondown)
        { 
            if (hitSomething && hit.collider.gameObject.tag == "Floor" && inPlayZone(hit.point))
            {
                Collider[] collidingColliders = Physics.OverlapCapsule(new Vector3(hit.point.x, 0, hit.point.z), new Vector3(hit.point.x, 3, hit.point.z), 0.675f);
                boundingArea.SetActive(true);
                boundingArea.transform.position = hit.point;
                bool obstructed = false;
                for (int a = 0; a < collidingColliders.Length; a = a + 1)
                {
                    if(collidingColliders[a].transform.tag != "Player" && collidingColliders[a].transform.tag != "Floor")
                    {
                        obstructed = true;
                        break;
                    }
                }

                lineRenderer.material.color = obstructed ? Color.red : Color.green;
            }
        } else if (farbuttondown)
        {
            if (hitSomething && hit.collider.gameObject.tag == "Floor" && inFarPlayZone(hit.point))
            {
                Collider[] collidingColliders = Physics.OverlapCapsule(new Vector3(hit.point.x, 0, hit.point.z), new Vector3(hit.point.x, 3, hit.point.z), 0.675f);
                boundingArea.SetActive(true);
                boundingArea.transform.position = hit.point;
                if (collidingColliders.Length == 1)
                {
                    lineRenderer.material.color = Color.green;
                }
            }
        }
        else if (pointToTeleportButtonDown)
        {
            if (hitSomething && hit.collider.gameObject.tag == "Floor")
            {
                lineRenderer.material.color = Color.blue;
            }
        }


        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + 100 * transform.forward);
    }

    bool inPlayZone(Vector3 point)
    {
        return Mathf.Abs(point.x - playZoneOrigin.position.x) < playzoneX && Mathf.Abs(point.z - playZoneOrigin.position.z) < playZoneZ;
    }

    bool inFarPlayZone(Vector3 point)
    {
        return Mathf.Abs(point.x - playZoneOrigin.position.x) > 2 || Mathf.Abs(point.z - playZoneOrigin.position.z) > 2;
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
