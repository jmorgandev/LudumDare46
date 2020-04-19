using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCursor : MonoBehaviour
{
    public Sprite default_hand;
    public Sprite hover_hand;
    public Sprite closed_hand;

    private SpriteRenderer sprite_renderer;

    int layer_mask;

    void Awake()
    {
        Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        sprite_renderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        layer_mask = 1 << LayerMask.NameToLayer("dynamics");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;

        Collider2D col = Physics2D.OverlapCircle(pos, 0.3f, layer_mask);
        if (col)
        {
            sprite_renderer.sprite = hover_hand;
        }
        else
        {
            sprite_renderer.sprite = default_hand;
        }
    }
}
