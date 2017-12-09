using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class JanitorController : AILerp {

	public enum State {
		Idle = 0,
		WalkingRight,
		WalkingLeft,
		DetectedPlayer
	}

	AIDestinationSetter destSetter;

	void Start () {
		destSetter = GetComponent<AIDestinationSetter> ();
	}

	void OnTargetReached () {
		print (1);
	}

	void Update () {

	}

}
