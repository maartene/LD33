using UnityEngine;
using System.Collections;

public class AutoRotate : MonoBehaviour {

	public Vector3 rotateSpeed = Vector3.up;
	public Space space = Space.World;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(rotateSpeed * Time.deltaTime, space);
	}
}
