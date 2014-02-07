using UnityEngine;
using System.Collections;

public class simcard : MonoBehaviour {
	public GameObject _name;
	public GameObject _hp;
	public GameObject _mp;
	public GameObject _sp;

	public GameObject _pawn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	public void updateUI () {
		_name.GetComponent<TextMesh> ().text = _pawn.GetComponent<pawn>()._name;
		_hp.GetComponent<TextMesh> ().text = "HP " + _pawn.GetComponent<pawn>()._hp;
		_mp.GetComponent<TextMesh> ().text = "MP " + _pawn.GetComponent<pawn>()._mp;
		_sp.GetComponent<TextMesh> ().text = "SP " + _pawn.GetComponent<pawn>()._sp;
	}
}
