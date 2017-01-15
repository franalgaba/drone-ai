using UnityEngine;
using System.Collections;

using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Text;
using System;

public class Udp : MonoBehaviour {

	private UdpClient udpClient;
	private IPEndPoint RemoteIpEndPoint;

	private byte[] bufSendDistObj;
	private byte[] bufSendAngObj;
	private byte[] bufSendAngDest;
	private byte[] bufReceiveAng;

	float anguloMotor;

	public int clientPort;
	public int serverPort;

	private String mRecibir;

	//borrar
	private float distanciaObj = 1234.123f;
	private float anguloObj = -2233.2124f;
	private float anguloDest = -0.9f;

	// Use this for initialization
	void Start () {
		 
		udpClient = new UdpClient(clientPort);
		//esto para la hora de recibir
		RemoteIpEndPoint = new IPEndPoint(IPAddress.Loopback, serverPort);

		try{
			Debug.Log("Cliente Drone X conectado con el Servidor");
			udpClient.Connect("localhost", serverPort);
			Debug.Log("Conectado");

		}  
		catch (Exception e ) {
			Debug.Log(e.ToString());
		}
	
	}

	//	public float Evaluar (float distanciaObj, float anguloObj, float anguloDest) {
	void FixedUpdate () {
		try{

			bufSendDistObj = Encoding.ASCII.GetBytes(distanciaObj.ToString());
			bufSendAngObj  = Encoding.ASCII.GetBytes(anguloObj.ToString());
			bufSendAngDest = Encoding.ASCII.GetBytes(anguloDest.ToString());

			// Sends a message to the host to which you have connected.
			Debug.Log("Enviando distanciaObj: " + Encoding.ASCII.GetString(bufSendDistObj));
			udpClient.Send(bufSendDistObj, bufSendDistObj.Length);
			Debug.Log("Enviando anguloObj: " + Encoding.ASCII.GetString(bufSendAngObj));
			udpClient.Send(bufSendAngObj, bufSendAngObj.Length);
			Debug.Log("Enviando anguloDestino: " + Encoding.ASCII.GetString(bufSendAngDest));
			udpClient.Send(bufSendAngDest, bufSendAngDest.Length);

			// Blocks until a message returns on this socket from a remote host.
			Debug.Log("Esperando a recibir."  + "   -hora: " + DateTime.Now.TimeOfDay);

			bufReceiveAng = udpClient.Receive(ref RemoteIpEndPoint); 

			mRecibir =Encoding.ASCII.GetString(bufReceiveAng);
			anguloMotor = float.Parse(mRecibir);
			Debug.Log("Received data string: " + mRecibir);
			Debug.Log("Received data float: " + anguloMotor);
			 
			distanciaObj += 0.1f;
			anguloObj += 0.1f;
			anguloDest = anguloDest + 0.1f;


		}catch (Exception e ) {
			Debug.Log(e.ToString());
		}
	}

}
	
//mEnviar = "Mensaje" ;
//sendBytes = Encoding.ASCII.GetBytes(mEnviar);
//otra forma de enviar sin establecer conexión
//udpClientB.Send(sendBytes, sendBytes.Length, "AlternateHostMachineName", 11000);

//Debug.Log("This message was sent from " + RemoteIpEndPoint.Address.ToString() +
//	" on their port number " + RemoteIpEndPoint.Port.ToString());

//udpClient.Close();