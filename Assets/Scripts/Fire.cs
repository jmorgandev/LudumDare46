using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private Animator fire_animator;
    private Animator fuel_animator;

    private int fire_health;

    // Start is called before the first frame update
    void Start()
    {
        fire_animator = transform.Find("FireSprite").GetComponent<Animator>();
        fuel_animator = transform.Find("FuelSprite").GetComponent<Animator>();
        fuel_animator.StopPlayback();

        Debug.Log(fire_health);
        InvokeRepeating("doHealth", 0f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void doHealth()
    {
        fire_health = Mathf.Min(fire_health + 1, 10);
        Debug.Log(fire_health);

        if (fire_health >= 0 && fire_health < 4)
        {
            fire_animator.SetInteger("stage", 0);
        }
        else if (fire_health >= 4 && fire_health < 8)
        {
            fire_animator.SetInteger("stage", 1);
        }
        else if (fire_health >= 8)
        {
            fire_animator.SetInteger("stage", 2);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
    }
}
