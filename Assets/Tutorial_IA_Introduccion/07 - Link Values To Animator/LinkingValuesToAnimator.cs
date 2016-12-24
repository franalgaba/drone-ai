using UnityEngine;
using System.Collections;

public class LinkingValuesToAnimator : MonoBehaviour {
		
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

	void Awake()
	{
		anim = GetComponent<Animator>();
	}
	
	void Update()
	{
		if (Input.GetKeyDown ("space"))	CreaDestino (); // Creamos WayPoint al apretar space
		
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
			if(porcentaje < 120 && porcentaje > 40  && velocidadAvance < velocidadMaxima) 
				velocidadAvance += 10.0f * Time.deltaTime;
			if(porcentaje < 40 && porcentaje > 0 && velocidadAvance > velocidadMinima 
			   || distancia < 5.0f && velocidadAvance > velocidadMinima) 
				velocidadAvance -= 10.0f * Time.deltaTime;
			
			// Creacion del WayPoint al acercarse a cierta distancia			
			if (distancia < 1 && reachedPoint == false) {
				reachedPoint = true;
				CreaDestino ();
			}

			// Animacion
			speedNorm = velocidadAvance / velocidadMaxima;
			anim.SetFloat ("speed", speedNorm);
			Debug.Log("Velocidad de Min(0) a Max(1): " + speedNorm);
			
			//Debug.Log ("Distancia: " + distancia + " Estas a un " + porcentaje + "% del WayPoint");
			Debug.DrawRay(ray.origin, direccion * 1.0f, Color.blue);
		}
	}
	
	
	
	// ESTO PARA CREAR UN NUEVO WAYPOINT
	void CreaDestino()
	{
		// Creo un cubo en un sitio aleatorio determinado por un rango y destruyo el anterior creado si lo hay . . .
		if (WP != null) {
			Destroy (WP);
			bStored = false;
			distanciaInicial = 0;
		}
		GameObject destroyWayPoint = GameObject.Find("WayPoint" + ID);
		Destroy (destroyWayPoint);
		GameObject wayPoint = GameObject.CreatePrimitive(PrimitiveType.Cube);
		wayPoint.transform.position = new Vector3 (Random.Range(-7.0f,7.0f), 0.0f, Random.Range(-7.0f,7.0f));
		wayPoint.name = "WayPoint" + ID;
		//wayPoint.GetComponent<MeshFilter> ().mesh = null;
		wayPoint.GetComponent<Renderer>().material.color = new Color(0.25f, 0.0f, 0.0f, 0.0f);
		wayPoint.transform.localScale = new Vector3 (0.5f,0.5f,0.5f);
		reachedPoint = false;
		WP = wayPoint;
	}
	

}
