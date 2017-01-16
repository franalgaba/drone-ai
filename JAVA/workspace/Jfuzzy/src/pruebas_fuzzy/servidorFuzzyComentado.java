package pruebas_fuzzy;

import java.io.IOException;
import java.net.*;

import fuzzy.obstacleAvoidance;

public class servidorFuzzyComentado {
	
	public static void main(String[] args) {
		
		//constantes
		final int serverPort = 9900;
		final float menosMil = -1000.9f;
		
		// Create a buffer where to put a received datagram
		byte[] buf ;
		String recibido1;
		
		float distObs;          //distancia obstaculo cercano
		float angObs;           //angulo obstaculo cercano
		float angDest;          //angulo destino 
		float angDrone = 1.1f;	//cuanto tiene que girar el drone - comanda al motor		
		
		
		DatagramPacket packetDistObs;
		DatagramPacket packetAngObs; 
		DatagramPacket packetAngDest;
		
		DatagramPacket packetAng;
		
		DatagramSocket udpSocket;
		
		obstacleAvoidance OA;
		
		InetAddress clientAddress;
		int clientPort;
		try {
			
			OA = new obstacleAvoidance();
			// Create a udp socket for sending and receiving datagrams in serverPort
			udpSocket = new DatagramSocket(serverPort);
			
			System.out.println("Server: waiting for new datagrams...");
			buf = new byte[300];
			packetDistObs = new DatagramPacket(buf, buf.length);
			packetAngObs = new DatagramPacket(buf, buf.length);
			packetAngDest = new DatagramPacket(buf, buf.length);			
			
			while (true) {								
				System.out.println("Esperando a recibir.");
				
				// Receive the distObj from client
				udpSocket.receive(packetDistObs);
				recibido1=new String(packetDistObs.getData());
				System.out.println("El packetDistObs recibido es string: " + recibido1);
				distObs = stringToFloat(recibido1);
				distObs = convertir99(distObs);
				System.out.println("El packetDistObs recibido es float: " + distObs);
				
				// Receive the angObj from client
				udpSocket.receive(packetAngObs);
				recibido1=new String(packetAngObs.getData());
				System.out.println("El packetAngObs recibido es string: " + recibido1);
				angObs = stringToFloat(recibido1);
				System.out.println("El packetAngObs recibido es float: " + angObs);
				
				
				// Receive the angDest from client
				udpSocket.receive(packetAngDest);
				recibido1=new String(packetAngDest.getData());
				System.out.println("El packetAngDest recibido es string: " + recibido1);				
				angDest = stringToFloat(recibido1);				
				System.out.println("El packetAngDest recibido es: " + angDest);
				
				// Get the address of client from packet
				clientAddress = packetDistObs.getAddress();
				clientPort = packetDistObs.getPort();	
				
				// Salgo del While si el mensaje recibido es  -1000.9
				//SI SE HACEN GENERACIONES HACER UN WAIT PARA CUANDO SE PARE LA GENERACION
				if (distObs == menosMil ) { 
					System.out.println("Las comunicaciones han finalizado");
					break;}
				
				if (angDest != -6789f || angObs == -6789f || distObs == -6789f ){
					angDrone = OA.evaluar( angDest, angObs, distObs);
				} 
			
				angDrone=comprobarFloat(angDrone);
					

				
				System.out.println("Enviando el mensaje al cliente: " + angDrone);
				buf = (Float.toString(angDrone)).getBytes();
				
				// Send the datagram packet to client
				packetAng = new DatagramPacket(buf, buf.length, clientAddress,clientPort);
				udpSocket.send(packetAng);
				
				System.out.println("Mensaje enviado.");
				System.out.println("       --------------------------        ");
				
				buf = new byte[300];
				packetDistObs = new DatagramPacket(buf, buf.length);
				packetAngObs = new DatagramPacket(buf, buf.length);
				packetAngDest = new DatagramPacket(buf, buf.length);	
				
				
				
						
			}
			udpSocket.close();
		}
		catch (IOException e) {
			e.printStackTrace();
		}
	}
	
	private static float convertir99(float distObj) {
		float out = distObj;
		
		if(distObj>29.0f)
			out= 33.0f;
		
		return out;
	}
	
	private static float comprobarFloat(final float angDrone) {
		float out =0.1f;
		String convertido = String.valueOf(angDrone);
		
		convertido= convertido.replace(".0",".1");	
		
		out= stringToFloat(convertido);
		
		if ((out %1)==0){
			out = out + 0.1f;
		}
		
		return out;
	}

	
	private static float stringToFloat (final String convertir){
		float out = 0.1f;
		String convertido;
		int posicionDecimal;
		
		posicionDecimal = convertir.indexOf('.')+2;
		convertido= convertir.substring(0, posicionDecimal);
		
		try{
			out = Float.parseFloat(convertido);
		} catch (NumberFormatException e){
			return out=-6789f;
		}
		
		return out;
		
	}
	
}
