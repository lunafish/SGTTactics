using UnityEngine;
using SimpleJSON;
using System.Collections;

public class UIDialog : MonoBehaviour {
	public GameObject _btn_1;
	public GameObject _btn_2;
	public GameObject _btn_3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool loadDlg( JSONNode json ) {
		Debug.Log (json ["name"]);
		setText (0, json ["dlg_1"]);
		setText (1, json ["dlg_2"]);
		setText (2, json ["dlg_3"]);

		return true;
	}

	public void setText( int btn, string text ) {
		if(btn == 0) {
			_btn_1.GetComponent<UIButton>().setText(text);
		} else if(btn == 1) {
			_btn_2.GetComponent<UIButton>().setText(text);
		} else if(btn == 2) {
			_btn_3.GetComponent<UIButton>().setText(text);
		}
	}

	public int getBtnIndex( GameObject obj ) {
		if(_btn_1 == obj) {
			return 0;
		} else if(_btn_2 == obj) {
			return 1;
		} else if(_btn_3 == obj) {
			return 2;
		}

		return -1;
	}
}
