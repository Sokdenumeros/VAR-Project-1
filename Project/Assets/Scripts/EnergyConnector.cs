using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyConnector : MonoBehaviour
{
    List<GameObject> currentCollisions = new List<GameObject>();
    bool power;
    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        power = false;
        mat = GetComponent<Renderer>().material;
    }

    void Update() {
        power = false;
        mat.DisableKeyword("_EMISSION");
        GetComponent<Renderer>().material = mat;
        GetComponent<Renderer>().material.color = Pickup.currentColor;
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
        Material mat = GetComponent<Renderer>().material;
        float intensity = 1.1f;
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", Color.yellow * intensity);
        GetComponent<Renderer>().material = mat;
        propagatePower();
    }

}
