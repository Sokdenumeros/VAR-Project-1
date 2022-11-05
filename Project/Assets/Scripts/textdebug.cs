using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textdebug : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMesh>().text = "aaa";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
