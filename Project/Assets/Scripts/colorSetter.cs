using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorSetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void red(float i) {
        if (i < 0) i = 0;
        if (i > 1) i = 1;
        Light l = GetComponent<Light>();
        l.color = new Color(i , l.color.g, l.color.b, l.color.a);
    }

    void green(float i)
    {
        if (i < 0) i = 0;
        if (i > 1) i = 1;
        Light l = GetComponent<Light>();
        l.color = new Color(l.color.r, i , l.color.b, l.color.a);
    }

    void blue(float i)
    {
        if (i < 0) i = 0;
        if (i > 1) i = 1;
        Light l = GetComponent<Light>();
        l.color = new Color(l.color.r, l.color.g, i , l.color.a);
    }
}
