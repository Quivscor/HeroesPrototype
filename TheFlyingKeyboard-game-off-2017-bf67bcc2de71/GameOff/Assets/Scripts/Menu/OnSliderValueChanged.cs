using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnSliderValueChanged : MonoBehaviour {

	private GameObject toDestroy;
	public GameObject unitSelect;
	public Transform parent;
	public List<GameObject> unitHolder = new List<GameObject> ();
	public List<GameObject> unitList = new List<GameObject>() ;
	public Slider slider;

	public void OnSliderValue() {
		while (unitList.Count < slider.value) {
			unitHolder [unitList.Count].SetActive (true);
			unitList.Add(unitHolder[unitList.Count]);
		}

		while(unitList.Count > slider.value) {
			unitHolder [unitList.Count-1].SetActive (false);
			unitList.RemoveAt (unitList.Count - 1);
		}

		//Debug.Log (unitList.Count + "/" + slider.value);
	}
}
