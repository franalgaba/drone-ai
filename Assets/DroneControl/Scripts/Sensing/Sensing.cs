using UnityEngine;
using System.Collections;

public class Sensing : MonoBehaviour {

	public float range;
	public Transform objective;

	private float distance = 0.0f;
	private float speed = 0.0f;

	void FixedUpdate () {
		RaycastHit delante;
		RaycastHit arriba;
		RaycastHit izquierda;
		RaycastHit derecha;
		RaycastHit atras;
		RaycastHit debajo;
		Rigidbody drone;

		drone = this.GetComponentInParent<Rigidbody> ();
		speed = drone.velocity.magnitude;
		print("velocidad en m/s: " + speed);
		//Enviar velocidad a controlador AI

		//Enviar aceleracion a controlador AI?

		//solo vemos la distancia respectos de los ejes xz, no nos importa la distancia respecto a la altura
		distance = Vector3.Distance(new Vector3(this.transform.position.x, 0.0f, this.transform.position.z), 
									new Vector3(objective.transform.position.x, 0.0f, objective.transform.position.z));
		print("objetivo a distancia: " + distance);
		//enviar distancia al controlador AI

		Debug.DrawRay (transform.position, transform.forward * range);

		if (Physics.Raycast(transform.position, transform.forward, out delante, range))
			print("objeto delante a distancia: " + delante.distance);
			//enviar distancia al controlador AI

		Debug.DrawRay (transform.position, transform.up * range);

		if (Physics.Raycast(transform.position, transform.up, out arriba, range))
			print("objeto arriba a distancia: " + arriba.distance);
			//enviar distancia al controlador AI
		
		Debug.DrawRay (transform.position, -transform.right * range);

		if (Physics.Raycast(transform.position, -transform.right, out izquierda, range))
			print("obejto a la izquierda a distancia: " + izquierda.distance);
			//enviar distancia al controlador AI
		
		Debug.DrawRay (transform.position, transform.right * range);

		if (Physics.Raycast(transform.position, transform.right, out derecha, range))
			print("objeto a la derecha a distancia: " + derecha.distance);
			//enviar distancia al controlador AI

		Debug.DrawRay (transform.position, -transform.forward * range);

		if (Physics.Raycast(transform.position, -transform.forward, out atras, range))
			print("objeto atras a distancia: " + atras.distance);
			//enviar distancia al controlador AI

		Debug.DrawRay (transform.position, -transform.up * range);

		if (Physics.Raycast(transform.position, -transform.up, out debajo, range))
			print("objeto debajo a distancia: " + debajo.distance);
			//enviar distancia al controlador AI
	}
}
