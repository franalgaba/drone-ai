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
			//aumentamos el numero de colisiones si hay un choque con un obstaculo
			numColision++;
		}
		if (collision.collider.tag == "Objetivo") 
		{
			//si chocamos con el objetivo se da por finalizada la simulacion generando un archivo .txt con los resultados
			this.gameObject.SetActive(false);
			Debug.Log("FIN DEL JUEGO");
			datosWrite.GetComponent<writeResultados>().setColision(numColision);
			tiempo = datosSensing.GetComponent<Sensing>().getTiempo();
			datosWrite.GetComponent<writeResultados>().setTiempo(tiempo);
        }
	}
}
