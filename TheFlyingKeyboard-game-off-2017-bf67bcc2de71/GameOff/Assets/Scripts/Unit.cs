using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
	//public FieldOfView fov;
	public TurnSystemScript turnSystem;
	public TurnClass turnClass;
	public bool isTurn;

	public bool isMoving = false;
	public bool cursorInRange;
	public int team;

	public float timeToTravel = 2;

//		STATYSTYKI		//////////////////////////////////////

	private Vector3 moveTarget;

	public int unitHealth;
	public int maxHealth;
	public int attack;
	public int defense;
	public int initiative;
	public int moveDistance;
	public Vector2 faceDir;
	public bool hasFirstStrike;
	public bool isCharging;

	public int unitsAmount;
	private int startUnitsAmount;

///////////////////////////////////////////////////////////////	

	public void Movement() {
		if (!isMoving && Input.GetMouseButtonDown (0)) {
			if (cursorInRange) {
				isMoving = true;
				moveTarget = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				moveTarget.z = transform.position.z;
			}
		}
		if(isMoving && !Mathf.Approximately(gameObject.transform.position.magnitude, moveTarget.magnitude)) {
			Move(moveTarget);
		}
		if(isMoving && Mathf.Approximately(gameObject.transform.position.magnitude, moveTarget.magnitude)) {
			isMoving = false;
			isTurn = false;
			turnClass.isTurn = isTurn;
			turnClass.wasTurnPrev = true;
			Reset ();
		}
	}

///// SEKWENCJA WALKI	////////////////////////////////

	//Zwraca liste targetow (element listy to ta jednostka ktora obrywa w tej turze)
	public List<Unit> TargetList(Unit enemy, bool isCharging) {
		List<Unit> targets = new List<Unit> ();
		if (!isCharging) {
			if (this.hasFirstStrike || !enemy.hasFirstStrike) {
				targets.Add (enemy);
				for (int i = 0; i < 5; i++) {
					if (i % 2 == 0) {
						targets.Add (this);
					} else {
						targets.Add (enemy);
					}
				}
			} else if (enemy.hasFirstStrike) {
				for (int i = 0; i < 6; i++) {
					if (i % 2 == 0) {
						targets.Add (this);
					} else {
						targets.Add (enemy);
					}
				}
			}
		} else {
			if (this.hasFirstStrike || !enemy.hasFirstStrike) {
				targets.Add (enemy);
				targets.Add (this);
			} else if (enemy.hasFirstStrike) {
				targets.Add (this);
				targets.Add (enemy);
			}
		}
		return targets;
	}

	float CalculateMultipler(Unit enemy) {
		float multiplier;
		float angle = Vector2.Angle (this.faceDir, enemy.faceDir);
		Debug.Log (angle);
		return angle;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (isTurn) {
			if (other.gameObject.tag == "Unit") {
				Unit unit = other.gameObject.GetComponent<Unit> ();
				if (this.team != unit.team) {
					List<Unit> targetList = new List<Unit> ();
					targetList = TargetList (unit, isCharging);
					float multiplier = CalculateMultipler (unit);
					Debug.Log ("napieprzanie");
					for (int i = 0; i < 6; i++) {
						Debug.Log ("Target["+i+"]" + targetList [i]);
						Attack(targetList[i]);
					}
				}
			}
		}
	}

////////////////////////////////////////////////////////

	public void Attack(Unit target) {
		int damage = unitsAmount * (this.attack - target.defense);
		target.UpdateHealth (-1 * damage);
	}

	public void Defend() {
		this.defense *= 2;
	}

	public void Move(Vector3 target) {
		faceDir = new Vector2(target.x,target.y).normalized;
		transform.position = Vector3.MoveTowards (transform.position, target, timeToTravel * Time.deltaTime);
	}

	public void UpdateHealth(int health) {
		this.maxHealth += health;
		for(int i = 1; i < unitsAmount; i++) {
			if (health > unitHealth) {
				this.unitsAmount--;
				this.maxHealth -= this.unitHealth;
			}
		}
	}

	public void InitStats(int unitHealth, int attack, int defense, int initiative, int moveDistance, bool hasFirstStrike, bool isCharging) {
		this.unitHealth = unitHealth;
		this.attack = attack;
		this.defense = defense;
		this.initiative = initiative;
		this.moveDistance = moveDistance;
		this.hasFirstStrike = hasFirstStrike;
		this.isCharging = isCharging;
	}

	public void InitNumbers(int startUnitsAmount) {
		this.startUnitsAmount = startUnitsAmount;
		this.unitsAmount = startUnitsAmount;
		this.maxHealth = startUnitsAmount * unitHealth;
	}

	public void Reset() {
		if (isMoving == false) {
			moveTarget = Vector2.zero;
		}
	}
}
