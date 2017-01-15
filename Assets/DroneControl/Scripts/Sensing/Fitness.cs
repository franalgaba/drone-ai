using UnityEngine;
using System.Collections;

public class Fitness : MonoBehaviour {

	public GameObject datos;

	private int puntuacion = 100;

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
			calculaFitness (0);
		}
		if (collision.collider.tag == "Objetivo") 
		{
			//this.gameObject.SetActive(false);
			calculaFitness (1);
			Debug.Log ("el fitness es de: " + puntuacion);
			//enviar puntuacion
		}
	}

	void calculaFitness(int accion)
	{
		maxDistanciaDest = datos.GetComponent<Sensing> ().getMaxDistanciaDest ();
		distanciaDest = datos.GetComponent<Sensing> ().getDistanciaDest ();
		tiempo = datos.GetComponent<Sensing> ().getTiempo ();

		if (accion == 0) {
			puntuacion -= 10;
		} else {
			if (distanciaDest > (maxDistanciaDest * 0.8)) {
				puntuacion -= 10;
			} else {
				if (distanciaDest > (maxDistanciaDest * 0.6)) {
					puntuacion -= 20;
				} else {
					if (distanciaDest > (maxDistanciaDest * 0.4)) {
						puntuacion -= 30;
					} else {
						if (distanciaDest > (maxDistanciaDest * 0.2)) {
							puntuacion -= 40;
						}
					}
				}
			}

			if (tiempo > 60) {
				puntuacion -= 10;
			}
		}
	}
}
