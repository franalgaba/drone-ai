using UnityEngine;
using System.Collections;

public class Fitness : MonoBehaviour {

	public GameObject datosSensing;
	public GameObject datosWrite;

    private int numColision = 0;

	//maxima distancia del objetivo
	private float maxDistanciaDest;
	//distancia del objetivo
	private float distanciaDest;
	//tiempo en ejecucion
	private float tiempo;

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag  == "Obstaculo") 
		{
			//penalizar por cada obstaculo que se choque= 0;
			numColision++;
		}
		if (collision.collider.tag == "Objetivo") 
		{
			this.gameObject.SetActive(false);
			Debug.Log("FIN DEL JUEGO");
			datosWrite.GetComponent<writeResultados>().setColision(numColision);
			tiempo = datosSensing.GetComponent<Sensing>().getTiempo();
			datosWrite.GetComponent<writeResultados>().setTiempo(tiempo);
        }
	}
}
