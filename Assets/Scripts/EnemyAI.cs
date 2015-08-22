using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	// very simple stub AI: move towards hero
	public GameObject hero;
	public float speed = 1.0f;
	public float meleeRange = 1.0f;
	public float meleeDamage = 1.0f;
	public float attackDelay = 0.5f;

	private float attackTimer = 0;

	private CharacterController characterController;
	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		attackTimer -= Time.deltaTime;

		float sqrMeleeRange = meleeRange * meleeRange;


		if (hero != null) {
			// move towards hero if not close enough
			if (Vector3.SqrMagnitude(hero.transform.position - transform.position) < sqrMeleeRange) {
				// we are close enough
				Debug.Log ("Close enough to attack");
				if (attackTimer <= 0) {
					MeleeAttack();
					attackTimer = attackDelay;
				}
			} else {
				// we need to move closer to target
				Vector3 direction = (hero.transform.position - transform.position).normalized;
				characterController.SimpleMove(direction * speed);

			}
		} else {
			// try and find hero
			hero = GameObject.FindGameObjectWithTag("Hero");
		}
	}

	private void MeleeAttack() {
		Health health = hero.GetComponent<Health>();

		if (health != null) {
			health.TakeDamage(meleeDamage);
		}
	}

}
