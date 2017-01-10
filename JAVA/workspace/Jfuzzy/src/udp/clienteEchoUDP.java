package udp;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.*;

public class clienteEchoUDP {

	public static void main(String[] args) {
		int clientPort = 7779;
		int serverPort = 9900;
		
		String mEnviado;

		BufferedReader in = new BufferedReader(new InputStreamReader(System.in));
		
		// Create a buffer where to put a received datagram
		byte[] buf;

		try {
			// Create a udp socket for sending and receiving datagrams in
			// serverPort
			// Si solo fuera para enviar no haria falta poner en el new
			// DatagramSocket el clientPort
			DatagramSocket udpSocket;
			udpSocket = new DatagramSocket(clientPort);
			DatagramPacket packet;
			
			while (true) {
				System.out.print("Escribe una peticion de ECHO personalizada: ");	
				mEnviado = in.readLine();// se lee del teclado y se guarda en mEnviado
				buf = null;
				buf = new byte[100];
				buf = mEnviado.getBytes();

				// creamos el paquete a enviar al servidor
				packet = new DatagramPacket(buf, buf.length, InetAddress.getByName("localhost"), serverPort);
				udpSocket.send(packet);
				
				// Salgo del While si el mensaje que envio al servidor es null o stop.
				if (mEnviado.equals(null) || mEnviado.equals("stop")) {
					System.out.println("Las comunicaciones han finalizado");
					break;}			

				packet = new DatagramPacket(buf, buf.length);
				udpSocket.receive(packet);

				// Miramos lo que nos llega para ver que la longitud era la correcta
				System.out.println("El ECHO recibido es: "  + new String(packet.getData()));
			}

			udpSocket.close();

		} catch (IOException e) {
			e.printStackTrace();
		}
	}

}
