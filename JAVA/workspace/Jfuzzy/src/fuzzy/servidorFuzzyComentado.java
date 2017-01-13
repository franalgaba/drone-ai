package fuzzy;

import java.io.IOException;
import java.net.*;

public class servidorFuzzyComentado {
	
	public static void main(String[] args) {
		
		//constantes
		final int serverPort = 9900;
		final float menosMil = -1000.9f;
		
		// Create a buffer where to put a received datagram
		byte[] buf ;

		float distObj;
		float angObj;
		float angDest;
		//borrar
		float angDrone = -88.99f;
		String recibido1;
		
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
				recibido1=new String(packetDistObj.getData());
				System.out.println("El packetDistObj recibido es string: " + recibido1);
				distObj = stringToFloat(recibido1);
				System.out.println("El packetDistObj recibido es float: " + distObj);
				
				// Receive the angObj from client
				udpSocket.receive(packetAngObj);
				recibido1=new String(packetAngObj.getData());
				System.out.println("El packetAngObj recibido es string: " + recibido1);
				angObj = stringToFloat(recibido1);
				System.out.println("El packetAngObj recibido es float: " + angObj);
				
				
				// Receive the datagram from client
				udpSocket.receive(packetAngDest);
				recibido1=new String(packetAngDest.getData());
				System.out.println("El packetAngDest recibido es string: " + recibido1);				
				angDest = stringToFloat(recibido1);				
				System.out.println("El packetAngDest recibido es: " + angDest);
				
				// Get the address of client from packet
				clientAddress = packetDistObj.getAddress();
				clientPort = packetDistObj.getPort();	
				
				// Salgo del While si el mensaje que envio al servidor es null o stop.
				if (distObj == menosMil ) { 
					System.out.println("Las comunicaciones han finalizado");
					break;}
				
				//AQUI SE LLAMARIA A LA FUNCION A EVALUAR FUZZY
				//angDrone= 1234.5678f;
				
				System.out.println("Enviando el mensaje al cliente: " + angDrone);
				buf = (Float.toString(angDrone)).getBytes();
				
				// Send the datagram packet to client
				packetAng = new DatagramPacket(buf, buf.length, clientAddress,clientPort);
				udpSocket.send(packetAng);
				
				System.out.println("Mensaje enviado.");
				System.out.println("       --------------------------        ");
				
				//puede no ser necesario
				buf = new byte[300];
				packetDistObj = new DatagramPacket(buf, buf.length);
				packetAngObj = new DatagramPacket(buf, buf.length);
				packetAngDest = new DatagramPacket(buf, buf.length);	
				
				//SI SE HACEN GENERACIONES HACER UN WAIT PARA CUANDO SE PARE LA GENERACION
				//POR SI PETA EL 
				angDrone = 1.1f;
						
			}
			udpSocket.close();
		}
		// udpSocket.close();
		catch (IOException e) {
			e.printStackTrace();
		}
	}
	
	private static float stringToFloat (final String convertir){
		float out = 0.0f;
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

//comprobar como se envia angDrone
//comprobar los buffer en varias recepciones y condistintos tamaños para ver como se quedan

//				distObj = ByteBuffer.wrap(packetDistObj.getData()).getFloat();
