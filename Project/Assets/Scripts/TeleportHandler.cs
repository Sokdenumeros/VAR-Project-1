using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportHandler : MonoBehaviour
{
    public static bool destinationValid = true;
    private readonly string interactableTag = "Interactable";
    private readonly string environmentTag = "Environment";
    private Color teleporterColor;
    private Color glowColor;
    private Color redGlowColor;

    void Start()
    {
        teleporterColor = this.GetComponent<Renderer>().material.color;
        glowColor = transform.GetChild(0).GetComponent<Renderer>().material.GetColor("_BottomColor");
        redGlowColor = Color.red;
        redGlowColor.a = glowColor.a;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == interactableTag || other.gameObject.tag == environmentTag)
        {
            destinationValid = false;
            this.GetComponent<Renderer>().material.color = Color.red;
          
            transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_BottomColor", redGlowColor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == interactableTag || other.gameObject.tag == environmentTag)
        {
            destinationValid = true;
            this.GetComponent<Renderer>().material.color = teleporterColor;
            transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_BottomColor", glowColor);
        }
    }
}