using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public enum State {
		Playing, Paused, GameOver
	}

	public State currentState = State.Playing;

	public Text timeLeftText;
	public float gameLength = 100f;
	public float timeLeft;

	void Start () {
		timeLeft = gameLength;
	}

	void Update () {

		switch (currentState) {

		case State.Playing:

			if (timeLeft > 0) {
				timeLeft -= Time.deltaTime;
				timeLeftText.text = ((int)timeLeft).ToString ();
			} else {
				currentState = State.GameOver;
				// LOSE
			}
			break;

		case State.Paused:
			break;

		case State.GameOver:
			break;

		default:
			break;


		}

	}
}
