using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {
    
    public float speed = 0.0f;
    public Color startColor = Color.red;
    public Color endColor = Color.yellow;
    private Renderer rend;
    private bool collision = false;
    private bool collision2 = false;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (collision)
        {
            rend.material.color = Color.Lerp(startColor, endColor, speed);
            speed += 0.005f;
            if(speed >= 1.0f)
            {
                collision = false;
                speed = 0.0f;
                rend.material.color = endColor;
            }
            Debug.Log(transform.position.y);
            
        }
        if (collision2)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z);
            if (transform.position.y >= 100)
            {
                collision2 = false;
                
            }
        }
	}

    public void OnTriggerEnter(Collider other)
    {
        rend.material.color = startColor;
        speed = 0;
        transform.position = new Vector3(transform.position.x, -50.0f, transform.position.z);
        collision = true;
        collision2 = true;
    }

}
