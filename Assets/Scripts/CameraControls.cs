﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private const float friction = 0.1f;
    private const float dampening = 1.0f - friction;
    private const float scroll_edge_factor = 0.12f;
    private const float camera_speed = 20.0f;
    private const float camera_max_speed = 5.0f;

    private Vector2 bounds_min;
    private Vector2 bounds_max;

    private Vector3 velocity;
    private Vector3 acceleration;

    private int scroll_edge_thickness = 0;

    Renderer fader;

    private void Awake()
    {
        scroll_edge_thickness = (int)((float)Screen.height * scroll_edge_factor);
        velocity = Vector3.zero;
        acceleration = Vector3.zero;
    }

    void Start()
    {
        float vertical_extent = GetComponent<Camera>().orthographicSize;
        float horizontal_extent = vertical_extent * GetComponent<Camera>().aspect;
        BoxCollider2D collider = GameObject.Find("CameraBounds").GetComponent<BoxCollider2D>();
        Vector2 collider_origin = new Vector2(collider.transform.position.x, collider.transform.position.y) + collider.offset;

        fader = transform.GetChild(0).GetComponent<Renderer>();

        Color c = fader.material.color;
        c.a = 1.3f;
        fader.material.color = c;

        bounds_min = collider_origin - (collider.size * 0.5f);
        bounds_max = collider_origin + (collider.size * 0.5f);

        bounds_min.y += vertical_extent;
        bounds_max.y -= vertical_extent;

        bounds_min.x += horizontal_extent;
        bounds_max.x -= horizontal_extent;
    }

    void Update()
    {
        fader.material.color = new Color(fader.material.color.r, fader.material.color.g, fader.material.color.b,
                                         Mathf.Max(fader.material.color.a - (0.6f * Time.deltaTime), 0f));
        acceleration.Set(0f, 0f, 0f);

        // Directional buttons override the edge scrolling
        float x_accel = 0f, y_accel = 0f;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            x_accel = -camera_speed;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            x_accel = camera_speed;
        }
        else
        {
            if (Input.mousePosition.x >= Screen.width - scroll_edge_thickness && Input.mousePosition.x <= Screen.width)
                x_accel = camera_speed;
            else if (Input.mousePosition.x <= scroll_edge_thickness && Input.mousePosition.x >= 0)
                x_accel = -camera_speed;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            y_accel = camera_speed;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            y_accel = -camera_speed;
        }
        else
        {
            if (Input.mousePosition.y >= Screen.height - scroll_edge_thickness && Input.mousePosition.y <= Screen.height)
                y_accel = camera_speed;
            else if (Input.mousePosition.y <= scroll_edge_thickness && Input.mousePosition.y >= 0)
                y_accel = -camera_speed;
        }

        acceleration.Set(x_accel, y_accel, 0f);
    }

    Vector3 ClipToBounds(Vector3 v, Vector2 min, Vector2 max)
    {
        return new Vector3(Mathf.Clamp(v.x, min.x, max.x), Mathf.Clamp(v.y, min.y, max.y), v.z);
    }

    void FixedUpdate()
    {
        velocity = acceleration.sqrMagnitude != 0.0f ? Vector3.ClampMagnitude(velocity + (acceleration * Time.fixedDeltaTime), camera_max_speed) :
                                                       velocity * dampening;

        transform.Translate(velocity * Time.fixedDeltaTime);

        if (transform.position.x >= bounds_max.x || transform.position.x <= bounds_min.x)
            velocity.x = 0f;

        if (transform.position.y >= bounds_max.y || transform.position.y <= bounds_min.y)
            velocity.y = 0f;

        transform.position = ClipToBounds(transform.position, bounds_min, bounds_max);
    }
}
