using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverBehaviour : MonoBehaviour
{

    private Material initialMat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnterXR()
    {
        initialMat = new(GetComponent<Renderer>().material);
        GetComponent<Renderer>().material.color = initialMat.color*2;
    }

    public void OnPointerExitXR()
    {
         GetComponent<Renderer>().material = initialMat;
    }
}
