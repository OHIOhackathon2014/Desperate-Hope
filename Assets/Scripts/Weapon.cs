using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public float fireRate = 0;
	public float Damage = 10;
	public LayerMask whatToHit;
	public Transform MuzzleFlashPrefab;
	public Transform BulletTrailPrefab;

	public Rigidbody2D BulletPrefab;
	public float speed = 30;

	float timeToFire = 0;
	Transform firePoint;

	// Use this for initialization
	void Awake () {
		firePoint = transform.FindChild ("FirePoint");
		if (firePoint == null) {
			Debug.LogError ("No firePoint? WHAT?!");
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (fireRate == 0) {
			if (Input.GetButtonDown ("Fire1")) {
				Shoot();
			}
		}
		else {
			if (Input.GetButton ("Fire1") && Time.time > timeToFire) {
				timeToFire = Time.time + 1/fireRate;
				Shoot();
			}
		}
	}
	
	void Shoot () {
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition-firePointPosition, 100, whatToHit);
		Effect();
		Debug.DrawLine (firePointPosition, (mousePosition-firePointPosition)*100, Color.cyan);
		if (hit.collider != null) {
			Debug.DrawLine (firePointPosition, hit.point, Color.red);
			Debug.Log ("We hit " + hit.collider.name + " and did " + Damage + " damage.");
			hit.collider.gameObject.SendMessage("ApplyDamage", 10,SendMessageOptions.DontRequireReceiver );
		}
	}

	// Handle trail and flash
	void Effect(){
		Instantiate (BulletTrailPrefab, firePoint.position, firePoint.rotation);
		Transform clone = (Transform) Instantiate (MuzzleFlashPrefab, firePoint.position, firePoint.rotation);

		clone.parent = firePoint;

		// Randomize the size of the flash
		float size = Random.Range (0.3f, 0.6f);
		clone.localScale = new Vector3 (size, size, size);

		// Must call .gameObject to destroy.
		Destroy (clone.gameObject, 0.02f); // destroy after time. Could also do coroutine

		Rigidbody2D clone1 = (Rigidbody2D) Instantiate(BulletPrefab, transform.position, transform.rotation);
		clone1.velocity = transform.TransformDirection(new Vector3(speed,0,0));
		Destroy (clone1, 3);
	}
}
