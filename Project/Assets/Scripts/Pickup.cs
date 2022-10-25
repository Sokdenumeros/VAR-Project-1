using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Vector3 relative_pos;
    private CameraPointerManager cam;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (cam != null) {
            Vector3 position = Vector3.zero;

            position = cam.transform.right.normalized * relative_pos.x + cam.transform.up.normalized * relative_pos.y + cam.transform.forward.normalized * relative_pos.z;

            this.GetComponent<Rigidbody>().velocity = (position - transform.position)*10;
            //this.GetComponent<Rigidbody>().angularVelocity = (cam.transform.parent.transform.eulerAngles - (transform.eulerAngles*3/180)) * 10;
            //transform.Translate(position-transform.position);
        }
    }

    public void pick(CameraPointerManager c) {
        if (cam != null) { 
            drop(); 
            return;
        }
        cam = c;
        Vector3 dif = transform.position - (cam.transform.position-cam.transform.parent.transform.position);
        relative_pos.x = Vector3.Dot(dif, cam.transform.right.normalized);
        relative_pos.y = Vector3.Dot(dif, cam.transform.up.normalized);
        relative_pos.z = Vector3.Dot(dif, cam.transform.forward.normalized);
    }

    public void drop() {
        cam = null;
    }
}
