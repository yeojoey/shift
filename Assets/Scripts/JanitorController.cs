using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class JanitorController : MonoBehaviour {

	AIDestinationSetter destSetter;
	AILerp aiLerp;

	void Start () {
		destSetter = GetComponent<AIDestinationSetter> ();
		aiLerp = GetComponent<AILerp> ();
	}

	void Update () {

		AstarPath.active.Scan (); // Recalculate the graph
	}

}
