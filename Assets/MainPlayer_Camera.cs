using UnityEngine;
using System.Collections;

public class MainPlayer_Camera : MonoBehaviour {

	public float moveSpeed = 2.0f;
	
	void Start () {
	
	}

	void Update () {
		float angle = gameObject.transform.rotation.eulerAngles.x;
		if (Input.GetKey ("q")) {
			if(!(angle > 50 && angle < 300)) {
				gameObject.transform.Rotate(Vector3.left * moveSpeed);
			}
		}
		
		if (Input.GetKey ("e")) {
			if(!(angle > 40 && angle < 280)) {
				gameObject.transform.Rotate(Vector3.right * moveSpeed);
			}
		}
	}
}
