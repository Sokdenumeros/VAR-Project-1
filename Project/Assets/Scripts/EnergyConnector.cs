using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyConnector : MonoBehaviour
{
    List<GameObject> currentCollisions = new List<GameObject>();
    bool power;
    // Start is called before the first frame update
    void Start()
    {
        power = false;
    }

    void Update() {
        power = false;
        GetComponent<Renderer>().material.color = Color.blue;
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

    private void GivePower() {
        if (power) return;
        power = true;
        GetComponent<Renderer>().material.color = Color.red;
        propagatePower();
    }

}
