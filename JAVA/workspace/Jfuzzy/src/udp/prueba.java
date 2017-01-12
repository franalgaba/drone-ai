package udp;

public class prueba {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		
		byte[] buf = new byte[4];
		String mEnviado = "pepe";
		
		System.out.println("Buf tiene: " + buf.toString());	
		
		buf = mEnviado.getBytes();
		

		System.out.println("Buf tiene: " + buf.toString());	
		
		buf = new byte[1];
		
		System.out.println("Buf tiene: " + buf.toString());	
		
		buf = null;
		
	
		
		buf = new byte[4];

		System.out.println("Buf tiene: " + buf.toString());	
	}

}
