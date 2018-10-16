using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seagull : MonoBehaviour {
    private Rigidbody2D rigidBody;
    private bool LR = false;
    public Fungus.Flowchart myFlowchart;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        //myFlowchart = GameObject.Find("GullSpawn").Flowchart;

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
                rigidBody.AddForce(new Vector2(0, 40f));
                rigidBody.velocity = new Vector2(1f, 4f);
            }
            else
            {
                rigidBody.AddForce(new Vector2(0, 40f));
                rigidBody.velocity = new Vector2(-1f, 4f);
            }
        }

        if (gameObject.transform.position.x <= .8 && gameObject.transform.position.x >= -.8)
        {
            //health loss
            myFlowchart.SetIntegerVariable("Health", (myFlowchart.GetIntegerVariable("Health") - 1));

            Launch();
        }
    }

    void Launch()
    { 
        if (LR)
        {
            rigidBody.AddForce(new Vector2(0, 50f));
            rigidBody.velocity = new Vector2(-5f, 10f);
        }
        else
        {
            rigidBody.AddForce(new Vector2(0, 50f));
            rigidBody.velocity = new Vector2(5f, 10f);
        }
    }

    private void OnMouseDown()
    {
        Launch();

        if (LR)
        {
            GameObject gull = GameObject.Instantiate(gameObject);
			gull.transform.position = new Vector3(-10f, 5f, 0f);
        }
        else
        {
            GameObject gull = GameObject.Instantiate(gameObject);
			gull.transform.position = new Vector3(10f, 5f, 0f);
        }
    }
}
