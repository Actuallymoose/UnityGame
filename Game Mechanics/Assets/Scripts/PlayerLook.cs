using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    public string mouseXInputName, mouseYInputName;

    [Range(0,10)]
    public float mouseSensitivity;
    public Transform player;

    float clampY;

	// Use this for initialization
	void Start ()
    {
        LockCursor();
        clampY = 0.0f;
	}

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void Update ()
    {
        CameraRotation();
	}

    void CameraRotation()
    {
        float mouseX = Input.GetAxisRaw(mouseXInputName) * mouseSensitivity;
        float mouseY = Input.GetAxisRaw(mouseYInputName) * mouseSensitivity;

        clampY += mouseY;

        if(clampY > 60.0f)
        {
            clampY = 60.0f;
            mouseY = 0.0f;
        }
        else if(clampY < -60.0f)
        {
            clampY = -60.0f;
            mouseY = 0.0f;
        }

        transform.Rotate(Vector3.left * mouseY);
        player.Rotate(Vector3.up * mouseX);
    }  
}
