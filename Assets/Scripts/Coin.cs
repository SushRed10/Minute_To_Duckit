using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	Rigidbody rb;

	public float speed = 300f;
	public float X;
    void Start()
    {
		rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		X += speed * Time.deltaTime;
		transform.rotation = Quaternion.Euler(-90f, X, 0);   
    }
}
