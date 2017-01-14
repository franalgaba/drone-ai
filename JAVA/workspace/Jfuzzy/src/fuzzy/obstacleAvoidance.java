package fuzzy;

import net.sourceforge.jFuzzyLogic.FIS;

public class obstacleAvoidance {
	
	private FIS fis;
	private String fileName= "obstacleAvoidance.fcl";
	private double valor;
	private float anguloMotor;
	

	public obstacleAvoidance() {
		super();
	     // Load from 'FCL' file
        fis = FIS.load(fileName,true);

       // Error while loading?
       if( fis == null ) { 
           System.err.println("Can't load file: '" + fileName + "'");
       } else {
    	   System.out.println("Se ha leido correctamente el fichero: " + fileName);
       }
	}
	
    public float evaluar(float dist, float angObj, float angDest) {   

        // Set inputs
        fis.setVariable("obstacleDist", dist);
        fis.setVariable("obstacleAng", angObj);
        fis.setVariable("targetAng", angDest);        

        // Evaluate
        fis.evaluate();
     	
		valor=fis.getVariable("droneAngle").getValue(); 
		anguloMotor= (float) valor;
		return anguloMotor;     	
    }

}