using UnityEngine;
using System.Collections;

public class Triggers : MonoBehaviour {

	private Sensing datos;

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("Obstaculo")) 
		{
			//penalizar
			calculaFitness ();
		}
		if (collision.collider.CompareTag("Objetivo")) 
		{
			//premiar
			calculaFitness ();
		}
	}

	void calculaFitness()
	{
		datos.getSpeed ();
		//etc
	}
}
