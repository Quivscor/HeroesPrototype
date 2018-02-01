using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystemScript : MonoBehaviour {

	public List<TurnClass> unitsGroup;

	// Use this for initialization
	void Start () {


		ResetTurns ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateTurns ();
	}

	void ResetTurns() {
		Debug.Log ("Reset Turns");
		for (int i = 0; i < unitsGroup.Count; i++) {
			if (i == 0) {
				Debug.Log ("First Object");
				unitsGroup [i].isTurn = true;
				unitsGroup [i].wasTurnPrev = false;
			} else {
				unitsGroup [i].isTurn = false;
				unitsGroup [i].wasTurnPrev = false;
			}
		}
	}

	void UpdateTurns() {
		for (int i = 0; i < unitsGroup.Count; i++) {
			if (!unitsGroup [i].wasTurnPrev) {
				unitsGroup [i].isTurn = true;
				break;
			} else if (i == unitsGroup.Count - 1 && unitsGroup[i].wasTurnPrev) {
				ResetTurns ();
			}
		}
	}
}

[System.Serializable]
public class TurnClass {
	public Unit unitGameObject;
	public bool isTurn;
	public bool wasTurnPrev;
}