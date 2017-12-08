using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Text timeLeftText;
	public float gameLength = 100f;
	public float timeLeft;

	void Start () {
		timeLeft = gameLength;
	}

	void Update () {

		if (timeLeft > 0) {
			timeLeft -= Time.deltaTime;
			timeLeftText.text = ((int) timeLeft).ToString ();
		} else {
			// LOSE
		}

	}
}
