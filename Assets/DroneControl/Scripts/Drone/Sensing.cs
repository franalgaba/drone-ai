using UnityEngine;
using System.Collections;

public class Sensing : MonoBehaviour {

	public float distance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		RaycastHit delante;
		RaycastHit arriba;
		RaycastHit izquierda;
		RaycastHit derecha;
		RaycastHit atras;

		//Enviar velocidad a controlador AI
		//Enviar aceleracion a controlador AI?

		//Falta calcular distancia hasta la meta

		Debug.DrawRay (transform.position, transform.forward * distance);

		if (Physics.Raycast(transform.position, transform.forward, out delante, distance))
			print("objeto delante a distancia: " + delante.distance);
			//enviar distancia al controlador AI

		Debug.DrawRay (transform.position, transform.up * distance);

		if (Physics.Raycast(transform.position, transform.up, out arriba, distance))
			print("objeto arriba a distancia: " + arriba.distance);
			//enviar distancia al controlador AI
		
		Debug.DrawRay (transform.position, -transform.right * distance);

		if (Physics.Raycast(transform.position, -transform.right, out izquierda, distance))
			print("obejto a la izquierda a distancia: " + izquierda.distance);
			//enviar distancia al controlador AI
		
		Debug.DrawRay (transform.position, transform.right * distance);

		if (Physics.Raycast(transform.position, transform.right, out derecha, distance))
			print("objeto a la derecha a distancia: " + derecha.distance);
			//enviar distancia al controlador AI

		Debug.DrawRay (transform.position, -transform.forward * distance);

		if (Physics.Raycast(transform.position, -transform.forward, out atras, distance))
			print("objeto atras a distancia: " + atras.distance);
			//enviar distancia al controlador AI
	}
}
