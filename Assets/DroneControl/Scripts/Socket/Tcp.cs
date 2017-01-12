using UnityEngine;
using System.Collections;

using System.Net.Sockets;
using System.IO;
using System;

public class Tcp : MonoBehaviour {

	private TcpClient tcpClient;
	private StreamWriter streamWriter;
	private StreamReader streamReader;

	private int Contador;
	private String mEnviar;
	private String mRecibir;

	void Start () {       
		try {
			Debug.Log("Conectando con el cliente");
			tcpClient = new TcpClient("localhost", 8800);
			Debug.Log("Conectado");

			NetworkStream networkStream = tcpClient.GetStream();
			streamWriter = new StreamWriter(networkStream);
			//BinaryWriter bw = new BinaryWriter(networkStream);	

			mEnviar = "Funcionando"+Contador;
			Debug.Log("Enviando String: " + mEnviar);
			streamWriter.WriteLine(mEnviar);
			Debug.Log("ENVIADO String: " + mEnviar);
			streamWriter.Flush();
			Debug.Log("Hecho el Flush");

			// PARTE DE RECIBIR
			streamReader = new StreamReader(networkStream);

			mRecibir = "noFunciona";
			Contador=0;

			Debug.Log("Esperando a recibir " + Contador + "   -hora: " + DateTime.Now.TimeOfDay);
			mRecibir = streamReader.ReadLine();
			Debug.Log("Received data: " + mRecibir);

		} catch (SocketException e) {
			Debug.Log ("SocketException: {0}" + e);			
		}


	}

	// Update is called once per frame
	void FixedUpdate () {
		Contador++;

		mEnviar = "Funcionando"+ Contador;

		Debug.Log("Enviando String: " + mEnviar);
		streamWriter.WriteLine(mEnviar);
		Debug.Log("ENVIADO String: " + mEnviar);
		streamWriter.Flush();
		Debug.Log("Hecho el Flush");

		// PARTE DE RECIBIR
		mRecibir = "noFunciona";

		Debug.Log("Esperando a recibir " + Contador + "hora" + DateTime.Now.TimeOfDay);
		mRecibir = streamReader.ReadLine();
		Debug.Log("Received data: " + mRecibir + "\n");

	}
}