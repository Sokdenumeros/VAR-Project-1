using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulbBehaviour : MonoBehaviour
{
    public Light llum;
    private float startingIntensity;
    // Start is called before the first frame update
    void Start()
    {
        startingIntensity = llum.intensity;
        llum.intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        llum.intensity = 0;
    }

    private void GivePower()
    {
        llum.intensity = 1.5f;
    }
}
