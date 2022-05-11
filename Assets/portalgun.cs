using UnityEngine;

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

        controls.Player.SwitchControls.performed += ctx => SwitchControls();

        controls.Player.ShootClosePortal.performed += ctx => { if (usePortals) { closebuttondown = farbuttondown ^ true; } };
        controls.Player.ShootClosePortal.canceled += ctx => { if (usePortals) { ShootClose(); } } ;

        controls.Player.ShootFarPortal.performed += ctx => { if (usePortals) { farbuttondown = closebuttondown ^ true; } } ;
        controls.Player.ShootFarPortal.canceled += ctx => { if (usePortals) { ShootFar(); } };

        controls.Player.PointToTeleport.performed += ctx => { if (!usePortals) { pointToTeleportButtonDown = true; } };
        controls.Player.PointToTeleport.canceled += ctx => { if (!usePortals) { PointToTeleport(); } };
    }

    void SwitchControls()
    {
        usePortals = !usePortals;
        closeCorner.SetActive(false);
        farCorner.SetActive(false);
        locationMarker.SetActive(false);
        farCornerWasShot = false;
        closeCornerWasShot = false;
    }

    void PointToTeleport()
    {
        pointToTeleportButtonDown = false;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 100) && hit.collider.gameObject.CompareTag("Floor"))
        {
            Vector3 relativePosition = playerHead.position - playZoneOrigin.position;
            relativePosition.y = 0;
            playZoneOrigin.transform.position = hit.point - relativePosition;

            Animator anim = canvas.GetComponent<Animator>();
            anim.Play("fade", 0, 0);

            audioSource.PlayOneShot(teleportWooshClip);
        }
    }

    void ShootClose()
    {
        if (!closebuttondown) return;
        closebuttondown = false;

        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 100) && hit.collider.gameObject.CompareTag("Floor") && InPlayZone(hit.point))
        {
            Collider[] collidingColliders = Physics.OverlapCapsule(new Vector3(hit.point.x, 0, hit.point.z), new Vector3(hit.point.x, 3, hit.point.z), 0.675f);
            foreach (Collider collidingCollider in collidingColliders)
            {
                if (!collidingCollider.transform.CompareTag("Player") && !collidingCollider.transform.CompareTag("Floor")) return;
            }
 
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

    void ShootFar()
    {
        if (!farbuttondown) return;
        farbuttondown = false;

        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 100) && hit.collider.gameObject.CompareTag("Floor") && InFarPlayZone(hit.point))
        {
            Collider[] collidingColliders = Physics.OverlapCapsule(new Vector3(hit.point.x, 0, hit.point.z), new Vector3(hit.point.x, 3, hit.point.z), 0.675f);

            foreach (Collider collidingCollider in collidingColliders)
            {
                if (!collidingCollider.transform.CompareTag("Player") && !collidingCollider.transform.CompareTag("Floor")) return;
            }

            float oldY = farCorner.transform.position.y;
            farCorner.transform.SetPositionAndRotation(new Vector3(hit.point.x, oldY, hit.point.z), closeCorner.transform.rotation);

            audioSource.PlayOneShot(portalShootClip);

            if (farCorner.activeSelf && closeCorner.activeSelf)
            {
                farCorner.SetActive(false);
                closeCorner.SetActive(false);
                locationMarker.transform.position = farCorner.transform.position;
                locationMarker.SetActive(true);
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
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + 100 * transform.forward);
        lineRenderer.material.color = Color.red;

        Ray ray = new Ray(transform.position, transform.forward);
        bool hitSomething = Physics.Raycast(ray, out RaycastHit hit, 100);

        if (closebuttondown)
        { 
            if (hitSomething && hit.collider.gameObject.CompareTag("Floor") && InPlayZone(hit.point))
            {
                Collider[] collidingColliders = Physics.OverlapCapsule(new Vector3(hit.point.x, 0, hit.point.z), new Vector3(hit.point.x, 3, hit.point.z), 0.675f);
                boundingArea.SetActive(true);
                boundingArea.transform.position = hit.point;
                foreach (Collider collidingCollider in collidingColliders)
                {
                    if (!collidingCollider.transform.CompareTag("Player") && !collidingCollider.transform.CompareTag("Floor")) return;
                }

                lineRenderer.material.color = Color.green;
            }
        } else if (farbuttondown)
        {
            if (hitSomething && hit.collider.gameObject.CompareTag("Floor") && InFarPlayZone(hit.point))
            {
                Collider[] collidingColliders = Physics.OverlapCapsule(new Vector3(hit.point.x, 0, hit.point.z), new Vector3(hit.point.x, 3, hit.point.z), 0.675f);
                boundingArea.SetActive(true);
                boundingArea.transform.position = hit.point;
                if (collidingColliders.Length == 1)
                {
                    lineRenderer.material.color = Color.green;
                }
            }
        } else if (pointToTeleportButtonDown)
        {
            if (hitSomething && hit.collider.gameObject.CompareTag("Floor"))
            {
                lineRenderer.material.color = Color.blue;
            }
        }
    }

    bool InPlayZone(Vector3 point)
    {
        return Mathf.Abs(point.x - playZoneOrigin.position.x) < playzoneX && Mathf.Abs(point.z - playZoneOrigin.position.z) < playZoneZ;
    }

    bool InFarPlayZone(Vector3 point)
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
