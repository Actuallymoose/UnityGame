using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHackingMenu : MonoBehaviour {

    public Canvas canvas;

    int clickableMask;

    public string openPlayerHacking;
    public string selectTarget;

    bool menuOpen = false;

    public PlayerLook look;
    public PlayerAttack ableToAttack;

    Camera cam;

    // Use this for initialization
    void Awake()
    {
        canvas.enabled = false;
        cam = Camera.main;
        clickableMask = LayerMask.GetMask("Clickable");
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
            ableToAttack.enabled = false;
        }
        else if (Input.GetButtonDown(openPlayerHacking))
        {
            canvas.enabled = false;
            menuOpen = false;
            look.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            ableToAttack.enabled = true;
        }

        if(menuOpen)
        {
            ClickTarget();
        }

    }

    void ClickTarget()
    {
        if(Input.GetButtonDown(selectTarget))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, clickableMask)) /* hit.collider != null*/
            {
                if(hit.collider != null)
                {
                    Debug.Log("You selected an enemy");
                }
            }
        }

    }
}
