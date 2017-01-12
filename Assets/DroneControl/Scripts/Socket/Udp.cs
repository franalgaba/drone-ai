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

	private Byte[] sendBytes;
	private Byte[] receiveBytes;

	private int Contador;
	private String mEnviar;
	private String mRecibir;

	// Use this for initialization
	void Start () {
		 
		Contador = 0;
		//el 11000 es el puerto del cliente
		udpClient = new UdpClient(11000);
		//esto para la hora de recibir
		RemoteIpEndPoint = new IPEndPoint(IPAddress.Loopback, 9900);

		try{
			Debug.Log("Cliente Drone conectado con el Servido");
			udpClient.Connect("localhost", 9900);

			mEnviar = "Mensaje" + Contador;
			sendBytes = Encoding.ASCII.GetBytes(mEnviar);

			// Sends a message to the host to which you have connected.
			Debug.Log("Conectado y Enviando String: " + mEnviar);
			udpClient.Send(sendBytes, sendBytes.Length);

			//otra forma de enviar sin establecer conexión
			//udpClientB.Send(sendBytes, sendBytes.Length, "AlternateHostMachineName", 11000);

			// Blocks until a message returns on this socket from a remote host.
			Debug.Log("Enviado y Esperando a recibir " + Contador + "   -hora: " + DateTime.Now.TimeOfDay);
			receiveBytes = udpClient.Receive(ref RemoteIpEndPoint); 
			mRecibir = Encoding.ASCII.GetString(receiveBytes);
			Debug.Log("Received data: " + mRecibir);

			Debug.Log("This message was sent from " + RemoteIpEndPoint.Address.ToString() +
				" on their port number " + RemoteIpEndPoint.Port.ToString());

			//udpClient.Close();

		}  
		catch (Exception e ) {
			Debug.Log(e.ToString());
		}
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Contador++;
		mEnviar = "Mensaje" + Contador;
		sendBytes = Encoding.ASCII.GetBytes(mEnviar);

		// Sends a message to the host to which you have connected.
		Debug.Log("Enviando String: " + mEnviar);
		udpClient.Send(sendBytes, sendBytes.Length);
		Debug.Log("ENVIADO");


		// Blocks until a message returns on this socket from a remote host.
		Debug.Log("Esperando a recibir " + Contador + "   -hora: " + DateTime.Now.TimeOfDay);
		receiveBytes = udpClient.Receive(ref RemoteIpEndPoint); 
		mRecibir = Encoding.ASCII.GetString(receiveBytes);
		Debug.Log("Received data: " + mRecibir);
	
	}

	//pendiente de implementar https://msdn.microsoft.com/es-es/library/system.net.sockets.udpclient(v=vs.110).aspx
	//  mirar https://msdn.microsoft.com/es-es/library/system.net.sockets.udpreceiveresult(v=vs.110).aspx
   //tengo sueño, ya no hago más hasta mañana

}





