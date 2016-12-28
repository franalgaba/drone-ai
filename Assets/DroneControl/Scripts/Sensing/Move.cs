using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public GameObject player;

	void LateUpdate ()
	{
		transform.position = player.transform.position;
		transform.rotation = Quaternion.Euler(0.0f, player.transform.rotation.eulerAngles.y, 0.0f);
		//transform.rotation = Quaternion.Euler(new Vector3(0.0f, player.transform.rotation.y, 0.0f));
	}

}
