package udp;

import java.io.IOException;
import java.net.*;


public class servidorEchoUDP {
	
	public static void main(String[] args) {
		int serverPort = 9900;
		// Create a buffer where to put a received datagram
		byte[] buf ;
		String mRecibido;
		DatagramPacket packet0;
		DatagramPacket packet1; 
		DatagramSocket udpSocket;
		try {
			// Create a udp socket for sending and receiving datagrams in serverPort
			udpSocket = new DatagramSocket(serverPort);
			
			System.out.println("Server: waiting for new datagrams...");
			buf = new byte[300];
			packet0 = new DatagramPacket(buf, buf.length);
			while (true) {
								
				System.out.println("Esperando a recibir.");
				// Receive the datagram from client
				udpSocket.receive(packet0);
				// Get the address of client from packet
				InetAddress clientAddress = packet0.getAddress();
				int clientPort = packet0.getPort();		
				
				mRecibido =  new String(packet0.getData());
				System.out.println("El ECHO recibido es: " + mRecibido);
				
				// Salgo del While si el mensaje que envio al servidor es null o stop.
				if (mRecibido.equals(null) || mRecibido.equals("stop") ) { 
					System.out.println("Las comunicaciones han finalizado");
					break;}
				
				mRecibido = mRecibido.replace('a', 'A');
				System.out.println("Enviando el mensaje al cliente: " + mRecibido);
				buf = mRecibido.getBytes();
				packet1 = new DatagramPacket(buf, buf.length, clientAddress,clientPort);
				// Send the datagram packet to client
				udpSocket.send(packet1);
				System.out.println("Mensaje enviado.");
				System.out.println("       --------------------------        ");
			}
			
			udpSocket.close();
		}
		// udpSocket.close();
		catch (IOException e) {
			e.printStackTrace();
		}
	}

}


