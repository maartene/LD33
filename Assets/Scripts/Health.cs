using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public float health = 10;
	public GameObject gib;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakeDamage(float damage) {
		health -= damage;
		if (health <= 0) {
			Die();
		}
	}

	private void Die() {
		Destroy(gameObject);
		if (gib != null) {
			Instantiate(gib, transform.position, transform.rotation);
		}
	}
}
