using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windTurbineBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject blades;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void GivePower()
    {
        blades.transform.eulerAngles += new Vector3(0,0,20)*Time.deltaTime;
    }
}
