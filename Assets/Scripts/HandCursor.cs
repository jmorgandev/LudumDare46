using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCursor : MonoBehaviour
{
    public Sprite default_hand;
    public Sprite hover_hand;
    public Sprite closed_hand;

    private SpriteRenderer sprite_renderer;

    LayerMask layer_mask;
    LayerMask grab_layer_mask;

    private bool grabbing = false;
    private TargetJoint2D grab_joint;
    private GameObject grabbed_object;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        sprite_renderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        layer_mask = 1 << LayerMask.NameToLayer("dynamics");
        grab_layer_mask = 1 << LayerMask.NameToLayer("dynamics") | 1 << LayerMask.NameToLayer("Default");
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);

        if (grabbing)
        {
            if (Input.GetMouseButtonUp(0) || Physics2D.LinecastAll(pos, grabbed_object.transform.position, grab_layer_mask).Length > 1)
            {
                grabbing = false;
                grabbed_object.GetComponent<Rigidbody2D>().AddTorque(Random.Range(0, 2) == 0 ? 10.0f : -10.0f);
                Destroy(grab_joint);
                grabbed_object = null;
            }
            else
            {
                sprite_renderer.sprite = closed_hand;
                grab_joint.target = transform.position;
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
                    grabbed_object = col.gameObject;
                    grabbed_object.SendMessage("OnGrab");
                    grab_joint = grabbed_object.AddComponent<TargetJoint2D>();
                    grab_joint.frequency = 10;
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
