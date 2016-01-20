using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {

	void Update () {
		if (Input.GetKey(KeyCode.R) && FruitTriggerDestroy.isGameOver == true){
			FruitTriggerDestroy.isGameOver = false;
			FruitTriggerDestroy.MISSES = 0;
			FruitTriggerDestroy.SCORE = 0;
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
