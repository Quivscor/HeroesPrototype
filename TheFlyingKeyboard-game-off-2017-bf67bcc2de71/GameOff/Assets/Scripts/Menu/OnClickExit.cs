using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickExit : MonoBehaviour {
	public void ExitGame() {
		Debug.Log ("Exit");
		Application.Quit ();
	}
}
