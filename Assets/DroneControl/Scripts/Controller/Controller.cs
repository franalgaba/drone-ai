using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public float Throttle = 0.0f;
	public float Yaw = 0.0f;
	public float Pitch = 0.0f;
	public float Roll = 0.0f;

    public enum ThrottleMode { None, LockHeight};

	//subir y bajar con W y S
	[Header("Throttle command")]
	public string ThrottleCommand = "Throttle";
	public bool InvertThrottle = true; 

	//rotar con A y D
    [Header("Yaw Command")] 
	public string YawCommand = "Yaw";
	public bool InvertYaw = false;

	// inclinar hacia delante y hacia detras con la I y la K
	[Header("Pitch Command")]
	public string PitchCommand = "Pitch";
	public bool InvertPitch = true;

	//inclinar iz y dcha con J y L
	[Header("Roll Command")]
	public string RollCommand = "Roll";
	public bool InvertRoll = true;

	void Update() {
        Throttle = Input.GetAxisRaw(ThrottleCommand) * (InvertThrottle ? -1 : 1);

		//Anulamos la rotacion manual, el controlador borroso se encarga
		Yaw = /*Input.GetAxisRaw(YawCommand)*/0 * (InvertYaw ? -1 : 1);

		//hacemos que el drone siempre vaya hacia delante
		Pitch = /*Input.GetAxisRaw(PitchCommand)*/1 * (InvertPitch ? -1 : 1);

        Roll = Input.GetAxisRaw(RollCommand) * (InvertRoll ? -1 : 1);
	}

}
