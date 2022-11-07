using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyConnector : MonoBehaviour
{
    List<GameObject> currentCollisions = new List<GameObject>();
    bool power;
    public Material emission;
    public Material initial;
    // Start is called before the first frame update
    void Start()
    {
        power = false;
        initial = GetComponent<Renderer>().material;
    }

    void Update() {
        power = false;
        GetComponent<Renderer>().material = initial;
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
        GetComponent<Renderer>().material = emission;
        propagatePower();
    }

}
