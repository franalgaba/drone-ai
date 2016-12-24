using UnityEngine;
using System.Collections;

public class RaycastPointToPoint : MonoBehaviour 
{
	public		GameObject		puntoB;

	void Update()
	{	
		Vector3 PuntoA = gameObject.transform.position;
		Vector3 PuntoB = puntoB.transform.position;
		Vector3 direccion = PuntoB - PuntoA;
		float distancia = Vector3.Distance (PuntoA, PuntoB);
		Ray ray = new Ray(PuntoA, PuntoB);

		Debug.DrawRay(ray.origin, direccion * 1.0f, Color.blue);
		Debug.Log ("Distancia: " + distancia);
	}

}
