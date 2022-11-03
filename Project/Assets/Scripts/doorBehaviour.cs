using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorBehaviour : MonoBehaviour
{
    private Vector3 startingPosition;
    public float offsetx;
    public float offsety;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = (startingPosition - transform.position)/2;
        if (move.magnitude < Time.deltaTime)
        {
            transform.position = startingPosition;
        }
        else
        {
            transform.position += move.normalized * Time.deltaTime;
        }
    }

    private void GivePower()
    {
        Vector3 target = startingPosition + new Vector3(offsetx, offsety, 0);
        Vector3 move = target - transform.position;
        if (move.magnitude < Time.deltaTime)
        {
            transform.position = target;
        }
        else {
            transform.position += move.normalized * Time.deltaTime;
        }
    }
}
