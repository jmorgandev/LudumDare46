using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCursor : MonoBehaviour
{
    public Sprite default_hand;
    public Sprite hover_hand;
    public Sprite closed_hand;

    SpriteRenderer sprite_renderer;

    void Awake()
    {
        Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;

        
        if (Input.GetMouseButton(0))
            sprite_renderer.sprite = hover_hand;
        else
            sprite_renderer.sprite = default_hand;
    }
}
