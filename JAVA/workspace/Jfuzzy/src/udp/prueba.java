package udp;

public class prueba {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		float num = 9876;
		
		byte[] buf = new byte[4];		
		System.out.println("Buf tiene: " + new String(buf));	
		
		String mEnviado = Float.toString(num);
		buf = mEnviado.getBytes();
		System.out.println("Buf tiene: " + new String(buf));	
		
		buf = new byte[1];
		System.out.println("Buf tiene: " + new String(buf));	
		num =1;
		mEnviado= Float.toString(num);		
		buf = mEnviado.getBytes();
		System.out.println("Buf tiene: " + new String(buf));
		
		buf = new byte[4];
		System.out.println("Buf tiene: " + new String(buf));
		
		float f = Float.parseFloat("-235.4545");
		System.out.println("Float " + f);
		
	}

}
