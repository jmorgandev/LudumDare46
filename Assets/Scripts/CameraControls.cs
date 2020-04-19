using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private const float friction = 0.1f;
    private const float dampening = 1.0f - friction;
    private const float scroll_edge_factor = 0.12f;
    private const float camera_speed = 20.0f;
    private const float camera_max_speed = 5.0f;


    private Vector3 velocity;
    private Vector3 acceleration;

    private int scroll_edge_thickness = 0;

    void Start()
    {
        scroll_edge_thickness = (int)((float)Screen.height * scroll_edge_factor);
        velocity = Vector3.zero;
        acceleration = Vector3.zero;
    }

    void DoScreenEdgeMovement()
    {
        float x_accel = 0f, y_accel = 0f;

        if (Input.mousePosition.x >= Screen.width - scroll_edge_thickness && Input.mousePosition.x <= Screen.width + scroll_edge_thickness)
            x_accel = camera_speed;
        else if (Input.mousePosition.x <= scroll_edge_thickness && Input.mousePosition.x >= -scroll_edge_thickness)
            x_accel = -camera_speed;

        if (Input.mousePosition.y >= Screen.height - scroll_edge_thickness && Input.mousePosition.y <= Screen.height + scroll_edge_thickness)
            y_accel = camera_speed;
        else if (Input.mousePosition.y <= scroll_edge_thickness && Input.mousePosition.y >= -scroll_edge_thickness)
            y_accel = -camera_speed;

        acceleration.Set(x_accel, y_accel, 0f);
    }

    // Return true if any directional buttons were pressed
    bool DoDirectionButtonMovement()
    {
        return false;
    }

    void Update()
    {
        acceleration.Set(0f, 0f, 0f);
            
        // Directional buttons override the edge scrolling
        if (!DoDirectionButtonMovement())
            DoScreenEdgeMovement();


    }

    void FixedUpdate()
    {
        velocity = acceleration.sqrMagnitude != 0.0f ? Vector3.ClampMagnitude(velocity + (acceleration * Time.fixedDeltaTime), camera_max_speed) :
                                                       velocity * dampening;

        transform.Translate(velocity * Time.fixedDeltaTime);

        Debug.Log(velocity);
    }
}
