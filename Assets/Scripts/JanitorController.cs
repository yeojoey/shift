using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class JanitorController : AILerp {

	AIDestinationSetter destSetter;
	AILerp aiLerp;

	void Start () {
		destSetter = GetComponent<AIDestinationSetter> ();
		aiLerp = GetComponent<AILerp> ();
	}

	void OnTargetReached () {
		print (1);
	}

	void Update () {

		AstarPath.active.Scan (); // Recalculate the graph
	}

}
