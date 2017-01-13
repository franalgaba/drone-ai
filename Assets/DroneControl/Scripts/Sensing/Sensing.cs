using UnityEngine;
using System.Collections;

public class Sensing : MonoBehaviour {

	//distancia a la que detecta objetos
	public float range;
	public Transform objective;

	//para coger la velocidad a la que va el drone
	private Rigidbody drone;

	//angulo del obstaculo respecto de la orientacion del drone
	private float anguloObst;
	private float minimaDist = 99.9f;

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

    private Collider closestObs;
    private Transform obstaculo;

	private int layerMask = 1 << 8;
	private Vector3 crossVector;


    void Start()
    {
        maxDistaciaDest = Vector3.Distance(new Vector3(this.transform.position.x, 0.0f, this.transform.position.z),
                                    new Vector3(objective.transform.position.x, 0.0f, objective.transform.position.z));
    }


	void FixedUpdate () {		

		drone = this.GetComponentInParent<Rigidbody> ();
		speed = drone.velocity.magnitude;

		obstaculos = Physics.OverlapSphere(transform.position, range, layerMask);

		//Debug.Log ("numero de obstaculos: " + obstaculos.Length);

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

		if (closestObs != null) 
		{
			obstaculo = closestObs.transform;
			//girar hacia la derecha -> angulo negativo / girar hacia la izquierda -> angulo positivo
			//misma polaridad que en el paper creo yo
			anguloObj = Vector3.Angle(-transform.forward, obstaculo.position);
			crossVector = Vector3.Cross (-transform.forward, obstaculo.position);
			if (crossVector.y < 0)
				anguloObj = -anguloObj;
		}
		else Debug.Log("obstaculo es null");
			

        //solo vemos la distancia respectos de los ejes xz, no nos importa la distancia respecto a la altura
        distanciaDest = Vector3.Distance(new Vector3(this.transform.position.x, 0.0f, this.transform.position.z), 
										 new Vector3(objective.transform.position.x, 0.0f, objective.transform.position.z));

        anguloDest = Vector3.Angle(transform.forward, objective.position);
		crossVector = Vector3.Cross (transform.forward, objective.position);
		//girar hacia la derecha -> angulo positivo / girar hacia la izquierda -> angulo negativo
		//misma polaridad que en el paper creo yo
		if (crossVector.y < 0)
			anguloDest = -anguloDest;


		//Todo funciona como debe
		Debug.Log("maxima distancia del objetivo: " + maxDistaciaDest);
		//La velocidad maxima es 1.35 m/s, comprobado empiricamente, es lo maximo que puede llegar en linea recta hasta el final
		Debug.Log("velocidad en m/s: " + speed);
		Debug.Log("distacia al objetivo: " + distanciaDest + " a un angulo de: " + anguloDest);
		Debug.Log("distacia minima de obstaculo: " + minDistObj + " con a un angulo de: " + anguloObj);

		minDistObj = 99.9f;
	}
}
