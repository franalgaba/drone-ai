using UnityEngine;
using System.Collections;

public class RaycastArrayObstacles : MonoBehaviour {

	public		GameObject		puntoB;
	 			GameObject		obstaculo;

	void Update()
	{
		Vector3 PuntoA = gameObject.transform.position;
		Vector3 PuntoB = puntoB.transform.position;
		Vector3 direccion = PuntoB - PuntoA;
		float distancia = Vector3.Distance (PuntoA, PuntoB);

		Ray ray = new Ray(gameObject.transform.position, puntoB.transform.position);
		Debug.DrawRay(ray.origin, direccion * 1.0f, Color.blue);

		// Declaramos una array que llenaremos con los contactos con los que choque ray
		RaycastHit[] hit;
		hit = Physics.RaycastAll (ray);

		// Nos interesa ver el primer obstaculo que nos barra el camino y averiguar cual es y a que distancia esta
		if (hit.Length > 0) 
		{
			foreach(RaycastHit h in hit)
			{
				obstaculo = h.transform.gameObject;
				Vector3 Obstaculo = obstaculo.transform.position;
				Vector3 direObstaculo = Obstaculo - PuntoA;
				float distObstaculo = Vector3.Distance (PuntoA, Obstaculo);
				Debug.Log("Primer Obstaculo " + h.transform.gameObject.name + " a distancia: " + distObstaculo);
				Debug.DrawRay(ray.origin, direObstaculo * 1.0f, Color.red);
				break; // Rompiendo aqui cogemos solo el primer obstaculo que se encuentra
			}
		}
	}
}
