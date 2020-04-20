using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpawnableObject : MonoBehaviour
{
    private Vector3 spawn_position;
    private float spawn_angle;

    private void Start()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        spawn_position = transform.position;
        spawn_angle = transform.rotation.z;
    }

    private void Update()
    {
        
    }

    public void OnGrab()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}


