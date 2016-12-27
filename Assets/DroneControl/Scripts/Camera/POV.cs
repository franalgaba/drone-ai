using UnityEngine;
using System.Collections;

public class POV : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;
	private Vector3 pos;
	private Quaternion rotation;


	void Start ()
	{
		offset = transform.position - player.transform.position;
	}

	void LateUpdate ()
	{

		pos = player.transform.position;

		pos.x = player.transform.position.x + offset.x;
		pos.y = player.transform.position.y + offset.y;

		rotation = Quaternion.Euler(0.0f, player.transform.rotation.eulerAngles.y, 0.0f);

		this.transform.position = pos;
		this.transform.rotation = rotation;


	}


}