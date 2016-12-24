using UnityEngine;
using System.Collections;

public class NavPointCreator : MonoBehaviour {

	public		int			ID;
				GameObject	WP;

	void Update()
	{		 
		// Apretando Space creo un nuevo punto de destino y borro el pasado punto de destino
		if (Input.GetKeyDown ("space"))
			CreaDestino ();
	}

	// ESTA FUNCION ES PARA CREAR UN NUEVO WAYPOINT
	void CreaDestino()
	{
		// Creo un cubo en un sitio aleatorio determinado por un rango y destruyo el anterior creado si lo hay . . .
		if (WP != null)	Destroy (WP);
		GameObject wayPoint = GameObject.CreatePrimitive(PrimitiveType.Cube);
		wayPoint.transform.position = new Vector3 (Random.Range(-7.0f,7.0f), 0.3f, Random.Range(-7.0f,7.0f));
		wayPoint.name = "WayPoint" + ID;
		wayPoint.transform.localScale = new Vector3 (0.5f,0.5f,0.5f);

		wayPoint.GetComponent<MeshFilter> ().mesh = null;
		//wayPoint.GetComponent<Renderer> ().material = null;
		//wayPoint.GetComponent<Renderer>().material.color = new Color(0.25f, 0.0f, 0.0f);

		// Convertimos WP en wayPoint
		WP = wayPoint;
	}
}
