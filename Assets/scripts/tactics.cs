using UnityEngine;
using System.Collections;

public class tactics : MonoBehaviour {
	private tacticsRule _rule = null;


	// Use this for initialization
	void Start () {
		_rule = tacticsRule.get ();
		_rule.makeTile ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
