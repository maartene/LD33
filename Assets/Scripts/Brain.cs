using UnityEngine;
using System.Collections;

public class Brain : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		Destroy (gameObject);
		PlayerEnemy playerEnemy = col.GetComponent<PlayerEnemy>();
		if (playerEnemy != null) {
			playerEnemy.AddBrain();
		}
	}
}
