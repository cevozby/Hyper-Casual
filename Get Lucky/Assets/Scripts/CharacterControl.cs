using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float speed = 3f, lerpValue;
    private Rigidbody playerRB;
    private Animator playerAnimator;

    public Camera mainCamera;
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Moving();
    }

    public void Moving()
    {
        if (Input.GetMouseButton(0) && GameManager.finish == false)
        {
            playerRB.velocity = new Vector3(speed * Time.deltaTime, playerRB.velocity.y, playerRB.velocity.z);
            playerAnimator.SetFloat("Speed", playerRB.velocity.x);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, hit.point.z), lerpValue * Time.deltaTime);
            }
        }
        if (Input.GetMouseButtonUp(0) || GameManager.finish == true)
        {
            playerRB.velocity = new Vector3(0f, 0f, 0f);
            playerAnimator.SetFloat("Speed", playerRB.velocity.x);
        }
    }

    
}
