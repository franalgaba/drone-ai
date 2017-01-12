using UnityEngine;
using System.Collections;

using System;
using System.IO;

public class Write : MonoBehaviour {

	private int Contador;
	// Create an instance of StreamWriter to write text to a file.
	private StreamWriter sw ;
	private const string fileName = "Prueba de escritura.txt";

	private bool sigue = true;

	// Use this for initialization
	void Start () {

		//si existe lo abre y si no lo crea
		sw = File.AppendText (fileName);

		// Add some text to the file.
		sw.Write("This is the ");
		sw.WriteLine("header for the file.");
		sw.WriteLine("-------------------");
		// Arbitrary objects can also be written to the file.
		sw.Write("The date is: ");
		sw.WriteLine(DateTime.Now);
		Contador = 0;
		sw.WriteLine ("El contador es: " + Contador);

		//sw.Close();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (sigue == true) {
			Contador++;
			sw.WriteLine ("El contador es: " + Contador + "   " + DateTime.Now.TimeOfDay);
		}

		//se puede poner otro contador para que cuando escriba X numero de lineas haga un flush
		if ((Contador%30) == 0) { 
			sw.Flush(); 
			sw.WriteLine ("Hago un flush");
			//sigue = !sigue;
		}	
	}

}
