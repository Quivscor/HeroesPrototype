using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataHolder : MonoBehaviour {

	public static DataHolder Instance { get; private set; }
	////////////////////////////////////////////////////////////////////////////
	public GameObject listHolderTeam1;
	public GameObject listHolderTeam2;
	public List<GameObject> unitListTeam1;
	public List<GameObject> unitListTeam2;
	public int[] unitTypeTeam1 = new int[7];
	public int[] unitTypeTeam2 = new int[7];
	public int[] unitQuantityTeam1 = new int[7];
	public int[] unitQuantityTeam2 = new int[7];
	////////////////////////////////////////////////////////////////////////////
	public void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	public void Update() {
		unitListTeam1 = listHolderTeam1.GetComponent<OnSliderValueChanged> ().unitList;
		unitListTeam2 = listHolderTeam2.GetComponent<OnSliderValueChanged> ().unitList;
		/*for (int i = 0; i < 7; i++) {
			unitTypeTeam1 [i] = unitListTeam1 [i].GetComponent<Dropdown> ().value;
			unitTypeTeam2 [i] = unitListTeam2 [i].GetComponent<Dropdown> ().value;
			unitQuantityTeam1[i] = int.Parse(unitListTeam1 [i].GetComponent<InputField> ().text);
			unitQuantityTeam2[i] = int.Parse(unitListTeam2 [i].GetComponent<InputField> ().text);
		}*/
	}
}
