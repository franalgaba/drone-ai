using UnityEngine;

public class Estados : MonoBehaviour {

	public enum Estado
	{
		Quieto,
		Desplazarse,
		Agacharse,
	}
	public	Estado	curState;

	public		float			velocidadMaxima		= 10.0f;
	public		float			velocidadMinima		= 1.0f;
	public		float			velocidadRotacion	= 7.0f;

	Vector3		NPC;	
	GameObject	WP;
	int			WP_number 			= 0;

	float		velocidadAvance		= 8.0f;
	float		distanciaInicial	= 0;
	float		porcentaje			= 100;
	bool		bStored				= false;
	bool		reachedPoint		= false;

	Animator	anim;
	float		speedNorm			= 0;


	// // // // // // // // // // // // // // // //

	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		// ESCOGIENDO LA DECISION
		if (Input.GetKeyDown ("1")) {
			WP = GameObject.Find("WayPoint" + WP_number); // Cogemos el proximo destino
			SiguePath();
			curState = Estado.Desplazarse;
		} 
		if(Input.GetKeyDown("2")) {
			curState = Estado.Quieto;
		}
		if(Input.GetKeyDown("3")) {
			curState = Estado.Agacharse;
		}
		// EJECUCION DE LA DECISION
		ActualizaEstado();

		// Animacion
		// Si no se esta desplazando, el NPC debe frenar
		if (curState != Estado.Desplazarse && velocidadAvance > 0) {
			velocidadAvance -= 20.0f * Time.deltaTime;
			// Poniendo aqui tb el movimiento, hago que frene sin brusquedad
			transform.position += transform.forward * (velocidadAvance * 1.5f) * Time.deltaTime;
		}
		anim.SetFloat ("speed", speedNorm);
	}

	void ActualizaEstado()
	{
		switch (curState)
		{		
		case Estado.Quieto: 		ActualizaQuieto();		break;
		case Estado.Desplazarse: 	ActualizaDesplazarse(); break;
		case Estado.Agacharse: 		ActualizaAgacharse(); 	break;		
		}
	}

	void ActualizaQuieto()
	{
		// Nos aseguramos de dar el valor correcto para el Animator
		if (speedNorm < -0.1f)
			speedNorm += 3.0f * Time.deltaTime;
		else
			speedNorm = velocidadAvance / velocidadMaxima;
	}

	void ActualizaDesplazarse()
	{
		// Esto es para que haga un frenado suave
		if (speedNorm < -0.1f)
			speedNorm += 3.0f * Time.deltaTime;
		else
		speedNorm = velocidadAvance / velocidadMaxima;

		Vector3 NPC = this.gameObject.transform.position;
		Vector3 WayPoint = WP.transform.position;
		Vector3 direccion = WayPoint - NPC;
		float distancia = Vector3.Distance (NPC, WayPoint);

		if(distanciaInicial != 0) porcentaje = ((distancia * 100) / distanciaInicial);
		
		Ray ray = new Ray (NPC, WayPoint);
		// Necesitamos recoger la distancia que hay al principio de la deteccion de un WayPoint para hacer el %
		if(!bStored) {
			distanciaInicial = distancia;
			bStored = true;
		}
		// Encarando al WayPoint controlando la velocidad de rotacion
		transform.rotation = Quaternion.Slerp (transform.rotation, 
		                                       Quaternion.LookRotation (WP.transform.position - transform.position), 
		                                       velocidadRotacion * Time.deltaTime);
		// Movimiento
		transform.position += transform.forward * velocidadAvance * Time.deltaTime;

		if(porcentaje < 120 && porcentaje > 20  && velocidadAvance < velocidadMaxima) 
			velocidadAvance += 10.0f * Time.deltaTime;
		if(porcentaje < 20 && porcentaje > 0 && velocidadAvance > velocidadMinima 
		   || distancia < 5.0f && velocidadAvance > velocidadMinima) 
			velocidadAvance -= 10.0f * Time.deltaTime;
		
		// Creacion del WayPoint al acercarse a cierta distancia			
		if (distancia < 1 && reachedPoint == false) {
			reachedPoint = true;
			SiguePath ();
		}
		Debug.Log (distancia);
	}

	void SiguePath()
	{
		Debug.Log (WP_number);
		if (WP == null) {
			WP_number = 0;
		}
		if (WP != null) {
			bStored = false;
			distanciaInicial = 0;
		}
		WP_number++;
		// Para que en este caso haga una loop
		if (WP_number >= 3)
			WP_number = 0;
		WP = GameObject.Find ("WayPoint" + WP_number);
		reachedPoint = false;
	}

	void ActualizaAgacharse()
	{
		Debug.Log ("ME ESTOY AGACHANDO");
		// Cando nos agachamos, tb frenamos. Esto da la sensacion de derrape
		if (speedNorm > -0.25f)	speedNorm -= 5.0f * Time.deltaTime;
	}
}