using UnityEngine;
using System.Collections;

public class POV : MonoBehaviour {

	public float interpVelocity;
	public float minDistance;
	public float followDistance;
	public GameObject target;
	public Vector3 offset;
	Vector3 targetPos;
	// Use this for initialization
	void Start () {
		targetPos = transform.position;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (target)
		{

			transform.position = Vector3.Lerp( transform.position, targetPos + offset, 0.25f);

		}
	}
}