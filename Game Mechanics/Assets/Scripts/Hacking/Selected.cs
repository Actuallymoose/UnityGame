using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selected : MonoBehaviour {

    public bool selected = false;

    Canvas canvas;

	// Use this for initialization
	void Awake () {
        canvas = GetComponentInChildren<Canvas>();
        selected = false;
        canvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(selected)
        {
            canvas.enabled = true;
        }
        else
        {
            canvas.enabled = false;
        }
	}
}
