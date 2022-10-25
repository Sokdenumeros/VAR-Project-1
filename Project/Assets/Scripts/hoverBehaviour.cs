using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoverBehaviour : MonoBehaviour
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

    public void OnPointerEnter()
    {
        initialMat = new(GetComponent<Renderer>().material);
        GetComponent<Renderer>().material.color = initialMat.color*2;
    }

    public void OnPointerExit()
    {
         GetComponent<Renderer>().material = initialMat;
    }
}
