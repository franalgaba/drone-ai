using UnityEngine;
using System.Collections;

public class Sensing : MonoBehaviour {

	//distancia a la que detecta objetos
	public float range;
	public Transform objective;

	private float distance = 0.0f;
	private float speed = 0.0f;

	//sensor delante
	private RaycastHit delante;

	//sensores de la izquierda
	private RaycastHit cercaIzquierda;
	private Transform offsetCI;
	private RaycastHit izquierda;
	private Transform offsetI;
	private RaycastHit muyIzquierda;

	//sensores de la serecha
	private RaycastHit cercaDerecha;
	private Transform offsetCD;
	private RaycastHit derecha;
	private Transform offsetD;
	private RaycastHit muyDerecha;

	//para coger la velocidad a la que va el drone
	private Rigidbody drone;

	//angulo del obstaculo respecto de la orientacion del drone
	private float anguloObst;
	private float minimaDist = 99.9f;


	void FixedUpdate () {		

		drone = this.GetComponentInParent<Rigidbody> ();
		speed = drone.velocity.magnitude;
		print("velocidad en m/s: " + speed);
		//Enviar velocidad a controlador AI

		//solo vemos la distancia respectos de los ejes xz, no nos importa la distancia respecto a la altura
		distance = Vector3.Distance(new Vector3(this.transform.position.x, 0.0f, this.transform.position.z), 
									new Vector3(objective.transform.position.x, 0.0f, objective.transform.position.z));
		print("objetivo a distancia: " + distance);
		//enviar distancia al controlador AI

		//para pintar las rayitas del sensor
		Debug.DrawRay (transform.position, transform.forward * range);

		//Delante 0 grados

		if (Physics.Raycast (transform.position, transform.forward, out delante, range)) 
		{
			minimaDist = delante.distance;
			//si el obstaculo esta delante el angulo es 0
			anguloObst = 0.0f;
		}

		//Muy izquierda -90 grados

		Debug.DrawRay (transform.position, -transform.right * range);

		if (Physics.Raycast (transform.position, -transform.right, out muyIzquierda, range)) 
		{
			if (muyIzquierda.distance < minimaDist) 
			{
				minimaDist = muyIzquierda.distance;
				anguloObst = -90.0f;
			}
		}

		//Cerca Izquierda -30 grados

		offsetCI = transform;
		offsetCI.rotation = Quaternion.Euler(0,-30,0);

		Debug.DrawRay (transform.position, offsetCI.forward  * range);

		if (Physics.Raycast (transform.position, offsetCI.forward, out cercaIzquierda, range)) 
		{
			if (cercaIzquierda.distance < minimaDist) 
			{
				minimaDist = cercaIzquierda.distance;
				anguloObst = -30.0f;
			}
		}

		//Izquierda -60 grados

		offsetI = transform;
		offsetI.rotation = Quaternion.Euler(0.0f, -60.0f, 0.0f);

		Debug.DrawRay (transform.position, offsetI.forward  * range);

		if (Physics.Raycast (transform.position, offsetI.forward, out izquierda, range)) 
		{
			if (izquierda.distance < minimaDist) 
			{
				minimaDist = izquierda.distance;
				anguloObst = -60.0f;
			}
		}
			
		//MuyDerecha 90 grados

		Debug.DrawRay (transform.position, transform.right * range);

		if (Physics.Raycast (transform.position, transform.right, out muyDerecha, range)) 
		{
			if (muyDerecha.distance < minimaDist) 
			{
				minimaDist = muyDerecha.distance;
				anguloObst = 90.0f;
			}
		}

		//CercaDerecha 30 grados

		offsetCD = transform;
		offsetCD.rotation = Quaternion.Euler(0.0f, 30.0f, 0.0f);

		Debug.DrawRay (transform.position, offsetCD.forward  * range);

		if (Physics.Raycast (transform.position, offsetCD.forward, out cercaDerecha, range)) 
		{
			if (cercaDerecha.distance < minimaDist) 
			{
				minimaDist = cercaDerecha.distance;
				anguloObst = 30.0f;
			}
		}

		//Derecha 60 grados

		offsetD = transform;
		offsetD.rotation = Quaternion.Euler(0.0f, 60.0f, 0.0f);

		Debug.DrawRay (transform.position, offsetD.forward * range);

		if (Physics.Raycast (transform.position, offsetD.forward, out derecha, range)) 
		{
			if (derecha.distance < minimaDist) 
			{
				minimaDist = derecha.distance;
				anguloObst = 60.0f;
			}
		}

		print("distacia minima de obstaculo: " + minimaDist + " con a un angulo de: " + anguloObst);
		//Enviar distancia minima y el angulo del obstaculo

	}
}
