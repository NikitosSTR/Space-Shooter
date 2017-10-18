using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;

	private GameController gameController;

	void Start(){
		GameObject gameControllerObject = GameObject.FindWithTag("GameController"); //FindWithTag("GameController");
		if (gameControllerObject) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (!gameController) {
			Debug.Log("Can't find 'GameController' script!");
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag("Boundary") || other.CompareTag("Enemy")){
			return;
		}

		if (other.tag == "Player") {
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		}
		if (explosion) {
			Instantiate(explosion, transform.position, transform.rotation);
		}
		gameController.AddScore(scoreValue);

		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
