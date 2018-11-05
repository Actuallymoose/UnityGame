using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    public string mouseXInputName, mouseYInputName;

    [Range(0,10)]
    public float mouseSensitivityY, mouseSensitivityX;
    public Transform player;

    float clampY, maxView;

	// Use this for initialization
	void Start ()
    {
        LockCursor();
        clampY = 0.0f;
        maxView = 60f;
	}

    // hides cursor and prevents it from exiting the screen
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
        float mouseX = Input.GetAxisRaw(mouseXInputName) * mouseSensitivityX;
        float mouseY = Input.GetAxisRaw(mouseYInputName) * mouseSensitivityY;

        clampY += mouseY;
        mouseY = Clamp(mouseY, -maxView, maxView);

        transform.Rotate(Vector3.left * mouseY);
        player.Rotate(Vector3.up * mouseX);
    }  

    float Clamp(float angle, float min, float max)
    {
        if(clampY > max)
        {
            clampY = max;
            angle = 0.0f;
        }
        else if (clampY < min)
        {
            clampY = min;
            angle = 0.0f;
        }

        return angle;
    }
}
