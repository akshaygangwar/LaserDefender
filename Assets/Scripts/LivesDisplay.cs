using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LivesDisplay : MonoBehaviour {

	public int lives = 3;
	//private HealthDisplay healthDisplay;
	//private PlayerController playerController;

	private Text liveText;

	// Use this for initialization
	void Start () {
		liveText = GetComponent<Text> ();
		//healthDisplay = GameObject.Find ("Health").GetComponent<HealthDisplay> ();
		//playerController = GameObject.Find ("Player").GetComponent<PlayerController> ();
		liveText.text = lives.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		liveText.text = getLives ().ToString ();
	}

	public int getLives(){
		return lives;
	}

	public void setLives(int newLives) {
		lives = newLives;
	}
}
