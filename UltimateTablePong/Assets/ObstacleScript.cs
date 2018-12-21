using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {
    
    public float speed = 0.0f;
    public Color startColor = Color.red;
    public Color endColor = Color.yellow;
    private Renderer rend;
    private bool collision = false;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (collision)
        {
            rend.material.color = Color.Lerp(startColor, endColor, speed);
            speed += 0.02f;
            if(speed >= 1.0f)
            {
                collision = false;
                speed = 0.0f;
                rend.material.color = endColor;
            }
        }
	}

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit!");
        rend.material.color = startColor;
        speed = 0;
        collision = true;
    }

}
