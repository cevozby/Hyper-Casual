using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{

    public float rotationSpeed = 5f , speed = 5f, space = 0.15f, height = 0.35f;
    Vector3 pos;
    void Start()
    {
        
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * speed);

        transform.position = new Vector3(transform.position.x, newY * space + height, transform.position.z);

        transform.eulerAngles = new Vector3(transform.rotation.x, rotationSpeed, transform.rotation.z);
        rotationSpeed++;
        if (rotationSpeed == 360f)
        {
            rotationSpeed = 0f;
        }
    }
}
