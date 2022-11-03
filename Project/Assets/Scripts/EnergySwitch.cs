using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySwitch : MonoBehaviour
{
    List<GameObject> currentCollisions = new List<GameObject>();
    bool power;
    // Start is called before the first frame update
    void Start()
    {
        power = false;
        GetComponent<Renderer>().material.color = Color.blue;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (power)
        {
            propagatePower();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        currentCollisions.Add(col.gameObject);
    }

    void OnTriggerExit(Collider col)
    {
        currentCollisions.Remove(col.gameObject);
    }

    private void propagatePower()
    {
        foreach (GameObject gObject in currentCollisions)
        {
            gObject.SendMessage("GivePower", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void interaction(CameraPointerManager c)
    {
        if (power)
        {
            power = false;
            transform.Rotate(-7, 0, 0);
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            power = true;
            transform.Rotate(7, 0, 0);
            GetComponent<Renderer>().material.color = Color.red;
            propagatePower();
        }
    }
}
