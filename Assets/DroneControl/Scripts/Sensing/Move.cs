using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public GameObject player;

	//para que rote con el drone, pero que  no se incline
	void LateUpdate () //se haga todo para que se haga despues de que se calculen todas la fisicas
	{
		transform.position = player.transform.position;
		transform.rotation = Quaternion.Euler(0.0f, player.transform.rotation.eulerAngles.y, 0.0f);
		//transform.rotation = Quaternion.Euler(new Vector3(0.0f, player.transform.rotation.y, 0.0f));

	}

}
