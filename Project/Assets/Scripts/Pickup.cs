using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Vector3 relative_pos;
    private CameraPointerManager cam;
    private Color initialColor;
    private Color transparentColor;

    // Start is called before the first frame update
    void Start()
    {
        initialColor = GetComponent<Renderer>().material.color;
        transparentColor = initialColor;
        transparentColor.a = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (cam != null) {
            if (Input.GetButtonUp("Fire1"))
            {
                drop();
            }
            else {
                Vector3 targetposition = cam.transform.position + cam.transform.right.normalized * relative_pos.x + cam.transform.up.normalized * relative_pos.y + cam.transform.forward.normalized * relative_pos.z;
                this.GetComponent<Rigidbody>().velocity = (targetposition - transform.position) * 10;
            }
            //transform.position = targetposition;
            //this.GetComponent<Rigidbody>().angularVelocity = (cam.transform.parent.transform.eulerAngles - (transform.eulerAngles*3/180)) * 10;
            //transform.Translate(position-transform.position);
        }
    }

    public void interaction(CameraPointerManager c) {
        if (cam != null) { 
            drop();
            return;
        }
        GetComponent<Renderer>().material.color = transparentColor;
        cam = c;
        Vector3 dif = transform.position - cam.transform.position;
        relative_pos.x = Vector3.Dot(dif, cam.transform.right.normalized);
        relative_pos.y = Vector3.Dot(dif, cam.transform.up.normalized);
        relative_pos.z = Vector3.Dot(dif, cam.transform.forward.normalized);
    }

    private void drop() {
        GetComponent<Renderer>().material.color = initialColor;
        cam = null;
    }
}
