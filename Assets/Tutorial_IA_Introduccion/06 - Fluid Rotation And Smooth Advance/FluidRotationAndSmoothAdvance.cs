using UnityEngine;
using System.Collections;

public class FluidRotationAndSmoothAdvance : MonoBehaviour 
{
	public		string			ID; // En este caso la ID funciona por si hay mas de un NPC para que siga su ruta y no la de otro NPC
	public		float			velocidadMaxima		= 10.0f;
	public		float			velocidadMinima		= 1.0f;
	public		float			velocidadRotacion	= 7.0f;

				float			velocidadAvance		= 8.0f;
				GameObject		WP					= null;	
				float			distanciaInicial	= 0;
				float 			porcentaje			= 100;
				bool			bStored				= false;
				bool			reachedPoint		= false;				
	
	void Update()
	{
		if (Input.GetKeyDown ("space"))	CreaDestino (); // Creamos WayPoint al apretar space

		// Cuando existe un WayPoint, ira a su encuentro
		if (WP != null) {
			// Recapitulamos primero la informacion necesaria para tomar decisiones
			Vector3 PuntoA = gameObject.transform.position;
			Vector3 PuntoB = WP.transform.position;
			Vector3 direccion = PuntoB - PuntoA;
			float distancia = Vector3.Distance (PuntoA, PuntoB);

			// Convertimos la distancia en porcentaje
			if(distanciaInicial != 0) porcentaje = ((distancia * 100) / distanciaInicial);

			Ray ray = new Ray (PuntoA, PuntoB);
			// Necesitamos recoger la distancia que hay al principio de la deteccion de un WayPoint para hacer el %
			// Cuando bStored esta en true quiere decir que hay una WayPoint y vamos en ruta hacia el
			if(!bStored) {
				distanciaInicial = distancia;
				bStored = true;
			}
			// MOVIMIENTO
			// Encarando al WayPoint controlando la velocidad de rotacion
			transform.rotation = Quaternion.Slerp (transform.rotation, 
			                                       Quaternion.LookRotation (direccion), 
			                                       velocidadRotacion * Time.deltaTime);
			// Avanzando
			transform.position += transform.forward * velocidadAvance * Time.deltaTime;
			// Incremento y Decremento de velocidad
			if(porcentaje < 120 && porcentaje > 60  && velocidadAvance < velocidadMaxima) 
				velocidadAvance += 10.0f * Time.deltaTime;
			if(porcentaje < 60 && porcentaje > 0 && velocidadAvance > velocidadMinima) 
				velocidadAvance -= 10.0f * Time.deltaTime;

			// Creacion del WayPoint al acercarse a cierta distancia y a la vez destruir el WayPoint alcanzado			
			if (distancia < 1 && reachedPoint == false) {
				reachedPoint = true;
				CreaDestino ();
			}
		}
	}



	// ESTO PARA CREAR UN NUEVO WAYPOINT
	void CreaDestino()
	{
		// Destruimos el anterior WayPoint creado si lo hay
		if (WP != null) {
			Destroy (WP);
			bStored = false;
			distanciaInicial = 0;
		}
		// Creamos un nuevo WayPoint en un rango determinado y le damos un nombre un color y una escala
		GameObject wayPoint = GameObject.CreatePrimitive(PrimitiveType.Cube);
		wayPoint.transform.position = new Vector3 (Random.Range(-7.0f,7.0f), 0.3f, Random.Range(-7.0f,7.0f));
		wayPoint.name = "WayPoint" + ID;
		wayPoint.GetComponent<MeshFilter> ().mesh = null;
		//wayPoint.GetComponent<Renderer>().material.color = new Color(0.25f, 0.0f, 0.0f, 0.0f);
		wayPoint.transform.localScale = new Vector3 (0.5f,0.5f,0.5f);
		// Seteamos reachedPoint a false para que sepa que lo ha de ir a buscar
		reachedPoint = false;
		// Igualamos WP al WayPoint creado
		WP = wayPoint;
	}

}
