using UnityEngine;
using System.Collections;

public class DifferentsPaths : MonoBehaviour {

	public		string			ID;
	public		float			velocidadMaxima		= 10.0f;
	public		float			velocidadMinima		= 1.0f;
	public		float			velocidadRotacion	= 7.0f;

	// VARIABLES PARA DIFERENTES PATHS Y ACCIONES

	public		GameObject[]	path;
				GameObject[] 	WPath;

	int 			numeroWP;
	GameObject		actualPath;

	////////////////////////////////////////////////

			
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
		if (actualPath == null)	TomaDecision (Random.Range(0,path.Length));
		if (WP == null && velocidadAvance > 0) velocidadAvance -= 10.0f * Time.deltaTime;
		if (WP != null) Movimiento ();

		// Animacion
		speedNorm = velocidadAvance / velocidadMaxima;
		anim.SetFloat ("speed", speedNorm);			
	}

	void Movimiento()
	{
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
		
		// Creacion del WayPoint al acercarse a cierta distancia			
		if (distancia < 1 && reachedPoint == false) {
			reachedPoint = true;
			SiguePath();
		}
		Debug.DrawRay(ray.origin, direccion * 1.0f, Color.blue);
	}
			
	void SiguePath()
	{
		// Asi reseteamos WP_number a 0
		if (WP == null) {
			WP_number = 0;
		}
		// Destruimos el WayPoint alcanzado para ir al nuevo WayPoint si lo hay
		if (WP != null) {
			Destroy (WP);
			bStored = false;
			distanciaInicial = 0;
		}
		// Si el numero total de WayPoints que forman el Path es igual al numero actual de WayPoint alcanzado, destruimos el Path
		if(numeroWP == WP_number) Destroy(actualPath);
		WP_number++;
		WP = GameObject.Find ("WayPoint" + WP_number);
		reachedPoint = false;
	}

	void TomaDecision(int decision)
	{
		actualPath = Instantiate(path[decision], path[decision].transform.position, path[decision].transform.rotation) as GameObject;
		numeroWP = actualPath.transform.childCount;
		Debug.Log("El path esta formado por: " + numeroWP + "Decision: " + decision);
		SiguePath();			
	}
}
