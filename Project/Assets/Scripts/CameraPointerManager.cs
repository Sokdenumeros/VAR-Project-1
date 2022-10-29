using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointerManager : MonoBehaviour
{
    public static CameraPointerManager Instance;
    [SerializeField] private GameObject pointer;
    [SerializeField] private GameObject teleporter;
    [SerializeField] private float maxDistancePointer = 4.5f;
    [Range(0,1)]
    [SerializeField] private float distancePointerToObject = 0.95f;

    public FadeScreen fadeScreen;

    private const float _maxDistance = 10f;
    private GameObject _gazedAtObject = null;

    private const string interactableTag = "Interactable";
    private const string environmentTag = "Environment";
    private const string floorTag = "Floor";
    private float scaleSize = 0.025f;
    private Color pointerColor;
    private GameObject currentPointerObj;
    private bool readyToTeleport = true;
    [HideInInspector]
    public Vector3 hitPoint;

    private void Awake()
    {
        if (Instance!=null && Instance!=this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        pointerColor = pointer.GetComponent<Renderer>().material.color;
    }

    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            hitPoint = hit.point;
            // GameObject detected in front of the camera.
            if (_gazedAtObject != hit.transform.gameObject)
            {
                // New GameObject.
                _gazedAtObject?.SendMessage("OnPointerExitXR", null, SendMessageOptions.DontRequireReceiver);
                _gazedAtObject = hit.transform.gameObject;
                _gazedAtObject.SendMessage("OnPointerEnterXR", null, SendMessageOptions.DontRequireReceiver);

                pointerColor.a = 1f;
                pointer.GetComponent<Renderer>().material.color = pointerColor;
            }
            switch (hit.transform.tag)
            {
                case interactableTag:
                    SwitchPointerObject(pointer);
                    pointer.GetComponent<Renderer>().material.color = Color.green;
                    if (Input.GetButtonDown("Fire1"))
                        hit.transform.gameObject.SendMessage("interaction", this, SendMessageOptions.DontRequireReceiver);
                        hit.transform.gameObject.SendMessage("OnPointerClickXR", this, SendMessageOptions.DontRequireReceiver);
                    break;
                case environmentTag:
                    SwitchPointerObject(pointer);
                    pointer.GetComponent<Renderer>().material.color = Color.white;
                    break;
                case floorTag:
                    SwitchPointerObject(teleporter);
                    break;
                default:
                    SwitchPointerObject(pointer);
                    pointer.GetComponent<Renderer>().material.color = Color.white;
                    break;
            }
            PointerOnGaze(hit.point);
        }
        else
        {
            // No GameObject detected in front of the camera.
            _gazedAtObject?.SendMessage("OnPointerExitXR", null, SendMessageOptions.DontRequireReceiver);
            _gazedAtObject = null;
            SwitchPointerObject(pointer);
            PointerOutGaze();
            pointerColor.a = 0.1f;
            pointer.GetComponent<Renderer>().material.color = pointerColor;
        }

        // Checks for screen touches.
        if (Google.XR.Cardboard.Api.IsTriggerPressed)
        {
            _gazedAtObject?.SendMessage("OnPointerClickXR", null, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void SwitchPointerObject(GameObject pointObject)
    {
        if (pointObject == pointer)
        {
            teleporter.GetComponent<Renderer>().enabled = false;
            teleporter.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
            pointer.SetActive(true);
            currentPointerObj = pointer;
        }
        if (pointObject == teleporter)
        {
            pointer.SetActive(false);
            teleporter.GetComponent<Renderer>().enabled = true;
            teleporter.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
            currentPointerObj = teleporter;
        }
    }

    IEnumerator TeleportRoutine(Vector3 location)
    {
        readyToTeleport = false;
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);
        Teleport(location);
        fadeScreen.FadeIn();
        readyToTeleport = true;
    }

    private void Teleport(Vector3 location)
    {
        transform.parent.gameObject.transform.position = new Vector3(location.x,
                transform.parent.gameObject.transform.position.y, location.z);

        transform.parent.gameObject.transform.eulerAngles = new Vector3(transform.parent.gameObject.transform.eulerAngles.x,
            teleporter.transform.eulerAngles.y, transform.parent.gameObject.transform.eulerAngles.z);

    }

    private void ControlTeleportRotation()
    {
        float yRotation = transform.eulerAngles.y;

        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            float joystickAngle = Mathf.Atan2(-(Input.GetAxis("Vertical")), Input.GetAxis("Horizontal")) * Mathf.Rad2Deg;
            teleporter.transform.eulerAngles = new Vector3(teleporter.transform.eulerAngles.x, (yRotation + joystickAngle + 90), teleporter.transform.eulerAngles.x);
        }
        else
        {
            teleporter.transform.eulerAngles = new Vector3(teleporter.transform.eulerAngles.x, yRotation, teleporter.transform.eulerAngles.x);
        }
    }

    private void PointerOnGaze(Vector3 hitPoint)
    {
        if (currentPointerObj == pointer)
        {
            float scale = scaleSize * Vector3.Distance(transform.position, hitPoint);
            pointer.transform.localScale = Vector3.one * scale;
            pointer.transform.parent.position = CalculatePointerPosition(transform.position, hitPoint, distancePointerToObject);
        }
        if (currentPointerObj == teleporter)
        {
            teleporter.transform.position = CalculatePointerPosition(transform.position, hitPoint, 1);
            ControlTeleportRotation();
            if (Input.GetButtonDown("Fire1") && TeleportHandler.destinationValid && readyToTeleport)
            {
                StartCoroutine(TeleportRoutine(hitPoint));
            }
        }
    }

    private void PointerOutGaze()
    {
        if (currentPointerObj == pointer)
        {
            pointer.transform.localScale = Vector3.one * 0.1f;
            pointer.transform.parent.transform.localPosition = new Vector3(0, 0, maxDistancePointer);
            pointer.transform.parent.parent.transform.rotation = transform.rotation;
        }
        if (currentPointerObj == teleporter)
        {
            teleporter.transform.localPosition = new Vector3(0, 0, maxDistancePointer);
        }
    }

    private Vector3 CalculatePointerPosition(Vector3 p0, Vector3 p1, float t)
    {
        float x = p0.x + t * (p1.x - p0.x);
        float y = p0.y + t * (p1.y - p0.y);
        float z = p0.z + t * (p1.z - p0.z);

        return new Vector3(x, y, z);
    }
}
