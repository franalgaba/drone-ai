using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

public class PatrolBetweenWalls : MonoBehaviour {

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
	bool			bCreandoWP			= false;
	float			waitTime			= 0.0f;
		
	void Awake()
	{
		anim = GetComponent<Animator>();
	}
	
	void Update()
	{
		if (Input.GetKeyDown ("space"))	CreaDestino (7,7); // Creamos WayPoint al apretar space
		// Creamos un Delay para dar tiempo a la rotacion
		if (bCreandoWP == true) {
			waitTime += 1.0f * Time.deltaTime;
			if(waitTime > 0.5f){
				// Una vez pasado el lapso de tiempo, liberaremos los valores para que cree un nuevo WayPoint si se debe
				waitTime = 0;
				bCreandoWP = false;
			}
		}
		
		if (WP != null) {
			Vector3 playerPosition = gameObject.transform.position;
			Vector3 target = WP.transform.position;
			Vector3 direccion = target - playerPosition;
			float distancia = Vector3.Distance (playerPosition, target);
			if(!bStored) {distanciaInicial = distancia;	bStored = true;}
			if(distanciaInicial != 0) porcentaje = ((distancia * 100) / distanciaInicial);			
			Ray ray = new Ray (playerPosition, target); // Este rayo va del player al target
			Ray rayForward = new Ray(ray.origin, transform.forward * 1.0f); // Este rayo va del player a lo que tenga delante de los morros
			// Comprobamos si hay obstaculos entre el player y el WP y si estan demasiado cerca
			RaycastHit[] hit;
			hit = Physics.RaycastAll (rayForward);
			if (hit.Length > 0) 
			{				
				foreach(RaycastHit h in hit)
				{
					GameObject obstaculo = h.transform.gameObject;
					Vector3 Obstaculo = obstaculo.transform.position;
					Vector3 direObstaculo = Obstaculo - playerPosition;
					float distObstaculo = Vector3.Distance (playerPosition, Obstaculo);
					Debug.Log("Primer Obstaculo " + obstaculo.name + " a distancia: " + distObstaculo);
					Debug.DrawRay(ray.origin, direObstaculo * 1.0f, Color.yellow);
					// Si se acerca mucho al obstaculo le decremento la velocidad
					if(distObstaculo < 5.5f && velocidadAvance > velocidadMinima) velocidadAvance -= 10.0f * Time.deltaTime;
					// Si la distancia hasta el obstaculo es poca, crea otro WP relativo a la posicion del personaje
					// Para poder crear el WayPoint tb debera estar bCreandWP en false, para que solo cree 1 WayPoint y espere
					// Hacemos esto para que no se quede avanzando detenido por un obstaculo
					// No lo hacemos con porcentaje puesto que en este caso no hace falta, ya que la distancia minima a la que choco
					// siempre es la misma
					if(distObstaculo < 3.1f && bCreandoWP == false) CreaDestino ((playerPosition.x + 3), (playerPosition.z + 3));
					break; // Rompiendo aqui cogemos solo el primer obstaculo que se encuentra
				}				
			}
			// MOVIMIENTO
			// Encarando al WayPoint controlando la velocidad de rotacion
			transform.rotation = Quaternion.Slerp (transform.rotation, 
			                                       Quaternion.LookRotation (WP.transform.position - transform.position), 
			                                       velocidadRotacion * Time.deltaTime);
			// Avanzando
			transform.position += transform.forward * velocidadAvance * Time.deltaTime;
			// Incremento y Decremento de velocidad
			if(porcentaje < 120 && porcentaje > 40  && velocidadAvance < velocidadMaxima) 
				velocidadAvance += 15.0f * Time.deltaTime;
			if(porcentaje < 40 && porcentaje > 0 && velocidadAvance > velocidadMinima 
			   || distancia < 5.0f && velocidadAvance > velocidadMinima) 
				velocidadAvance -= 10.0f * Time.deltaTime;
			
			// Creacion del WayPoint al acercarse a cierta distancia			
			if (distancia < 1 && reachedPoint == false) {
				reachedPoint = true;
				// Creamos un destino en relacion a la posicion actual del NPC
				CreaDestino ((playerPosition.x + 5), (playerPosition.z + 5));
			}
			
			// Animacion
			speedNorm = velocidadAvance / velocidadMaxima;
			anim.SetFloat ("speed", speedNorm);

			// DEBUGS
			Debug.DrawRay(ray.origin, direccion * 1.0f, Color.blue);
		}
	}
	
	
	
	// ESTO PARA CREAR UN NUEVO WAYPOINT
	void CreaDestino(float rangoX, float rangoZ)
	{
		// Creo un cubo en un sitio aleatorio determinado por un rango y destruyo el anterior creado si lo hay . . .
		if (WP != null) {
			Destroy (WP);
			bStored = false;
			distanciaInicial = 0;
		}
		// Para que esta funcion no sea rellamada mientras estoy creando un WayPoint, seteamos bCreandoWP a true
		bCreandoWP = true;
		// Creamos el WayPoint de destino
		GameObject wayPoint = GameObject.CreatePrimitive(PrimitiveType.Cube);
		wayPoint.transform.position = new Vector3 (Random.Range(-rangoX ,rangoX), 0, Random.Range(-rangoZ ,rangoZ));
		wayPoint.name = "WayPoint" + ID;
		//wayPoint.GetComponent<MeshFilter> ().mesh = null; // Habilitar para hacer desaparecer la mesh de WP
		wayPoint.GetComponent<Renderer>().material.color = new Color(0.25f, 0.0f, 0.0f, 0.0f);
		wayPoint.transform.localScale = new Vector3 (0.5f,0.5f,0.5f);
		reachedPoint = false;
		WP = wayPoint;
	}
}
