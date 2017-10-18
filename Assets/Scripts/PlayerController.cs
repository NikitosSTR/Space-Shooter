using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Boundary{
	
	public float xMin, xMax, zMin, zMax;

}

public class PlayerController : MonoBehaviour {

	public float speed, tilt, fireRate;
	public Boundary boundary;
	public GameObject shot;
	public Transform shotSpawn;

	private Rigidbody rb;
	private float nextFire;
	private AudioSource audioSource;

	void Start(){
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	void Update(){
		if (Input.GetButton("Fire1") && (Time.time > nextFire)){
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation); // as GameObject
			audioSource.Play();
		}
	}

	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		Vector3 pos = new Vector3(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);
		rb.position = pos;

		rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * (-tilt));
	}
}
