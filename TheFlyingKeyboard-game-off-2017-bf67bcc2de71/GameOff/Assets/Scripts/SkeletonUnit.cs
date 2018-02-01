using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonUnit : Unit {

	private int origUnitHealth = 13;
	private int origMaxHealth;
	private int origAttack = 5;
	private int origDefense = 2;
	private int origInitiative = 3;
	private int origMoveDistance = 50;

	public void Start() {
		InitStats (13, 5, 2, 3, 10, false, false);

		turnSystem = GameObject.Find ("Turn System Controller").GetComponent<TurnSystemScript> ();
		faceDir = Vector2.right;
		foreach (TurnClass tc in turnSystem.unitsGroup) {
			if (tc.unitGameObject.name == gameObject.name)
				turnClass = tc;
		}
	}

	public void Update() {
		isTurn = turnClass.isTurn;
		Debug.DrawRay (transform.position, faceDir * 5f);
		Debug.Log ("faceDir = " + faceDir);
		if (isTurn) {
			Movement ();
		}
	}

}
