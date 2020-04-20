using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpawnableObject : MonoBehaviour
{
    public float respawn_time = 5f;
    public float alpha = 1f;

    private Vector3 spawn_position;
    private float spawn_angle;

    Material mat;
    Color col;

    bool fade_in = false;

    private void Start()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        spawn_position = transform.position;
        spawn_angle = transform.rotation.eulerAngles.z;
        mat = GetComponent<Renderer>().material;
        col = mat.color;
        col.a = 1f;
    }

    private void Update()
    {
        mat.color = col;
        if (fade_in)
        {
            col.a = Mathf.Min(col.a + (1f * Time.deltaTime), 1f);
            fade_in = col.a != 1f;
        }
    }

    public void OnGrab()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        col.a = 1f;
        fade_in = false;
    }

    IEnumerator Respawn(float respawn_time)
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
        col.a = 0f;
        transform.position = spawn_position;
        transform.rotation.eulerAngles.Set(0, 0, spawn_angle);
        yield return new WaitForSeconds(respawn_time);
        GetComponent<Collider2D>().enabled = true;
        fade_in = true;
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


