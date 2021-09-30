using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public GameObject cameraFollow;
    //private Vector3 cameraPos;
    private Transform player, win;
    //private float cameraOffSet=4f;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }
    void Start()
    {
        
    }



    void Update()
    {
        if (win == null)
        {
            win = GameObject.Find("win(Clone)").GetComponent<Transform>();
            
        }
        /*if(transform.position.y>player.position.y & transform.position.y > win.position.y+cameraOffSet)
        {
            cameraPos = new Vector3(transform.position.x, player.position.y, transform.position.z);
            transform.position = new Vector3(transform.position.x, cameraPos.y, -5);
        }*/
        if (transform.position.y > cameraFollow.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, cameraFollow.transform.position.y, transform.position.z);
        }
        transform.rotation = new Quaternion(15, 0, 0, 90);
    }
}
