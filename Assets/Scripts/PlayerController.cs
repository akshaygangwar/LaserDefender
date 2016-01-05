 using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 15.0f;
	public GameObject projectile;
	public float projectileSpeed;

	public AudioClip fireSound;

	public float firingRate = 0.2f;
	public float health = 100f;

	private LivesDisplay livesDisplay;

	float xmin;
	float xmax;

	// Use this for initialization
	void Start () {
		livesDisplay = GameObject.Find ("Lives").GetComponent<LivesDisplay> ();
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));

		xmin = leftmost.x;
		xmax = rightmost.x;
	
	}

	void Fire() {
		Vector3 StartPosition = transform.position + new Vector3 (0, 1, 0);
		GameObject beam = Instantiate (projectile, StartPosition, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed * Time.deltaTime;

		} else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime;

		}

		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);

		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating("Fire", 0.00001f, firingRate); 
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke("Fire");
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		
		Projectile missile = col.gameObject.GetComponent<Projectile> ();
		
		if (missile != null) {
			
			health -= missile.GetDamage();
			missile.Hit();
			
			if(health <= 0) {
				int remainingLives = livesDisplay.getLives();
				if(remainingLives <= 0){
					Destroy(gameObject);
					LoadNextLevel();
				}
				else {
					remainingLives--;
					livesDisplay.setLives(remainingLives);
					health = 100f;
				}
			}
			
		}
		
	}

	public float getHealth() {
		return this.health;
	}

	public void LoadNextLevel() {
		//Brick.breakableCount = 0;
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
