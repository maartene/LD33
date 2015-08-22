using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	// very simple stub AI: move towards hero
	public GameObject hero;
	public GameObject brains;
	public float speed = 1.0f;
	public float meleeRange = 1.0f;
	public float meleeDamage = 1.0f;
	public float attackDelay = 0.5f;
	public float rotateSpeed = 360f;

	private float _attackTimer = 0;

	private CharacterController _characterController;
	// Use this for initialization
	void Start () {
		_characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		_attackTimer -= Time.deltaTime;

		float sqrMeleeRange = meleeRange * meleeRange;

		if (brains != null) {
			Vector3 direction = (brains.transform.position - transform.position).normalized;
			_characterController.SimpleMove(direction * speed);

			// rotate towards brains
			Quaternion lookRotation = Quaternion.LookRotation(direction);
			//Debug.DrawLine(transform.position, transform.position + direction, Color.blue, 0.2f);
			Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
			newRotation = Quaternion.Euler(0, newRotation.eulerAngles.y, 0);
			transform.rotation = newRotation;
		}

		if (hero != null) {
			Vector3 direction = (hero.transform.position - transform.position).normalized;
			// move towards hero if not close enough
			if (Vector3.SqrMagnitude(hero.transform.position - transform.position) < sqrMeleeRange) {
				// we are close enough
				Debug.Log ("Close enough to attack");
				if (_attackTimer <= 0) {
					MeleeAttack();
					_attackTimer = attackDelay;
				}
			} else {
				// we need to move closer to target
				_characterController.SimpleMove(direction * speed);

			}

			// rotate towards hero

			Quaternion lookRotation = Quaternion.LookRotation(direction);
			//Debug.DrawLine(transform.position, transform.position + direction, Color.blue, 0.2f);
			Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
			newRotation = Quaternion.Euler(0, newRotation.eulerAngles.y, 0);
			transform.rotation = newRotation;

		} 

		if (brains == null & hero == null) {
			brains = GameObject.FindGameObjectWithTag("Brains");
			if (brains == null) {
				hero = GameObject.FindGameObjectWithTag("Hero");
			}
		}
	}

	private void MeleeAttack() {
		Health health = hero.GetComponent<Health>();

		if (health != null) {
			health.TakeDamage(meleeDamage);
		}
	}

}
