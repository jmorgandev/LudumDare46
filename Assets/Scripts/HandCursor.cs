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

    private bool grabbing = false;
    private TargetJoint2D grab_joint;

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

        if (grabbing)
        {
            sprite_renderer.sprite = closed_hand;
            grab_joint.target = transform.position;
            if (Input.GetMouseButtonUp(0))
            {
                grabbing = false;
                Destroy(grab_joint);
            }
        }
        else
        {
            Collider2D col = Physics2D.OverlapCircle(pos, 0.3f, layer_mask);
            if (col)
            {
                if (Input.GetMouseButtonDown(0) && !grabbing)
                {
                    grabbing = true;
                    grab_joint = col.gameObject.AddComponent<TargetJoint2D>();
                    grab_joint.frequency = 20;
                }
                else
                    sprite_renderer.sprite = hover_hand;
            }
            else
            {
                sprite_renderer.sprite = default_hand;
            }
        }
    }
}
