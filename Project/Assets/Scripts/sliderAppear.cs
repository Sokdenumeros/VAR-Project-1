using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliderAppear : MonoBehaviour
{
    bool actv;
    public GameObject sliderCanvas;
    // Start is called before the first frame update
    void Start()
    {
        actv = false;
        sliderCanvas.SetActive(actv);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void interaction(CameraPointerManager c)
    {
        if (actv) actv = false;
        else actv = true;
        sliderCanvas.SetActive(actv);
    }
}
