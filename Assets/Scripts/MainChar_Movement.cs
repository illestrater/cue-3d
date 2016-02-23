using UnityEngine;
using System.Collections;

public class MainChar_Movement : MonoBehaviour {

	public float moveSpeed = 2.0f;
	public float rotateSpeed = 2.0f;

	void Start () {
	
	}

	void Update () {

		if (Input.GetKey ("w")) {
			gameObject.transform.Translate(Vector3.forward * moveSpeed);
		}

		if (Input.GetKey ("s")) {
			gameObject.transform.Translate(Vector3.back * moveSpeed);
		}

		if (Input.GetKey ("a")) {
			gameObject.transform.Rotate(Vector3.down * rotateSpeed);
		}

		if (Input.GetKey ("d")) {
			gameObject.transform.Rotate(Vector3.up * rotateSpeed);
		}

		if (Input.GetKeyDown ("space")){
			gameObject.transform.Translate(Vector3.up * 260 * Time.deltaTime, Space.World);
		} 
	
	}
}
