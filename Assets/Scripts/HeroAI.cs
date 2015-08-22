using UnityEngine;
using System.Collections;

public class HeroAI : MonoBehaviour {

	// basic hero AI: just shoots at first target that come in range
	public GameObject target;
	public float shootRange = 3.0f;
	public float shootDamage = 2.0f;
	public float attackDelay = 0.5f;
	public float rotateSpeed = 360f;
	
	private float _attackTimer = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		_attackTimer -= Time.deltaTime;

		if (target != null) {
			// shoot
			Debug.Log ("Shoot! at target: "+target.name, target);
			if (_attackTimer <= 0) {
				Shoot();
				_attackTimer = attackDelay;
			}

			Vector3 direction = target.transform.position - transform.position;

			Quaternion lookRotation = Quaternion.LookRotation(direction);
			Debug.DrawLine(transform.position, transform.position + direction, Color.blue, 0.2f);
			Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
			newRotation = Quaternion.Euler(0, newRotation.eulerAngles.y, 0);
			transform.rotation = newRotation;


		} else {
			// try and find target
			target = FindTargetWithinRange(shootRange, "Enemy");
		}

	}

	private GameObject FindTargetWithinRange(float range, string tag) {
		float sqrRange = range*range;
		GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);

		foreach (GameObject go in gos) {
			if (Vector3.SqrMagnitude(go.transform.position - transform.position) < sqrRange) {
				return go;
			}
		}

		return null;
	}

	private void Shoot() {
		Health health = target.GetComponent<Health>();
		
		if (health != null) {
			health.TakeDamage(shootDamage);
		}
	}
}
