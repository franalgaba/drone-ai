package udp;

import java.nio.ByteBuffer;

public class prueba3 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		
		byte[] buf = new byte[4];		
		System.out.println("Buf tiene: " + ByteBuffer.wrap(buf).getFloat());	
		
		float mEnviado = -88;
		buf = ByteBuffer.allocate(4).putFloat(mEnviado).array();
		System.out.println("Buf tiene: " + ByteBuffer.wrap(buf).getFloat());	
		
		mEnviado = 33;
		buf = ByteBuffer.allocate(4).putFloat(mEnviado).array();
		System.out.println("Buf tiene: " + ByteBuffer.wrap(buf).getFloat());
		
		mEnviado=1;		
		buf = ByteBuffer.allocate(4).putFloat(mEnviado).array();
		System.out.println("Buf tiene: " + ByteBuffer.wrap(buf).getFloat());	
		
		buf = new byte[4];
		System.out.println("Buf tiene: " + ByteBuffer.wrap(buf).getFloat());	
	}

}
