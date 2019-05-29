using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtDirection : MonoBehaviour
{
	public Transform target; // Reference to a target
	
    // Update is called once per frame
    void LateUpdate()
    {
        // Rotate to target direction
		transform.rotation = Quaternion.LookRotation(target.forward);
    }
}
