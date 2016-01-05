using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthDisplay : MonoBehaviour {

	private PlayerController playerController;
	private Text healthText;

	public float health = 100;
	// Use this for initialization
	void Start () {
		playerController = GameObject.Find ("Player").GetComponent<PlayerController> ();
		healthText = GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		float newHealth = playerController.getHealth ();
		healthText.text = newHealth.ToString ();
	}
}
