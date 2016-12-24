using UnityEngine;
using System.Collections;

public class FollowPath : MonoBehaviour {

	public		string			ID;
	public		float			velocidadMaxima		= 10.0f;
	public		float			velocidadMinima		= 1.0f;
	public		float			velocidadRotacion	= 7.0f;
	
	float			velocidadAvance		= 8.0f;
	GameObject		WP					= null;	
	float			distanciaInicial	= 0;
	float 			porcentaje			= 100;
	bool			bStored				= false;
	bool			reachedPoint		= false;
	Animator		anim;
	float			speedNorm;
	int				WP_number 			= 0;
	
	void Awake()
	{
		anim = GetComponent<Animator>();
	}
	
	void Update()
	{
		if (Input.GetKeyDown ("space"))	SiguePath(); // Creamos WayPoint al apretar space

		if (WP == null && velocidadAvance > 0) velocidadAvance -= 10.0f * Time.deltaTime;
		if (WP != null) {
			Vector3 PuntoA = gameObject.transform.position;
			Vector3 PuntoB = WP.transform.position;
			Vector3 direccion = PuntoB - PuntoA;
			float distancia = Vector3.Distance (PuntoA, PuntoB);
			
			if(distanciaInicial != 0) porcentaje = ((distancia * 100) / distanciaInicial);
			
			Ray ray = new Ray (PuntoA, PuntoB);
			// Necesitamos recoger la distancia que hay al principio de la deteccion de un WayPoint para hacer el %
			if(!bStored) {
				distanciaInicial = distancia;
				bStored = true;
			}
			// MOVIMIENTO
			// Encarando al WayPoint controlando la velocidad de rotacion
			transform.rotation = Quaternion.Slerp (transform.rotation, 
			                                       Quaternion.LookRotation (WP.transform.position - transform.position), 
			                                       velocidadRotacion * Time.deltaTime);
			// Avanzando
			gameObject.transform.position += gameObject.transform.forward * velocidadAvance * Time.deltaTime;
			// Incremento y Decremento de velocidad
			if(porcentaje < 120 && porcentaje > 20  && velocidadAvance < velocidadMaxima) 
				velocidadAvance += 10.0f * Time.deltaTime;
			if(porcentaje < 20 && porcentaje > 0 && velocidadAvance > velocidadMinima 
			   || distancia < 5.0f && velocidadAvance > velocidadMinima) 
				velocidadAvance -= 10.0f * Time.deltaTime;
			
			// Al alcanzar un WayPoint ira a por el siguiente			
			if (distancia < 1 && reachedPoint == false) {
				reachedPoint = true;
				SiguePath();
			}
			Debug.DrawRay(ray.origin, direccion * 1.0f, Color.blue);
		}
		// Animacion
		speedNorm = velocidadAvance / velocidadMaxima;
		anim.SetFloat ("speed", speedNorm);
		Debug.Log("Velocidad de Min(0) a Max(1): " + speedNorm);
		Debug.Log("Velocidad de Avance: " + velocidadAvance);
	}

	void SiguePath()
	{
		if (WP == null) {
			WP_number = 0;
		}
		if (WP != null) {
			Destroy (WP);
			bStored = false;
			distanciaInicial = 0;
		}
		WP_number++;
		WP = GameObject.Find ("WayPoint" + WP_number);
		reachedPoint = false;
	}
}
