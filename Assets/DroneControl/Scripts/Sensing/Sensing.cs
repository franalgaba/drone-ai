using UnityEngine;
using System.Collections;

public class Sensing : MonoBehaviour {

	public GameObject funcion;

	//distancia a la que detecta objetos
	public float range;
	public Transform objective;

	//para coger la velocidad a la que va el drone
	private Rigidbody drone;

    private Collider[] obstaculos;
    private Vector3 closestPoint;
    private float distObs = 99.9f;

    private float speed = 0.0f;
    //variables a enviar
    //distancia al objetivo
    private float distanciaDest = 0.0f;
    //distancia del obstaculo
    private float minDistObj = 99.9f; 
    //angulo del obstaculo
    private float anguloObj; 		  
    //maxima distancia al objetivo
    private float maxDistaciaDest;
    //angulo del objetivo
    private float anguloDest;
	//tiempo en ejecucion
	private float tiempo;

    private Collider closestObs;
    private Transform obstaculo;

	private int layerMask = 1 << 8;
	private Vector3 crossVector;

	//angulo que debe girar el drone
	private float anguloGiro = 0.0f;

    void Start()
    {
		//cogemos la maxima distancia al destino
        maxDistaciaDest = Vector3.Distance(new Vector3(this.transform.position.x, 0.0f, this.transform.position.z),
                                    new Vector3(objective.transform.position.x, 0.0f, objective.transform.position.z));
    }


	void FixedUpdate () {
		//obtenemos el tiempo que lleva ejecutandose
		tiempo = Time.realtimeSinceStartup;

		drone = this.GetComponentInParent<Rigidbody> ();
		//obtenemos la velocidad del drone
		speed = drone.velocity.magnitude;

		//obtenemos todos los obstaculos dentro del rango de la esfera que recubre el drone
		obstaculos = Physics.OverlapSphere(transform.position, range, layerMask);

		//recorremos todos los obstaculos pero nos quedamos con el que este mas cerca del drone
        int i = 0;
        while( i < obstaculos.Length)
        {
			closestPoint = obstaculos[i].ClosestPointOnBounds(transform.position);
			distObs = Vector3.Distance(closestPoint, transform.position);
			if (distObs < minDistObj)
			{
				closestObs = obstaculos[i];
				minDistObj = distObs;
			}
			i++;
        }

		//obtenemos el angulo del drone respecto del obstaculo
		if (closestObs != null) 
		{
			obstaculo = closestObs.transform;
			anguloObj = Vector3.Angle(-transform.forward, obstaculo.position);
			//vemos la polaridad del angulo
			crossVector = Vector3.Cross (-transform.forward, obstaculo.position);
			if (crossVector.y < 0)
				anguloObj = -anguloObj;
		}
		else Debug.Log("obstaculo es null");
			

        //comprobamos la distancia actual al destino
        distanciaDest = Vector3.Distance(new Vector3(this.transform.position.x, 0.0f, this.transform.position.z), 
										 new Vector3(objective.transform.position.x, 0.0f, objective.transform.position.z));

		//cogemos el angulo de la direccion del drone respecto de la posicion del destino
        anguloDest = Vector3.Angle(transform.forward, objective.position);
		crossVector = Vector3.Cross (transform.forward, objective.position);
		if (crossVector.y < 0)
			anguloDest = -anguloDest;


		Debug.Log("distObstaculo: " + minDistObj + " angObstaculo: " + anguloObj + " anguloDest: " + anguloDest);
		minDistObj = convertirCeroFloat (minDistObj);
		anguloObj = (convertirCeroFloat (anguloObj)) *  -1.0f;
		anguloDest = (convertirCeroFloat (anguloDest));
		Debug.Log("-- Enviado  distObstaculo: " + minDistObj + " angObstaculo: " + anguloObj + " anguloDest: " + anguloDest);

		anguloGiro = funcion.GetComponent<clienteFuzzy> ().Evaluar (minDistObj, anguloObj, anguloDest);
		Debug.Log ("++ Recibido  angDrone: " + anguloGiro);


		//reiniciamos la distancia minima
		minDistObj = 99.9f;
	}

	private float convertirCeroFloat (float inicial){
		float final;

		if (inicial < 0.1f && inicial > -0.1f) {
			final = 0.1f;
		} else {
			final = inicial ;
		}

		return final;
	}

	public float getSpeed()
	{
		return speed;
	}

	public float getDistanciaDest()
	{
		return distanciaDest;
	}

	public float getMinDistObj()
	{
		return minDistObj;
	}

	public float getAnguloObj()
	{
		return anguloObj;
	}

	public float getMaxDistanciaDest()
	{
		return maxDistaciaDest;
	}

	public float getAnguloDest()
	{
		return anguloDest;
	}
	public float getTiempo()
	{
		return tiempo;
	}
	public float getAnguloGiro()
	{
		return anguloGiro;
	}

}
