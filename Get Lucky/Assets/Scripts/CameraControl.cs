using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;
    public float cameraSpeed = 1f, backPosition=1.5f;

    void Start()
    {
        transform.position = new Vector3(player.transform.position.x - 2f, player.transform.position.y + 1.75f, player.transform.position.z);
        //transform.rotation = new Quaternion(25f, 90f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Slerp(transform.position, new Vector3(player.transform.position.x - 2f, transform.position.y, transform.position.z), cameraSpeed);
    }
}
