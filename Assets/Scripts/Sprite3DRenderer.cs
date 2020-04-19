using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite3DRenderer : MonoBehaviour
{
    public List<Sprite> frames;

    private int angle_step = 0;
    private SpriteRenderer sprite_renderer;
    void Start()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        angle_step = 180 / frames.Count;
    }

    // Update is called once per frame
    void Update()
    {
        // Access frames based on Y-angle
        sprite_renderer.flipX = transform.rotation.y > (angle_step * frames.Count);
        sprite_renderer.sprite = frames[(int)transform.rotation.y % angle_step];
    }
}
