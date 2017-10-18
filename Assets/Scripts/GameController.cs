using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Security;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public int hazardCount;
	public Vector3 spawnValue;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	private int score;
	private bool isGameOver;
	private bool isRestart;

	void Start () {
		isGameOver = false;
		isRestart = false;
		score = 0;
		restartText.text = "";
		gameOverText.text = "";

		UpdateScore();
		StartCoroutine(SpawnWaves());
	}

	void Update(){
		if (isRestart) {
			Debug.Log("isRestart = true");
			if (Input.GetKeyDown(KeyCode.R)) {
				Debug.Log("You press R!");
				isRestart = false;
				Application.LoadLevel("Main"); //LoadScene("Main"); //.LoadLevel(Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves(){
		yield return new WaitForSeconds(startWait);
		while(true) {
			yield return new WaitForSeconds(waveWait);
			for (int i = 0; i < hazardCount; i++) {
				SpawnHazard();
				yield return new WaitForSeconds(spawnWait);
			}

			if (isGameOver) {
				restartText.text = "Press \"R\" for Restart";	
				isRestart = true;
				break;
			}
		}
	}

	void SpawnHazard(){
		GameObject hazard = hazards[Random.Range(0, hazards.Length)];
		Vector3 spawnPosition = new Vector3(
			Random.Range(-spawnValue.x, spawnValue.x), 
			spawnValue.y, 
			spawnValue.z
		);
		Quaternion spawnRotation = Quaternion.identity;
		Instantiate(hazard, spawnPosition, spawnRotation);
	}

	void UpdateScore(){
		scoreText.text = "Score: " + score;
	}

	public void GameOver(){
		gameOverText.text = "GAME OVER";
		isGameOver = true;
	}

	public void AddScore(int newScoreValue){
		score += newScoreValue;
		UpdateScore();
	}
}