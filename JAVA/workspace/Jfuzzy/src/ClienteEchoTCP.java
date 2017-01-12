

import java.net.*;
import java.io.*;

public class ClienteEchoTCP {
	
	
	public static void main(String[] args) {
		String host = "localhost";
		int puerto = 8800;
		
		OutputStreamWriter os; //flujo de salida de caracteres
		BufferedWriter CS;
		
		InputStreamReader is;   //flujo de entrada de caracteres
		BufferedReader CE; 
		
		//convierte el fujo de bytes provieniente del teclado en un fujo de lineas de caracteres 
		BufferedReader in = new BufferedReader(new InputStreamReader(System.in));
		String mEnviado,mRecibido;
		
		try {
			Socket sCliente = new Socket(host, puerto);	//se crea el socket del cliente
			System.out.println("Se establece la comunicacion.");
		
			os = new OutputStreamWriter(sCliente.getOutputStream());
			//Prepara la salida para que escriba en lineas de caracteres
			CS = new BufferedWriter(os);
			
			is = new InputStreamReader( sCliente.getInputStream());
			//Prepara la entrada para que lea en lineas de caracteres
			CE = new BufferedReader(is);
			
			while (true) {
				System.out.print("Escribe una peticion de ECHO personalizada: ");	
				mEnviado = in.readLine();// se lee del teclado y se guarda en mEnviado
				CS.write(mEnviado); //envio el mensaje al servidor
				CS.newLine();
				CS.flush();
								
				// Salgo del While si el mensaje que envio al servidor es null o stop.
				if (mEnviado.equals(null) || mEnviado.equals("stop")) {
					System.out.println("Las comunicaciones han finalizado");
					break;}
					
				mRecibido = CE.readLine(); //recibo la replica del mensaje del servidor
				System.out.print("El ECHO recibido es: ");
				System.out.println(mRecibido);				
				}	
			//cerrar los canales tambien es necesario?? SI, aunque sClient.close ya lo cierra por defecto.
			CE.close();
			CS.close();
					
			//cierro el socket
			sCliente.close(); 
		} catch (UnknownHostException e) {			 
			System.out.println("Referencia a host no resuelta");
		} catch (IOException e) { 
			System.out.println("Error enlas comunicaciones");
		}
	}
}