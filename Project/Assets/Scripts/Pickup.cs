using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Vector3 relative_pos;
    private CameraPointerManager cam;
    private Color initialColor;
    private Color transparentColor;
    public static Color currentColor;
    private int ax;

    // Start is called before the first frame update
    void Start()
    {
        ax = 0;
        initialColor = GetComponent<Renderer>().material.color;
        transparentColor = initialColor;
        currentColor = initialColor;
        transparentColor.a = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire2")) ax = (ax + 1) % 3;
        if (cam != null) {
            if (Input.GetButtonUp("Fire1"))
            {
                drop();
            }
            else {
                if (ax == 0) transform.Rotate(0, Input.GetAxis("Horizontal")*Time.deltaTime * 100, 0);
                if (ax == 1) transform.Rotate(Input.GetAxis("Horizontal") * Time.deltaTime * 100, 0,0);
                if (ax == 2) transform.Rotate(0,0, Input.GetAxis("Horizontal") * Time.deltaTime*100);

                relative_pos += relative_pos.normalized*(Input.GetAxisRaw("Vertical")*Time.deltaTime*8);
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
        currentColor = transparentColor;
        cam = c;
        Vector3 dif = transform.position - cam.transform.position;
        relative_pos.x = Vector3.Dot(dif, cam.transform.right.normalized);
        relative_pos.y = Vector3.Dot(dif, cam.transform.up.normalized);
        relative_pos.z = Vector3.Dot(dif, cam.transform.forward.normalized);
    }

    private void drop() {
        GetComponent<Renderer>().material.color = initialColor;
        transparentColor = initialColor;
        cam = null;
    }
}
