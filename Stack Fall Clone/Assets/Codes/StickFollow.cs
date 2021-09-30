using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickFollow : MonoBehaviour
{
    public GameObject player;
    Vector3 playerPos;
    public bool touch;


    void Start()
    {
        touch = false;
    }



    void Update()
    {
        if(transform.position.y > player.transform.position.y && !touch)
        {
            playerPos = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
            transform.position = playerPos;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            touch = true;
        }
    }
}
