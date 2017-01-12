package fuzzy;

import java.io.IOException;
import java.net.*;

public class servidorFuzzy {
	
	public static void main(String[] args) {
		
		//constantes
		final int serverPort = 9900;
		final float menosMil = -1000;
		
		// Create a buffer where to put a received datagram
		byte[] buf ;

		float distObj;
		float angObj;
		float angDest;
			
		float angDrone;
		DatagramPacket packetDistObj;
		DatagramPacket packetAngObj; 
		DatagramPacket packetAngDest;
		
		DatagramPacket packetAng;
		
		DatagramSocket udpSocket;
		
		InetAddress clientAddress;
		int clientPort;
		try {
			// Create a udp socket for sending and receiving datagrams in serverPort
			udpSocket = new DatagramSocket(serverPort);
			
			System.out.println("Server: waiting for new datagrams...");
			buf = new byte[300];
			packetDistObj = new DatagramPacket(buf, buf.length);
			packetAngObj = new DatagramPacket(buf, buf.length);
			packetAngDest = new DatagramPacket(buf, buf.length);			
			
			while (true) {								
				System.out.println("Esperando a recibir.");
				
				// Receive the distObj from client
				udpSocket.receive(packetDistObj);
				distObj = Float.parseFloat(new String(packetDistObj.getData()));
				System.out.println("El packetDistObj recibido es float: " + distObj);
				System.out.println("El packetDistObj recibido es string: " + new String(packetDistObj.getData()));
				
				// Receive the angObj from client
				udpSocket.receive(packetAngObj);
				angObj = Float.parseFloat(new String(packetAngObj.getData()));
				System.out.println("El packetAngObj recibido es float: " + angObj);
				System.out.println("El packetAngObj recibido es string: " + new String(packetAngObj.getData()));
				
				// Receive the datagram from client
				udpSocket.receive(packetAngDest);
				angDest = Float.parseFloat(new String(packetAngDest.getData()));
				System.out.println("El packetAngDest recibido es: " + angDest);
				System.out.println("El packetAngDest recibido es string: " + new String(packetAngDest.getData()));
				
				// Get the address of client from packet
				clientAddress = packetDistObj.getAddress();
				clientPort = packetDistObj.getPort();	
				
				// Salgo del While si el mensaje que envio al servidor es null o stop.
				if (distObj == menosMil ) { 
					System.out.println("Las comunicaciones han finalizado");
					break;}
				
				//AQUI SE LLAMARIA A LA FUNCION A EVALUAR FUZZY
				angDrone= 99999;
				
				System.out.println("Enviando el mensaje al cliente: " + angDrone);
				buf = (Float.toString(angDrone)).getBytes();
				
				packetAng = new DatagramPacket(buf, buf.length, clientAddress,clientPort);
				// Send the datagram packet to client
				udpSocket.send(packetAng);
				System.out.println("Mensaje enviado.");
				System.out.println("       --------------------------        ");
				
				//SI SE HACEN GENERACIONES HACER UN WAIT PARA CUANDO SE PARE LA GENERACION
				//POR SI PETA EL 
			}
			udpSocket.close();
		}
		// udpSocket.close();
		catch (IOException e) {
			e.printStackTrace();
		}
	}
}

//comprobar como se envia angDrone
//comprobar los buffer en varias recepciones y condistintos tamaños para ver como se quedan

//				distObj = ByteBuffer.wrap(packetDistObj.getData()).getFloat();
