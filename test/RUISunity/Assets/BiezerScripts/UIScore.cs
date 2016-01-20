using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScore : MonoBehaviour {

	public Text score;

	// Use this for initialization
	void Start () {
		score.text = "Score : " + FruitTriggerDestroy.SCORE;
		Debug.Log (FruitTriggerDestroy.SCORE);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
