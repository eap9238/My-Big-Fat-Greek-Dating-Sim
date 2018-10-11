using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seagull : MonoBehaviour {
    private Rigidbody2D rigidbody;
    private bool LR = false;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        if (gameObject.transform.position.x < 0)
        {
            LR = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y < 0)
        {
            if (LR)
            {
                rigidbody.AddForce(new Vector2(0, 40f));
                rigidbody.velocity = new Vector2(1f, 4f);
            }
            else
            {
                rigidbody.AddForce(new Vector2(0, 40f));
                rigidbody.velocity = new Vector2(-1f, 4f);
            }
        }

        if (gameObject.transform.position.x <= .8 && gameObject.transform.position.x >= -.8)
        {
            Launch();
        }
    }

    void Launch()
    { 
        if (LR)
        {
            rigidbody.AddForce(new Vector2(0, 50f));
            rigidbody.velocity = new Vector2(-5f, 10f);
        }
        else
        {
            rigidbody.AddForce(new Vector2(0, 50f));
            rigidbody.velocity = new Vector2(5f, 10f);
        }
    }

    private void OnMouseDown()
    {
        Launch();

        if (LR)
        {
            GameObject gull = GameObject.Instantiate(gameObject);
            gull.transform.position.Set(-10f, 5f, 0f);
        }
        else
        {
            GameObject gull = GameObject.Instantiate(gameObject);
            gull.transform.position.Set(10f, 5f, 0f);
        }
    }
}
