using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class progBar : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fill;
    public Light bulb;
    public string message;
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void changeValue(Vector3 point) {
        Vector3 xvec = point - GetComponent<BoxCollider>().bounds.min;
        float progress = xvec.x / GetComponent<BoxCollider>().bounds.size.x;
        setProgress(progress);
    }

    void setProgress(float j) {
        float i = j;
        if (i < 0) i = 0;
        if (i > 1) i = 1;
        fill.transform.localScale = new Vector3(i,1,1);
        bulb.SendMessage(message, i, SendMessageOptions.DontRequireReceiver);
    }
}
