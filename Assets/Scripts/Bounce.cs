using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {

	float lerpTime;
	float currentLerpTime;
	float perc = 1;

	Vector3 startPos;
	Vector3 endPos;

	// Update is called once per frame
	void Update () {
	
		if (Input.GetButtonDown ("up") || Input.GetButtonDown ("down") || Input.GetButtonDown ("left") || Input.GetButtonDown ("right")) {
			if(perc == 1){ 
				lerpTime = 1;
				currentLerpTime = 0;
			}
		}
		startPos = gameObject.transform.position;
		if (Input.GetButtonDown ("right")) {
			endPos = new Vector3(transform.position.x - 10, transform.position.y,transform.position.z);
		}
		if (Input.GetButtonDown ("left")) {
//			endPos = new Vector3(transform.position.x + 10, transform.position.y,transform.position.z);
			Quaternion target = Quaternion.Euler(transform.rotation.x + 90, 0, 0);
			transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 2);
		}
		if (Input.GetButtonDown ("up")) {
			endPos = new Vector3(transform.position.x, transform.position.y,transform.position.z - 10);
		}
		if (Input.GetButtonDown ("down")) {
			endPos = new Vector3(transform.position.x, transform.position.y,transform.position.z + 10);
		}
		currentLerpTime += Time.deltaTime * 5.5F;
		perc = currentLerpTime / lerpTime;
		gameObject.transform.position = Vector3.Lerp (startPos, endPos, perc * 10);
	}
}
