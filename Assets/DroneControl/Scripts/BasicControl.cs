using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BasicControl : MonoBehaviour {

	public GameObject funcion;

    [Header("Limites")]
    public float speedLimit;

    [Header("Control")]
	public Controller Controller;
	public float ThrottleIncrease;
	
	[Header("Motors")]
	public Motor[] Motors;
	public float ThrottleValue;

    [Header("Internal")]
    public ComputerModule Computer;

	private float anguloGiro = 0.0f;

	public float turningRate = 30f; 
	// Rotation we should blend towards.
	private Quaternion _targetRotation;
	// Call this when you want to turn the object smoothly.
	public void SetBlendedEulerAngles(Vector3 angles)
	{
		_targetRotation = Quaternion.Euler(angles);
	}

	//actualizamos los motores del drone basandonos en su movimiento
	void FixedUpdate() {
        Computer.UpdateComputer(Controller.Pitch, Controller.Roll, Controller.Throttle * ThrottleIncrease);
        ThrottleValue = Computer.HeightCorrection;
        ComputeMotors();
        if (Computer != null)
            Computer.UpdateGyro();
        ComputeMotorSpeeds();

		anguloGiro = funcion.GetComponent<Sensing> ().getAnguloGiro();
		_targetRotation = Quaternion.Euler(0.0f, anguloGiro, 0.0f);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, turningRate * Time.deltaTime);
    }

	//actualizamos las fuerzas aplicadas a cada motor
    private void ComputeMotors()
    {
        float yaw = 0.0f;
        Rigidbody rb = GetComponent<Rigidbody>();
        int i = 0;
        foreach (Motor motor in Motors)
        {
            motor.UpdateForceValues();
            yaw += motor.SideForce;
            i++;
            Transform t = motor.GetComponent<Transform>();
			rb.AddForceAtPosition(transform.up * motor.UpForce, t.position, ForceMode.Impulse);
        }
        rb.AddTorque(Vector3.up * yaw, ForceMode.Force);
    }

	//modificamos la velocidad de las helices del drone
    private void ComputeMotorSpeeds()
    {
        foreach (Motor motor in Motors)
        {
            if (Computer.Gyro.Altitude < 0.1)
                motor.UpdatePropeller(0.0f);
            else
                motor.UpdatePropeller(1200.0f); 
        }
    }
}