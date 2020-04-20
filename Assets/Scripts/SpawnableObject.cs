using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpawnableObject : MonoBehaviour
{
    public float respawn_time = 5f;

    private Vector3 spawn_position;
    private float spawn_angle;

    private void Start()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        spawn_position = transform.position;
        spawn_angle = transform.rotation.eulerAngles.z;
    }

    private void Update()
    {
        
    }

    public void OnGrab()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    IEnumerator Respawn(float respawn_time)
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        transform.position = spawn_position;
        transform.rotation.eulerAngles.Set(0, 0, spawn_angle);
        yield return new WaitForSeconds(respawn_time);
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    public void OnConsume()
    {
        StartCoroutine(Respawn(respawn_time));
    }

    public void OnReject()
    {

    }

    public void OnBurn()
    {

    }
}


