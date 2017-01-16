package tcp;


import java.net.*;
import java.io.*;

public class servidorEchoTCP {
	
	public static void main(String[] args) {
				
		int puerto = 8800;
		
		OutputStreamWriter os; //flujo de salida de caracteres
		BufferedWriter CS; 
		
		InputStreamReader is;  //flujo de entrada de caracteres
		BufferedReader CE; 
		
		String mRecibido;
		
		try {
			ServerSocket sServidor = new ServerSocket(puerto); //se crea el socket
			System.out.println("Esperando establecer la comunicacion.");
			Socket serverClient = sServidor.accept();          //se bloquea a la espera una conexion entrante
			System.out.println("Se establece la comunicacion.");
			
			is = new InputStreamReader(serverClient.getInputStream());
			//Prepara la entrada para que lea en lineas de caracteres
			CE = new BufferedReader(is);
			
			os = new OutputStreamWriter(serverClient.getOutputStream());
			//Prepara la salida para que escriba en lineas de caracteres
			CS = new BufferedWriter (os);
			
			while (true) {				
				System.out.println("Esperando a leer.");
				mRecibido = CE.readLine(); //se lee lo que envia el cliente
				System.out.print("El ECHO recibido es: ");
				System.out.println(mRecibido);	

				// Salgo del While si el mensaje que envio al servidor es null o stop.
				if (mRecibido.equals(null) || mRecibido.equals("stop") ) { 
					System.out.println("Las comunicaciones han finalizado");
					break;}
				System.out.println("Enviando el mensaje al cliente");
				CS.write(mRecibido); //envio el mensaje al cliente
				CS.newLine();
				CS.flush();
				System.out.println("Mensaje enviado.");
			}	
			
			CE.close();
			CS.close();			
			
			serverClient.close();
			sServidor.close();
	
		} catch (IOException e) {
			System.out.println("Error en las comunicaciones");
			System.exit(0);
		} 
	}
}