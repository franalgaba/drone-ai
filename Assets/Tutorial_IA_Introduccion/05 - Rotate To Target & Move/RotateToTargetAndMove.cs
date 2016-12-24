using UnityEngine;
using System.Collections;

public class RotateToTargetAndMove : MonoBehaviour {

	public		GameObject		puntoB;
	public		float			velocidadCharacter	= 8.0f;

	void Update()
	{
		Vector3 PuntoA = gameObject.transform.position;
		Vector3 PuntoB = puntoB.transform.position;
		Vector3 direccion = PuntoB - PuntoA;
		float distancia = Vector3.Distance (PuntoA, PuntoB);
		Ray ray = new Ray(PuntoA, PuntoB);

		if (Input.GetMouseButton (0)) 
		{
			// Encaramos al target(puntoB)
			transform.LookAt (PuntoB);
			// Avanzamos hacia delante
			gameObject.transform.position += gameObject.transform.forward * velocidadCharacter * Time.deltaTime;
		}

		Debug.DrawRay(ray.origin, direccion * 1.0f, Color.blue);
		Debug.Log ("Distancia: " + distancia);
	}
}
