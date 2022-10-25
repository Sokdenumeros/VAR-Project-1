using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class CameraPointerManager : MonoBehaviour
{
    [SerializeField] private GameObject pointer;
    [SerializeField] private float maxDistancePointer = 4.5f;
    [Range(0,1)]
    [SerializeField] private float distancePointerToObject = 0.95f;

    private const float _maxDistance = 10f;
    private GameObject _gazedAtObject = null;

    private const string interactableTag = "Interactable";
    private const string environmentTag = "Environment";
    private const string floorTag = "Floor";
    private float scaleSize = 0.025f;
    private Color pointerColor;
    private bool readyToTeleport = true;


    private void Start()
    {
        GazeManager.Instance.OnGazeSelection += GazeSelection;
        pointerColor = pointer.GetComponent<Renderer>().material.color;
    }

    private void GazeSelection()
    {
        _gazedAtObject?.SendMessage("OnPointerClick", null, SendMessageOptions.DontRequireReceiver);
    }

    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            // GameObject detected in front of the camera.
            if (_gazedAtObject != hit.transform.gameObject)
            {
                // New GameObject.
                _gazedAtObject?.SendMessage("OnPointerExit", null, SendMessageOptions.DontRequireReceiver);
                _gazedAtObject = hit.transform.gameObject;
                _gazedAtObject.SendMessage("OnPointerEnter", null, SendMessageOptions.DontRequireReceiver);

                pointerColor.a = 1f;
                pointer.GetComponent<Renderer>().material.color = pointerColor;
            }

            PointerOnGaze(hit.point);

            switch (hit.transform.tag)
            {
                case interactableTag:
                    GazeManager.Instance.StartGazeSelection();
                    pointer.GetComponent<Renderer>().material.color = Color.green;
                    if (Input.GetButtonDown("Fire1"))
                        hit.transform.gameObject.SendMessage("interaction", this, SendMessageOptions.DontRequireReceiver);
                    break;
                case environmentTag:
                    GazeManager.Instance.CancelGazeSelection();
                    pointer.GetComponent<Renderer>().material.color = Color.white;
                    break;
                case floorTag:
                    pointer.GetComponent<Renderer>().material.color = Color.blue;
                    GazeManager.Instance.CancelGazeSelection();
                    if (Input.GetButtonDown("Fire1")&& readyToTeleport)
                    {
                        Teleport(hit.point);
                    }
                    break;
                default:
                    GazeManager.Instance.CancelGazeSelection();
                    pointer.GetComponent<Renderer>().material.color = Color.white;
                    break;
            }
        }
        else
        {
            // No GameObject detected in front of the camera.
            _gazedAtObject?.SendMessage("OnPointerExit", null, SendMessageOptions.DontRequireReceiver);
            _gazedAtObject = null;
            PointerOutGaze();
            pointerColor.a = 0.5f;
            pointer.GetComponent<Renderer>().material.color = pointerColor;
        }

        // Checks for screen touches.
        if (Google.XR.Cardboard.Api.IsTriggerPressed)
        {
            _gazedAtObject?.SendMessage("OnPointerClick", null, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void Teleport(Vector3 location)
    {
        transform.parent.gameObject.transform.position = new Vector3(location.x,
                transform.parent.gameObject.transform.position.y, location.z);

    }


    // Modifies pointer while gazing at Objects
    private void PointerOnGaze(Vector3 hitPoint)
    {
        float scale = scaleSize * Vector3.Distance(transform.position, hitPoint);
        pointer.transform.localScale = Vector3.one * scale;
        pointer.transform.parent.position = CalculatePointerPosition(transform.position, hitPoint, distancePointerToObject);
    }

    private void PointerOutGaze()
    {
        pointer.transform.localScale = Vector3.one * 0.1f;
        pointer.transform.parent.transform.localPosition = new Vector3(0, 0, maxDistancePointer);
        pointer.transform.parent.parent.transform.rotation = transform.rotation;
    }

    private Vector3 CalculatePointerPosition(Vector3 p0, Vector3 p1, float t)
    {
        float x = p0.x + t * (p1.x - p0.x);
        float y = p0.y + t * (p1.y - p0.y);
        float z = p0.z + t * (p1.z - p0.z);

        return new Vector3(x, y, z);
    }
}
