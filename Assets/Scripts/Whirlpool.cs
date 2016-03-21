﻿using UnityEngine;
using System.Collections;

public class Whirlpool : MonoBehaviour {
    public Collider2D[] inRange;
    public float pullRadius = 2;
    public float pullForce = 1;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        inRange = Physics2D.OverlapCircleAll(transform.position, pullRadius);
        foreach (Collider2D collider in inRange) {
            // calculate direction from target to me
            Vector2 forceDirection = transform.position - collider.transform.position;
            
            // apply force on target towards me
            float inverse = 1f / forceDirection.magnitude;
            if (collider.gameObject.tag != "Whirlpool")
            {
                collider.GetComponent<Rigidbody2D>().AddForce(10f * inverse * forceDirection * pullForce * Time.fixedDeltaTime);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "FishPlayerBullet" || coll.gameObject.tag == "BearPlayerBullet" || coll.gameObject.tag == "FishAllyBullet" || coll.gameObject.tag == "BearAllyBullet")
        {
            Destroy(coll.gameObject);
        }

    }
    
}