using UnityEngine;
using System.Collections;

public class PlayerEnemy : MonoBehaviour {

	public float speed = 1.0f;
	public float meleeRange = 1.0f;
	public float meleeDamage = 1.0f;
	public float attackDelay = 0.5f;
	public float rotationSpeed = 360f;
	public Camera camera;

	private float attackTimer = 0;
	private float cameraX = 0;

	private CharacterController characterController;


	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate(0, Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime, 0);

		cameraX += -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
		cameraX = Mathf.Clamp(cameraX, -30f, 30f);
		camera.transform.localRotation = Quaternion.Euler(cameraX, camera.transform.localRotation.y, camera.transform.localRotation.z);

		Vector3 direction = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
		characterController.SimpleMove(direction * speed);

	}
}
