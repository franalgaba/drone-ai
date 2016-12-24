using UnityEngine;
using System.Collections;

public class CoordinatesSensors : MonoBehaviour 
{
	public		GameObject		sensorForward;
	public		GameObject		sensorBackward;
	public		GameObject		sensorRight;
	public		GameObject		sensorLeft;
	public		GameObject		sensorUp;
	public		GameObject		sensorDown;

	void Update()
	{
		Vector3 playerPosition   = gameObject.transform.position;

		// RAY FORWARD
		Vector3 sensorPosition_F = sensorForward.transform.position;
		Vector3 direccionF = sensorPosition_F - playerPosition;		
		Ray rayForward = new Ray(playerPosition, sensorPosition_F);
		Debug.DrawRay(rayForward.origin, direccionF * 0.5f, Color.blue);

		// RAY BACKWARD
		Vector3 sensorPosition_B = sensorBackward.transform.position;
		Vector3 direccionB = sensorPosition_B - playerPosition;		
		Ray rayBackward = new Ray(playerPosition, sensorPosition_B);
		Debug.DrawRay(rayBackward.origin, direccionB * 1.0f, Color.blue);

		// RAY RIGHT
		Vector3 sensorPosition_R = sensorRight.transform.position;
		Vector3 direccionR = sensorPosition_R - playerPosition;		
		Ray rayRight = new Ray(playerPosition, sensorPosition_R);
		Debug.DrawRay(rayForward.origin, direccionR * 1.0f, Color.blue);

		// RAY LEFT
		Vector3 sensorPosition_L = sensorLeft.transform.position;
		Vector3 direccionL = sensorPosition_L - playerPosition;		
		Ray rayLeft = new Ray(playerPosition, sensorPosition_L);
		Debug.DrawRay(rayForward.origin, direccionL * 1.0f, Color.blue);

		// RAY UP
		Vector3 sensorPosition_U = sensorUp.transform.position;
		Vector3 direccionU = sensorPosition_U - playerPosition;		
		Ray rayUp = new Ray(playerPosition, sensorPosition_U);
		Debug.DrawRay(rayForward.origin, direccionU * 1.0f, Color.blue);

		// RAY DOWN
		Vector3 sensorPosition_D = sensorDown.transform.position;
		Vector3 direccionD = sensorPosition_D - playerPosition;		
		Ray rayDown = new Ray(playerPosition, sensorPosition_D);
		Debug.DrawRay(rayForward.origin, direccionD * 1.0f, Color.blue);

		// Sensor de choque Forward
		RaycastHit[] hitForward;
		hitForward = Physics.RaycastAll (rayForward);
		if (hitForward.Length > 0) 
		{
			foreach(RaycastHit f in hitForward)
			{
				GameObject obstaculoF = f.transform.gameObject;
				Vector3 ObstaculoF = obstaculoF.transform.position;
				Vector3 direObstaculoF = ObstaculoF - playerPosition;
				float distObstaculoF = Vector3.Distance (playerPosition, ObstaculoF);
				Debug.Log("Obstaculo en frente " + f.transform.gameObject.name + " a distancia: " + distObstaculoF);
				Debug.DrawRay(rayForward.origin, direObstaculoF * 1.0f, Color.red);
				break; 
			}
		}

		// Sensor de choque Backward
		RaycastHit[] hitBackward;
		hitBackward = Physics.RaycastAll (rayBackward);
		if (hitBackward.Length > 0) 
		{
			foreach(RaycastHit b in hitBackward)
			{
				GameObject obstaculoB = b.transform.gameObject;
				Vector3 ObstaculoB = obstaculoB.transform.position;
				Vector3 direObstaculoB = ObstaculoB - playerPosition;
				float distObstaculoB = Vector3.Distance (playerPosition, ObstaculoB);
				Debug.Log("Obstaculo detras " + b.transform.gameObject.name + " a distancia: " + distObstaculoB);
				Debug.DrawRay(rayBackward.origin, direObstaculoB * 1.0f, Color.red);
				break; 
			}
		}

		// Sensor de choque Right
		RaycastHit[] hitRight;
		hitRight = Physics.RaycastAll (rayRight);
		if (hitRight.Length > 0) 
		{
			foreach(RaycastHit r in hitRight)
			{
				GameObject obstaculoR = r.transform.gameObject;
				Vector3 ObstaculoR = obstaculoR.transform.position;
				Vector3 direObstaculoR = ObstaculoR - playerPosition;
				float distObstaculoR = Vector3.Distance (playerPosition, ObstaculoR);
				Debug.Log("Obstaculo a la derecha " + r.transform.gameObject.name + " a distancia: " + distObstaculoR);
				Debug.DrawRay(rayRight.origin, direObstaculoR * 1.0f, Color.red);
				break; 
			}
		}

		// Sensor de choque Left
		RaycastHit[] hitLeft;
		hitLeft = Physics.RaycastAll (rayLeft);
		if (hitLeft.Length > 0) 
		{
			foreach(RaycastHit l in hitLeft)
			{
				GameObject obstaculoL = l.transform.gameObject;
				Vector3 ObstaculoL = obstaculoL.transform.position;
				Vector3 direObstaculoL = ObstaculoL - playerPosition;
				float distObstaculoL = Vector3.Distance (playerPosition, ObstaculoL);
				Debug.Log("Obstaculo a la izquierda " + l.transform.gameObject.name + " a distancia: " + distObstaculoL);
				Debug.DrawRay(rayLeft.origin, direObstaculoL * 1.0f, Color.red);
				break; 
			}
		}

		// Sensor de choque Up
		RaycastHit[] hitUp;
		hitUp = Physics.RaycastAll (rayUp);
		if (hitUp.Length > 0) 
		{
			foreach(RaycastHit u in hitUp)
			{
				GameObject obstaculoU = u.transform.gameObject;
				Vector3 ObstaculoU = obstaculoU.transform.position;
				Vector3 direObstaculoU = ObstaculoU - playerPosition;
				float distObstaculoU = Vector3.Distance (playerPosition, ObstaculoU);
				Debug.Log("Obstaculo arriba " + u.transform.gameObject.name + " a distancia: " + distObstaculoU);
				Debug.DrawRay(rayUp.origin, direObstaculoU * 1.0f, Color.red);
				break; 
			}
		}

		// Sensor de choque Down
		RaycastHit[] hitDown;
		hitDown = Physics.RaycastAll (rayDown);
		if (hitDown.Length > 0) 
		{
			foreach(RaycastHit d in hitDown)
			{
				GameObject obstaculoD = d.transform.gameObject;
				Vector3 ObstaculoD = obstaculoD.transform.position;
				Vector3 direObstaculoD = ObstaculoD - playerPosition;
				float distObstaculoD = Vector3.Distance (playerPosition, ObstaculoD);
				Debug.Log("Obstaculo debajo " + d.transform.gameObject.name + " a distancia: " + distObstaculoD);
				Debug.DrawRay(rayDown.origin, direObstaculoD * 1.0f, Color.red);
				break; 
			}
		}
	}


}
