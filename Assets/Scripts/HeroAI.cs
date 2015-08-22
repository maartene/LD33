using UnityEngine;
using System.Collections;

public class HeroAI : MonoBehaviour {

	// basic hero AI: just shoots at first target that come in range
	public GameObject target;
	public float shootRange = 3.0f;
	public float shootDamage = 2.0f;
	public float attackDelay = 0.5f;
	
	private float attackTimer = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		attackTimer -= Time.deltaTime;

		if (target != null) {
			// shoot
			Debug.Log ("Shoot! at target: "+target.name, target);
			if (attackTimer <= 0) {
				Shoot();
				attackTimer = attackDelay;
			}


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
