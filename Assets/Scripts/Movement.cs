using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class Movement : MonoBehaviour
{
	public PoolManager poolManagerScript;

	public Joystick joystick;

	// Update Score
	public Text ScoreTxt;
	public int Score;

	//Time limit 
	public Text Timelimit;
	public float count = 60f;

	public float movSpeed = 5.0f;

	/// <summary>
	///            Floating Object Script
	/// </summary>
	/// 
	public float waterLevel=0.0f;
	public float floatThreshold = 2.0f;
	public float waterDensity = 0.125f;
	public float downForce = 4.0f;

	float forceFactor;
	Vector3 floatForce;

	public float frequency = 2.5f;
	public float magnitude = 0.1f;

	private Rigidbody rb;

	//public float gyroRotationSpeed = 70f;

	//UI
	public Image HealthImg;
	public float PlayerHealth = 100f;

	//GameOver UI GameObject
	public GameObject GameOver;


	//Main Health 100 - affected health for every hit is 20
	public void HealthAffected(float Val)//Val = 20
	{
		
		if (PlayerHealth > 0)
		{
			//PlayerHealth -= 20;
			//HealthImg.fillAmount -= 0.2f;

			PlayerHealth -= Val;

			HealthImg.fillAmount -= 0.2f;

			if (PlayerHealth == 0)
			{
				//GameOver
				GameOver.SetActive(true);
				Time.timeScale = 0;
			}
		}
		
		
	}


	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Meteor")
		{
			EnemyManager Emg = collision.gameObject.GetComponent<EnemyManager>();

			if (Emg.ObjectType == EnemyManager.Type.coin)
			{
				//Debug.Log("Collsion detected");
				Score++;
				ScoreTxt.text = "" + Score;
				Emg.PlaceBack();
			}

			if (Emg.ObjectType == EnemyManager.Type.meteor)
			{
				collision.gameObject.GetComponent<Collider>().enabled = false;
				//Debug.LogError("Hit");
				HealthAffected(20);
				Emg.PlaceBack();
			}
		}
	}
	private void Start()
	{

		count = 60;

		ScoreTxt.text = "";
		rb = GetComponent<Rigidbody>();
		Input.gyro.enabled = true;

		Debug.Log(transform.position);
	}

	Vector3 pos;
	void SineMove()
	{
		pos = transform.position;
		pos.y = 0.3f + ( Mathf.Sin(Time.time * frequency) * magnitude);
		//transform.position = transform.position.y * Mathf.Sin(Time.time * frequency) * magnitude;

		transform.position = pos;
	}


	float xValue;
	float zValue;
	float yAngle;
	private void Update()
	{
		TimeCount();
		SineMove();

		//Keyboard Control  
		//xValue = Input.GetAxis("Horizontal");
		//zValue = Input.GetAxis("Vertical");

		//Mobile Control Movement
		xValue = joystick.Horizontal;
		zValue = joystick.Vertical;
		yAngle = xValue * 45f;

		//Debug.Log("SSSSSS " + xValue + " ... " + zValue );

		if (xValue >= 0.2f)
		{

			transform.rotation = Quaternion.Euler(0, yAngle, 0);
			transform.Translate(new Vector3(xValue * movSpeed * Time.deltaTime, 0, zValue * movSpeed * Time.deltaTime));
		}
		else if(xValue <= -0.2f)
		{
			transform.rotation = Quaternion.Euler(0, yAngle, 0);
			transform.Translate(new Vector3(xValue * movSpeed * Time.deltaTime, 0, zValue * movSpeed * Time.deltaTime));
		}
	}

	public void TimeCount()
	{

		Debug.Log("..." + count);

		count -= Time.deltaTime;


		Timelimit.text = "00 : " + (int)count ;

		if (count < 0)
		{
			Debug.Log("Game Over");
			Debug.Log("High Score : " + Score);
			poolManagerScript.RestartScene();
		}
		
	}
	/*
	private void GyroInput()
	{
		Vector3 rotation = Input.gyro.rotationRate * gyroRotationSpeed;

		RotateView(new Vector3(-rotation.x , 0, -rotation.z));
	}

	private void RotateView(Vector3 rotation)
	{
		//Rotate Player
		transform.Rotate(rotation * Time.deltaTime);
		
		if (Input.gyro.enabled)
		{
			Vector3 localEuler = transform.localEulerAngles;
			transform.localRotation = Quaternion.Euler(localEuler.x, 0 , localEuler.z);
		}

		float playerPitchX = transform.eulerAngles.x;
		float playerPitchZ = transform.eulerAngles.z;

		transform.rotation = Quaternion.Euler(playerPitchX, 0, playerPitchZ);
	}*/

}
