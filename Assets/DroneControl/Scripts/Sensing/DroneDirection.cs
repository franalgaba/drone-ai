using UnityEngine;
using System.Collections;

public class DroneDirection : MonoBehaviour {

	public Transform target;
	public Transform drone;
	public float speed;

	void Update() {
		//float step = speed * Time.deltaTime;
		//transform.position = Vector3.MoveTowards(transform.position, target.position, step);

		transform.position = drone.position;
		transform.LookAt(target);
	}
}
