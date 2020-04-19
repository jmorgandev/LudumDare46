using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite3DRenderer : MonoBehaviour
{
    public List<Sprite> frames;

    private int angle_step = 0;
    private SpriteRenderer sprite_renderer;

    private Transform physics_object;
    void Start()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        angle_step = 180 / frames.Count;

        physics_object = transform.parent;
        transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        // Access frames based on Y-angle
        sprite_renderer.flipX = Mathf.Abs(physics_object.rotation.eulerAngles.y) % 180 < 90;
        sprite_renderer.sprite = frames[Mathf.Abs((int)(physics_object.rotation.eulerAngles.y / angle_step)) % frames.Count];

        Debug.Log(sprite_renderer.flipX);

        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        transform.position = physics_object.position;
    }
}
