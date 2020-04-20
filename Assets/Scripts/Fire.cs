using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private Animator fire_animator;
    private SpriteRenderer fuel_renderer;

    public Sprite[] fuel_frames;

    private CircleCollider2D item_area;

    private int fire_health;

    // Start is called before the first frame update
    void Start()
    {
        fire_animator = transform.Find("FireSprite").GetComponent<Animator>();
        fuel_renderer = transform.Find("FuelSprite").GetComponent<SpriteRenderer>();

        item_area = GetComponent<CircleCollider2D>();

        Debug.Log(fire_health);
        UpdateHealthEffects();
        InvokeRepeating("doHealth", 2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateHealthEffects()
    {
        fuel_renderer.sprite = fuel_frames[Mathf.Min(fire_health, fuel_frames.Length - 1)];

        if (fire_health >= 0 && fire_health < 4)
        {
            fire_animator.SetInteger("stage", 0);
            item_area.radius = 0.6f;
        }
        else if (fire_health >= 4 && fire_health < 8)
        {
            fire_animator.SetInteger("stage", 1);
            item_area.radius = 0.95f;
        }
        else if (fire_health >= 8)
        {
            fire_animator.SetInteger("stage", 2);
            item_area.radius = 1.25f;
        }
    }

    private void doHealth()
    {
        fire_health = Mathf.Min(fire_health + 1, 10);
        Debug.Log(fire_health);

        UpdateHealthEffects();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
    }
}
