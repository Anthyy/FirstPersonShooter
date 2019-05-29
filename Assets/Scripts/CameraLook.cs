using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
	public bool isCursorHidden = true;
	public float minPitch = -20f, maxPitch = 80f;
	public Vector2 speed = new Vector2(120f, 120f);
	
	private Vector2 euler; // Current rotation of camera
	
    // Start is called before the first frame update
    void Start()
    {
        // Is the cursor supposed to be hidden?
		if(isCursorHidden) 
		{
			// Locks the cursor in place and hides it
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		// Get current euler rotation of camera
		euler = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the camera based on Mouse movement
		euler.y += Input.GetAxis("Mouse X") * speed.x * Time.deltaTime;
		euler.x -= Input.GetAxis("Mouse Y") * speed.y * Time.deltaTime;
		
		// Clamp the value
		euler.x = Mathf.Clamp(euler.x, minPitch, maxPitch);
		
		// Rotate the Player & Camera Seperately
		transform.parent.localEulerAngles = new Vector3(0, euler.y, 0);
		transform.localEulerAngles = new Vector3(euler.x, 0, 0);
    }
}
