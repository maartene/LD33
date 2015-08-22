using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowBrainCount : MonoBehaviour {

	public PlayerEnemy playerEnemy;
	private Text _text;
	private string _initialText;

	// Use this for initialization
	void Start () {
		_text = GetComponent<Text>();
		_initialText = _text.text;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerEnemy != null) {
			_text.text = _initialText + playerEnemy.BrainCount;
		}
	}
}
