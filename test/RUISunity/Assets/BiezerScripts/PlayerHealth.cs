using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerHealth : MonoBehaviour {

	public float defaultHealth;
	public float regenerationTime;
	private float health;
	public Slider healthSlider;                                 // Reference to the UI's health bar.
	public Text hp;
	public Text pts;
	public int points;
	public long time;
	public int defaultRestartTime = 10;
	private int restartTime;
	private bool gameOver = false;

	private GameObject[] enemiesDarthSidius;
	// Use this for initialization

	private void initial()
	{
		gameOver = false;
		//time = 0;
		defaultHealth = FruitTriggerDestroy.missesLimit;
		health = defaultHealth;
		points = 0;
		healthSlider.value = health/defaultHealth * 100;
		//InvokeRepeating ("RegenerateHealth", regenerationTime, regenerationTime);
		String str = health.ToString ();
		hp.text = str;
		pts.text = this.points.ToString ();

		/*foreach ( flyingPlanes obj in GameObject.FindGameObjectWithTag ("Scripts").GetComponents <flyingPlanes>())
		{
			obj.startFire();
		}*/

	}

	void Start () {
		initial ();
	}


	public void setMisses(int x){
		health = x;
		healthSlider.value = health / defaultHealth * 100;
		String str = health.ToString ();
		hp.text = str;
	}

	public void getScore (int _score)
	{
		this.points = _score;
		pts.text = this.points.ToString ();
	}

	private void GameOver()
	{



		/*Debug.Log ("gameover");
		gameOver = true;
		this.CancelInvoke ();

		Debug.Log ("planes ? ");
		foreach ( flyingPlanes obj in GameObject.FindGameObjectWithTag ("Scripts").GetComponents <flyingPlanes>())
		{
			Debug.Log ("planes! ");
			obj.stopFire();
		}

		foreach( GameObject obj in GameObject.FindGameObjectsWithTag("avion"))
		{
			Destroy(obj);
		}

		//show gameover screen
		GameObject.FindGameObjectWithTag ("gameOverLight").GetComponent<Canvas> ().enabled = true;
		//GameObject.FindGameObjectWithTag ("gameOverLight").GetComponent<Canvas> ().GetComponent<Text> ().text = "Score: " + points;
		GameObject.FindGameObjectWithTag ("gameOverLight").GetComponentInChildren<Text> ().text = "Score: " + points;

		restartGame ();*/
	}

	private void restartGame()
	{
		restartTime = defaultRestartTime;
		InvokeRepeating ( "countDown" ,1f, 1f);
	}

	private void countDown()
	{
		Text txt = GameObject.FindGameObjectWithTag ("gameOverLight").GetComponentInChildren<Text> ();

		if (restartTime > 0) {

			txt.text = "Game will restart in " + restartTime-- + " seconds.";
			return ;
		}

	

		GameObject.FindGameObjectWithTag ("gameOverLight").GetComponent<Canvas> ().enabled = false;
		txt.text = "";

		initial ();
		CancelInvoke ("countDown");
	}
}
