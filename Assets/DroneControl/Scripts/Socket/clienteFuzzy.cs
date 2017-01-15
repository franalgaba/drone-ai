using UnityEngine;
using System.Collections;

using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Text;
using System;

public class clienteFuzzy : MonoBehaviour {

	private UdpClient udpClient;
	private IPEndPoint RemoteIpEndPoint;

	private byte[] bufSendDistObj;
	private byte[] bufSendAngObj;
	private byte[] bufSendAngDest;
	private byte[] bufReceiveAng;

	private float anguloMotor ;

	public int clientPort; //11000
	public int serverPort; //9900

	private String mRecibir;

	// Use this for initialization
	void Start () {
		 
		udpClient = new UdpClient(clientPort);
		//esto para la hora de recibir
		RemoteIpEndPoint = new IPEndPoint(IPAddress.Loopback, serverPort);

		try{
			Debug.Log("Cliente Drone conectado con el Servidor");
			udpClient.Connect("localhost", serverPort);
			Debug.Log("Conectado");

		}  
		catch (Exception e ) {
			Debug.Log(e.ToString());
		}
	
	}

	//OJO SIEMPRE QUE SE LLAME A ESTA FUNCION SE TIENEN QUE PONER LOS ARGUMENTOS CON ALGUN VALOR DECIMAL EJEMPLO XXX.1
	public float Evaluar (float distanciaObj, float anguloObj, float anguloDest) {
		try{

			bufSendDistObj = Encoding.ASCII.GetBytes(distanciaObj.ToString("n1"));
			bufSendAngObj  = Encoding.ASCII.GetBytes(anguloObj.ToString("n1"));
			bufSendAngDest = Encoding.ASCII.GetBytes(anguloDest.ToString("n1"));

			// Sends a message to the host to which you have connected.
			udpClient.Send(bufSendDistObj, bufSendDistObj.Length);
			udpClient.Send(bufSendAngObj, bufSendAngObj.Length);
			udpClient.Send(bufSendAngDest, bufSendAngDest.Length);

			// Blocks until a message returns on this socket from a remote host.
			bufReceiveAng = udpClient.Receive(ref RemoteIpEndPoint); 
			mRecibir =Encoding.ASCII.GetString(bufReceiveAng);
			anguloMotor = float.Parse(mRecibir);

		}catch (Exception e ) {
			Debug.Log(e.ToString());
			anguloMotor = 0.0f;
		}

		return anguloMotor;
	}

}