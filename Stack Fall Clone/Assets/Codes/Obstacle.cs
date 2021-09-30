using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody rb;
    private MeshRenderer meshRenderer;
    private Collider obstacleCollider;
    private ObstacleController obstacleController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        obstacleCollider = GetComponent<Collider>();
        obstacleController = transform.parent.GetComponent<ObstacleController>();
    }



    void Update()
    {
        
    }

    public void Shatter()
    {
        rb.isKinematic = false;
        obstacleCollider.enabled = false;

        Vector3 forcepoint = transform.parent.position;
        float parentXpos = transform.parent.position.x;
        float xPos = meshRenderer.bounds.center.x;

        Vector3 subdirection = (parentXpos - xPos < 0) ? Vector3.right : Vector3.left;
        Vector3 dir = (Vector3.up * 1.5f + subdirection).normalized;

        float force = Random.Range(20, 35);
        float torque = Random.Range(110, 180);


        rb.AddForceAtPosition(dir * force, forcepoint, ForceMode.Impulse);

        rb.AddTorque(Vector3.left * torque);

        rb.velocity = Vector3.down;
    }

}
