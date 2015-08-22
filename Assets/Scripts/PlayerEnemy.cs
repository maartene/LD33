using UnityEngine;
using System.Collections;

public class PlayerEnemy : MonoBehaviour {

	public float speed = 1.0f;
	public float meleeRange = 1.0f;
	public float meleeDamage = 1.0f;
	public float attackDelay = 0.5f;
	public float rotationSpeed = 360f;
	public Camera camera;

	private float _attackTimer = 0;
	private float _cameraX = 0;
	private int _brainCount = 0;

	private CharacterController _characterController;

	public int BrainCount {
		get { return _brainCount; }
	}

	// Use this for initialization
	void Start () {
		_characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

		_attackTimer -= Time.deltaTime;

		transform.Rotate(0, Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime, 0);

		_cameraX += -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
		_cameraX = Mathf.Clamp(_cameraX, -30f, 30f);
		camera.transform.localRotation = Quaternion.Euler(_cameraX, camera.transform.localRotation.y, camera.transform.localRotation.z);

		Vector3 direction = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
		_characterController.SimpleMove(direction * speed);

		if (Input.GetButtonDown("Fire1") && _attackTimer <= 0) {
			MeleeAttack();
			_attackTimer = attackDelay;
		}

	}

	private void MeleeAttack() {
		// determine whether you hit someone by raycast forward.
		RaycastHit hit;
		if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, meleeRange)) {
			Health health = hit.collider.GetComponent<Health>();
			
			if (health != null) {
				Debug.DrawLine(camera.transform.position, hit.point, Color.red, 0.5f);
				Debug.Log ("Gameobject: "+health.gameObject.name+" took damage.", health.gameObject);
				health.TakeDamage(meleeDamage);
			}
		}


	}

	public void AddBrain() {
		Debug.Log ("BRAINZZZ!");
		_brainCount++;
	}
}
