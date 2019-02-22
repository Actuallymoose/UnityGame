using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHackingMenu : MonoBehaviour {

    public Canvas canvas;

    public string openPlayerHacking;

    bool menuOpen = false;

    public PlayerLook look;

    // Use this for initialization
    void Awake()
    {
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(openPlayerHacking) && !menuOpen)
        {
            canvas.enabled = true;
            menuOpen = true;
            look.enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetButtonDown(openPlayerHacking))
        {
            canvas.enabled = false;
            menuOpen = false;
            look.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
