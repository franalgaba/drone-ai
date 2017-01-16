using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public GameObject player;

	//para que el eje rote con el drone en el eje Y pero no en el X y Z.
	void LateUpdate () 
	{
		transform.position = player.transform.position;
		transform.rotation = Quaternion.Euler(0.0f, player.transform.rotation.eulerAngles.y, 0.0f);
	}

}
