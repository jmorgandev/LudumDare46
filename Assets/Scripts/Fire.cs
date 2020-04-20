using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private Animator fire_animator;
    private Animator fuel_animator;

    public int Health
    { 
        get { return fire_health; }
        set { fire_health = value; }
    }
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
        fire_health++;
        fire_health = Mathf.Min(fire_health, 7);
        Debug.Log(fire_health);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
    }
}
