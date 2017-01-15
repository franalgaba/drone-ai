package pruebas_fuzzy;

import java.util.HashMap;

import net.sourceforge.jFuzzyLogic.FIS;
import net.sourceforge.jFuzzyLogic.FunctionBlock;
import net.sourceforge.jFuzzyLogic.defuzzifier.DefuzzifierCenterOfGravity;
import net.sourceforge.jFuzzyLogic.membership.MembershipFunction;
import net.sourceforge.jFuzzyLogic.membership.MembershipFunctionPieceWiseLinear;
import net.sourceforge.jFuzzyLogic.membership.MembershipFunctionTriangular;
import net.sourceforge.jFuzzyLogic.membership.Value;
import net.sourceforge.jFuzzyLogic.plot.JDialogFis;
import net.sourceforge.jFuzzyLogic.rule.LinguisticTerm;
import net.sourceforge.jFuzzyLogic.rule.Rule;
import net.sourceforge.jFuzzyLogic.rule.RuleBlock;
import net.sourceforge.jFuzzyLogic.rule.RuleExpression;
import net.sourceforge.jFuzzyLogic.rule.RuleTerm;
import net.sourceforge.jFuzzyLogic.rule.Variable;
import net.sourceforge.jFuzzyLogic.ruleAccumulationMethod.RuleAccumulationMethodMax;
import net.sourceforge.jFuzzyLogic.ruleActivationMethod.RuleActivationMethodMin;
import net.sourceforge.jFuzzyLogic.ruleConnectionMethod.RuleConnectionMethodAndMin;
 
public class PruebaCBF9reglas { 

	public static void main(String[] args) throws Exception { 
		System.out.println("Inicio de la prueba del Controlador Borroso de Frenada (CBF)"); 
 
// ***************************************************************************************************************	
		FIS fis = new FIS(); 
 
		// FUNCTION_BLOCK CBF 
		FunctionBlock functionBlock = new FunctionBlock(fis); 
		fis.addFunctionBlock("CBF", functionBlock); 
 
		//  VAR_INPUT               
		//    distancia : REAL; 
		//    velocidad : REAL 
		//  END_VAR 
 
		Variable distancia = new Variable("distancia"); // Distancia entre vehículos
		Variable velocidad = new Variable("velocidad"); // Velocidad del vehículo
		functionBlock.setVariable(distancia.getName(), distancia); 
		functionBlock.setVariable(velocidad.getName(), velocidad); 
 
		//  VAR_OUTPUT 
		//    frenada : REAL; 
		//  END_VAR 
 
		Variable frenada = new Variable("frenada"); 
		functionBlock.setVariable(frenada.getName(), frenada); 
 
		//  FUZZIFY distancia 
		//    TERM corta := (0, 1) (5, 0) ; 
		//    TERM media := 3, 5, 7; 
		//    TERM larga := (5, 0) (10, 1); 
		//  END_FUZZIFY 
 
		// distancia = corta
		Value dcortaX[] = { new Value(0), new Value(5.5) }; 
		Value dcortaY[] = { new Value(1), new Value(0) }; 
		MembershipFunction dcorta = new MembershipFunctionPieceWiseLinear(dcortaX, dcortaY); 
 
		// distancia = media
		MembershipFunction dmedia = new MembershipFunctionTriangular(new Value(3), new Value(5), new Value(7)); 
 
		// distancia = larga
		Value dlargaX[] = { new Value(5.7), new Value(10) }; 
		Value dlargaY[] = { new Value(0), new Value(1) }; 
		MembershipFunction dlarga = new MembershipFunctionPieceWiseLinear(dlargaX, dlargaY); 

		// Términos lingüísticos 
		LinguisticTerm ltdCorta = new LinguisticTerm("corta", dcorta); 
		LinguisticTerm ltdMedia = new LinguisticTerm("media", dmedia); 
		LinguisticTerm ltdLarga = new LinguisticTerm("larga", dlarga); 
		distancia.add(ltdCorta); 
		distancia.add(ltdMedia); 
		distancia.add(ltdLarga); 
 
		//  FUZZIFY velocidad 
		//    TERM baja := (0, 1) (5, 0) ; 
		//    TERM media := 3, 5, 7; 
		//    TERM alta := (5, 0) (10, 1); 
		//  END_FUZZIFY 
 
		// velocidad = corta
		Value vbajaX[] = { new Value(0), new Value(5) }; 
		Value vbajaY[] = { new Value(1), new Value(0) }; 
		MembershipFunction vbaja = new MembershipFunctionPieceWiseLinear(vbajaX, vbajaY); 
 
		// velocidad = media
		MembershipFunction vmedia = new MembershipFunctionTriangular(new Value(3), new Value(5), new Value(7)); 
 
		// velocidad = larga
		Value valtaX[] = { new Value(5), new Value(10) }; 
		Value valtaY[] = { new Value(0), new Value(1) }; 
		MembershipFunction valta = new MembershipFunctionPieceWiseLinear(valtaX, valtaY); 
 
		// Términos lingüísticos para la variable velocidad
		LinguisticTerm ltvBaja = new LinguisticTerm("baja", vbaja); 
		LinguisticTerm ltvMedia = new LinguisticTerm("media", vmedia); 
		LinguisticTerm ltvAlta = new LinguisticTerm("alta", valta); 
		velocidad.add(ltvBaja); 
		velocidad.add(ltvMedia); 
		velocidad.add(ltvAlta); 
 
		//  DEFUZZIFY frenada 
		//    TERM baja := (0, 1) (5, 0) ; 
		//    TERM media := 3, 5, 7; 
		//    TERM alta := (5, 0) (10, 1); 
		//    METHOD : COG; 
		//    DEFAULT := 0; 
		//  END_DEFUZZIFY 
 
		// frenada = baja
		Value fbajaX[] = { new Value(0), new Value(5) }; 
		Value fbajaY[] = { new Value(1), new Value(0) }; 
		MembershipFunction fbaja = new MembershipFunctionPieceWiseLinear(fbajaX, fbajaY); 
 
		// frenada = media
		MembershipFunction fmedia = new MembershipFunctionTriangular(new Value(3), new Value(5), new Value(7)); 
 
		// frenada = alta
		Value faltaX[] = { new Value(5), new Value(10) }; 
		Value faltaY[] = { new Value(0), new Value(1) }; 
		MembershipFunction falta = new MembershipFunctionPieceWiseLinear(faltaX, faltaY); 
 
		// Términos lingüísticos para la variable frenada
		LinguisticTerm ltfBaja = new LinguisticTerm("baja", fbaja); 
		LinguisticTerm ltfMedia = new LinguisticTerm("media", fmedia); 
		LinguisticTerm ltfAlta = new LinguisticTerm("alta", falta); 
 
		frenada.add(ltfBaja); 
		frenada.add(ltfMedia); 
		frenada.add(ltfAlta); 
 
		frenada.setDefuzzifier(new DefuzzifierCenterOfGravity(frenada)); 
 
		RuleBlock ruleBlock = new RuleBlock(functionBlock); 
		
		//  RULEBLOCK No1 
		ruleBlock.setName("No1"); 

		//    ACCU : MAX; 
		ruleBlock.setRuleAccumulationMethod(new RuleAccumulationMethodMax()); 

		//    ACT : MIN; 
		ruleBlock.setRuleActivationMethod(new RuleActivationMethodMin()); 
 
		// RULE 1 : IF distancia IS corta AND velocidad IS baja THEN frenada IS media; 
		// RULE 2 : IF distancia IS corta AND velocidad IS media THEN frenada IS media; 
		// RULE 3 : IF distancia IS corta AND velocidad IS alta THEN frenada IS alta; 

		// RULE 4 : IF distancia IS media AND velocidad IS baja THEN frenada IS baja; 
		// RULE 5 : IF distancia IS media AND velocidad IS media THEN frenada IS media; 
		// RULE 6 : IF distancia IS media AND velocidad IS alta THEN frenada IS alta; 

		// RULE 7 : IF distancia IS larga AND velocidad IS baja THEN frenada IS baja; 
		// RULE 8 : IF distancia IS larga AND velocidad IS media THEN frenada IS baja; 
		// RULE 9 : IF distancia IS larga AND velocidad IS alta THEN frenada IS media; 

		Rule rule1 = new Rule("Rule1", ruleBlock); 
		RuleTerm term1rule1 = new RuleTerm(distancia, "corta", false); 
		RuleTerm term2rule1 = new RuleTerm(velocidad, "baja", false); 
		RuleExpression antecedenteAnd1 = new RuleExpression(term1rule1, term2rule1, RuleConnectionMethodAndMin.get()); 
		rule1.setAntecedents(antecedenteAnd1); 
		rule1.addConsequent(frenada, "media", false); 
		rule1.setWeight(0.3);
		ruleBlock.add(rule1); 
 
		Rule rule2 = new Rule("Rule2", ruleBlock); 
		RuleTerm term1rule2 = new RuleTerm(distancia, "corta", false); 
		RuleTerm term2rule2 = new RuleTerm(velocidad, "media", false); 
		RuleExpression antecedenteAnd2 = new RuleExpression(term1rule2, term2rule2, RuleConnectionMethodAndMin.get()); 
		rule2.setAntecedents(antecedenteAnd2); 
		rule2.addConsequent(frenada, "media", false); 
		ruleBlock.add(rule2); 

		Rule rule3 = new Rule("Rule3", ruleBlock); 
		RuleTerm term1rule3 = new RuleTerm(distancia, "corta", false); 
		RuleTerm term2rule3 = new RuleTerm(velocidad, "alta", false); 
		RuleExpression antecedenteAnd3 = new RuleExpression(term1rule3, term2rule3, RuleConnectionMethodAndMin.get()); 
		rule3.setAntecedents(antecedenteAnd3); 
		rule3.addConsequent(frenada, "alta", false); 
		ruleBlock.add(rule3); 
		

		Rule rule4 = new Rule("Rule4", ruleBlock); 
		RuleTerm term1rule4 = new RuleTerm(distancia, "media", false); 
		RuleTerm term2rule4 = new RuleTerm(velocidad, "baja", false); 
		RuleExpression antecedenteAnd4 = new RuleExpression(term1rule4, term2rule4, RuleConnectionMethodAndMin.get()); 
		rule4.setAntecedents(antecedenteAnd4); 
		rule4.addConsequent(frenada, "baja", false); 
		ruleBlock.add(rule4); 
 
		Rule rule5 = new Rule("Rule5", ruleBlock); 
		RuleTerm term1rule5 = new RuleTerm(distancia, "media", false); 
		RuleTerm term2rule5 = new RuleTerm(velocidad, "media", false); 
		RuleExpression antecedenteAnd5 = new RuleExpression(term1rule5, term2rule5, RuleConnectionMethodAndMin.get()); 
		rule5.setAntecedents(antecedenteAnd5); 
		rule5.addConsequent(frenada, "media", false); 
		ruleBlock.add(rule5); 

		Rule rule6 = new Rule("Rule6", ruleBlock); 
		RuleTerm term1rule6 = new RuleTerm(distancia, "media", false); 
		RuleTerm term2rule6 = new RuleTerm(velocidad, "alta", false); 
		RuleExpression antecedenteAnd6 = new RuleExpression(term1rule6, term2rule6, RuleConnectionMethodAndMin.get()); 
		rule6.setAntecedents(antecedenteAnd6); 
		rule6.addConsequent(frenada, "alta", false); 
		ruleBlock.add(rule6); 

		
		Rule rule7 = new Rule("Rule7", ruleBlock); 
		RuleTerm term1rule7 = new RuleTerm(distancia, "larga", false); 
		RuleTerm term2rule7 = new RuleTerm(velocidad, "baja", false); 
		RuleExpression antecedenteAnd7 = new RuleExpression(term1rule7, term2rule7, RuleConnectionMethodAndMin.get()); 
		rule7.setAntecedents(antecedenteAnd7); 
		rule7.addConsequent(frenada, "baja", false); 
		ruleBlock.add(rule7); 
 
		Rule rule8 = new Rule("Rule8", ruleBlock); 
		RuleTerm term1rule8 = new RuleTerm(distancia, "larga", false); 
		RuleTerm term2rule8 = new RuleTerm(velocidad, "media", false); 
		RuleExpression antecedenteAnd8 = new RuleExpression(term1rule8, term2rule8, RuleConnectionMethodAndMin.get()); 
		rule8.setAntecedents(antecedenteAnd8); 
		rule8.addConsequent(frenada, "baja", false); 
		ruleBlock.add(rule8); 

		Rule rule9 = new Rule("Rule9", ruleBlock); 
		RuleTerm term1rule9 = new RuleTerm(distancia, "larga", false); 
		RuleTerm term2rule9 = new RuleTerm(velocidad, "alta", false); 
		RuleExpression antecedenteAnd9 = new RuleExpression(term1rule9, term2rule9, RuleConnectionMethodAndMin.get()); 
		rule9.setAntecedents(antecedenteAnd9); 
		rule9.addConsequent(frenada, "media", false); 
		ruleBlock.add(rule9); 

		/*rule1.setWeight(0.5);
		rule2.setWeight(0.8);
		rule3.setWeight(1.0);

		rule4.setWeight(0.6);
		rule5.setWeight(0.8);
		rule6.setWeight(1.0);

		rule7.setWeight(0.4);
		rule8.setWeight(0.6);
		rule9.setWeight(0.8);*/

		// END_RULEBLOCK 
		// 
		// END_FUNCTION_BLOCK 
		HashMap<String, RuleBlock> ruleBlocksMap = new HashMap<String, RuleBlock>(); 
		ruleBlocksMap.put(ruleBlock.getName(), ruleBlock); 
		functionBlock.setRuleBlocks(ruleBlocksMap); 
  
//*******************************************************************************************************************		
		
		// Asignamos valores a las variables de entrada 
		fis.getVariable("distancia").setValue(6.5); 
		fis.getVariable("velocidad").setValue(4.5); 

		// Evaluamos el sistema para estos valores de entrada
		fis.evaluate(); 
 
		// Mostramos el resultado en la consola
		System.out.println("Distancia: " + fis.getVariable("distancia").getValue() + " y " +
							"Velocidad: " + fis.getVariable("velocidad").getValue() + 
							" ==> Frenada: " + fis.getVariable("frenada").getValue());

		System.out.println(rule1.toString());
		System.out.println(rule2.toString());
		System.out.println(rule3.toString());
		System.out.println(rule4.toString());
		System.out.println(rule5.toString());
		System.out.println(rule6.toString());
		System.out.println(rule7.toString());
		System.out.println(rule8.toString());
		System.out.println(rule9.toString());

		// Mostramos el FIS en formato FCL 
		System.out.println(fis); 
  
		// Creamos una ventana de diálogo mostrando el FIS
		JDialogFis jdf = new JDialogFis(fis, 800, 600); 
		jdf.repaint(); 
   
  		System.out.println("Final de la prueba del Controlador Borroso de Frenado"); 
  	} 
 
 public PruebaCBF9reglas() { 
	} 
}
