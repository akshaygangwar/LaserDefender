using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public float health = 150f;
	public GameObject enemyProjectile;
	public float enemyLaserSpeed = 5f;
	public AudioClip fireSound;
	public AudioClip deathSound;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 150;
	private ScoreKeeper scoreKeeper;

	// Use this for initialization
	void Start () {
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}
	
	// Update is called once per frame
	void Update () {

		float probability = Time.deltaTime * shotsPerSecond;
		if (Random.value < probability) {
			EnemyFire ();
		}
	
	}

	void EnemyFire() {
		Vector3 StartPosition = transform.position + new Vector3 (0, -1, 0);
		GameObject enemyLaser = Instantiate (enemyProjectile, StartPosition, Quaternion.identity) as GameObject;
		enemyLaser.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, -enemyLaserSpeed, 0);
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
	}

	void OnTriggerEnter2D(Collider2D col) {

		Projectile missile = col.gameObject.GetComponent<Projectile> ();

		if (missile != null) {
		
			health -= missile.GetDamage();
			missile.Hit();

			if(health <= 0) {
				AudioSource.PlayClipAtPoint(deathSound, transform.position);
				Destroy(gameObject);
				scoreKeeper.Score(scoreValue);
			}
		
		}

	}

}
