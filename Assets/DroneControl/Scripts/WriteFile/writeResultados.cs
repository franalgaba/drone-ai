using UnityEngine;
using System.Collections;

using System;
using System.IO;

public class writeResultados : MonoBehaviour {

	// Create an instance of StreamWriter to write text to a file.
	private StreamWriter sw ;
	private const string fileName = "Resultados.txt";


	// Use this for initialization
	void Start () {

		//si existe lo abre y si no lo crea
		sw = File.AppendText (fileName);

		// Add some text to the file.
		sw.WriteLine("Resultados obtenidos en la simulacion");
		sw.WriteLine("    ---------------------------      ");
	}

	// Escribe el numero de colisiones
	public void setColision (int numColisiones) {

		sw.WriteLine ("El num de colisiones es: " + numColisiones);
		sw.Flush(); 

	}

	// Escribe el tiempo tardado en llegar al objetivo
	public void setTiempo (float tiempo) {

		sw.WriteLine ("El tiempo tardadado en llegar al objetivo es de: " + tiempo + " segundos.");
		sw.Flush(); 
		sw.Close();

	}
}