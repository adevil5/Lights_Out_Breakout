using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {

	public static int score = 0;
	public static GameManager instanceOf;
	public int startingScore = 0;
	public static int currentHearts;
	public int maxHearts = 3;
	public Texture heartTextureFull;
	public Texture heartTextureEmpty;
	public GameObject cheatWallPrefab;
	private GameObject cheatWall;
	private string[] levelNames;
	private int currentLevel;

	protected GameManager () {}

	// Use this for initialization
	void Start () {
//		DontDestroyOnLoad (GameManager.Instance);
		if(instanceOf == null){
			instanceOf = (GameManager)this;
		}
		DontDestroyOnLoad (gameObject);
		if(cheatWall == null){
			cheatWall = (GameObject)Instantiate (cheatWallPrefab);
		}
		DontDestroyOnLoad (cheatWall);
		currentHearts = maxHearts;
		score = startingScore;
		levelNames = new string[]{"Level1","Level2","Level3","Level4","Level5","Win","GameOver"};
		currentLevel = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.W)){
			//wall cheat
			if(cheatWall.collider.isTrigger){
				cheatWall.collider.isTrigger = false;
			} else {
				cheatWall.collider.isTrigger = true;
			}
		}

		if(Input.GetKeyDown(KeyCode.S)){
			LoadNextLevel();
		}
	}

	public void MissedBall(GameObject ball){
		currentHearts--;
		if (currentHearts < 1) {
			GameOver();
		} else {
			ResetBall(ball);
		}
	}

	void GameOver(){
		currentLevel = 0;
		Application.LoadLevel (levelNames[levelNames.Length-1]);

	}

	private void ResetBall(GameObject ball){
		Destroy(ball);
		GameObject paddleObj = GameObject.Find ("PlayerPaddle");
		PlayerPaddle playerScript = paddleObj.GetComponent<PlayerPaddle> ();
		playerScript.SpawnBall ();
	}

	public void LoadNextLevel(){
		Application.LoadLevel (levelNames[++currentLevel]);
	}

	void OnGUI(){
		GUI.Label (new Rect (5, 4, 100, 100), "Score: " + score);
		if (currentHearts >= 0) {
			if(currentHearts > 0){
				for (int i = 0; i < currentHearts; i++) {
					GUI.DrawTexture (new Rect (Screen.width - 85 + (i * 27), 5, 25, 25), heartTextureFull);
				}
			}
			if (currentHearts < maxHearts) {
				for (int i = currentHearts; i < maxHearts; i++) {
					GUI.DrawTexture (new Rect (Screen.width - 85 + (i * 27), 5, 25, 25), heartTextureEmpty);
				}
			}
		}
	}
}
