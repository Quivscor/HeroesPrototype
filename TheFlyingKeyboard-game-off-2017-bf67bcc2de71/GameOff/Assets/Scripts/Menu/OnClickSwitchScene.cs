using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class OnClickSwitchScene : MonoBehaviour {
	public void SwitchScene ()
	{
		Debug.Log ("New scene");
		SceneManager.LoadScene ("BattleMap");
	}
}
