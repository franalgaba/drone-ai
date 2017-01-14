package pruebas_fuzzy;

import net.sourceforge.jFuzzyLogic.FIS;
import net.sourceforge.jFuzzyLogic.plot.JDialogFis;
import net.sourceforge.jFuzzyLogic.plot.JFuzzyChart;
import net.sourceforge.jFuzzyLogic.rule.Variable;

import net.sourceforge.jFuzzyLogic.rule.Rule;

/**
 * Test parsing an FCL file
 * @author pcingola@users.sourceforge.net
 */
public class obstacleAvoidanceComentado {
	
	private FIS fis;
	private String fileName= "obstacleAvoidance.fcl";
	private double valor;
	private float anguloMotor;
	

	public obstacleAvoidanceComentado() {
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
        
		// Mostramos el resultado en la consola
		System.out.println("obstacleDist: " + fis.getVariable("obstacleDist").getValue() + 
						   ", obstacleAng: " + fis.getVariable("obstacleAng").getValue() +
						   " y targetAng: " + fis.getVariable("targetAng").getValue() + 
							" ==> droneAngle: " + fis.getVariable("droneAngle").getValue() + "\n\n\n");

        // Show output variable's chart
        Variable droneAngle = fis.getVariable("droneAngle");
        JFuzzyChart.get().chart(droneAngle, droneAngle.getDefuzzifier(), true);

   
 		// Creamos una ventana de diálogo mostrando el FIS
     	JDialogFis jdf = new JDialogFis(fis, 800, 600); 
     	jdf.repaint(); 
     	
     	 // Show each rule (and degree of support)
     	for( Rule r : fis.getFunctionBlock("obstacleAvoidance").getFuzzyRuleBlock("No1").getRules() )
    	   System.out.println(r);
     	
     	
     	
		valor=fis.getVariable("droneAngle").getValue(); 
		anguloMotor= (float) valor;
		return anguloMotor;
     	
    }
    
    // Para mostrar el FIS en formato FCL 
 	// System.out.println(fis); 
}