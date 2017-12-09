using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public enum State
	{
		Idle = 0,
		WalkingRight = 1,
		WalkingLeft = 2,
		GoingGhost = 3,
		GoingReal = 4,
		Pushing = 5
	}


	public State currentState;
	public float speed = 5f;
	public bool isGhost;
	public float transformTime = 0.5f;
	public float idleThreshold = 0.1f;


	private Collider2D collider;
    private SpriteRenderer spriteR;
    private Sprite[] sprites;
	private Animator animator;



    void Start ()
	{
		isGhost = false;
		collider = GetComponent<Collider2D> ();
		currentState = State.Idle;
		animator = GetComponent<Animator> ();
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("doggo");
    }

	void OnCollisionEnter2D  (Collision2D other) {
		if (other.gameObject.CompareTag ("Obstacle") && currentState != State.Pushing) {
			currentState = State.Pushing;
			other.gameObject.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
		}
	}

	void OnCollisionExit2D (Collision2D other) {
		if (other.gameObject.CompareTag ("Obstacle")) {
			other.gameObject.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Static;
			currentState = State.Idle;
		}

		AstarPath.active.Scan ();
	}


	void Update ()
	{

		Vector2 direction;

		switch (currentState) {

		case State.Idle:
		case State.WalkingLeft:
		case State.WalkingRight:


			direction = Vector2.right * Input.GetAxis ("Horizontal") + Vector2.up * Input.GetAxis ("Vertical");

			if (direction.magnitude < idleThreshold) {
				currentState = State.Idle;
			} else if (direction.x > 0) {
				currentState = State.WalkingRight;
			} else {
				currentState = State.WalkingLeft;
			}

			transform.Translate (speed * 0.01f * direction);

			if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift)) {
				if (!isGhost) {
					StartCoroutine (GoGhost ());
				} else {
					StartCoroutine (GoReal ());
				}
			}

			break;

		case State.Pushing:
			direction = Vector2.right * Input.GetAxis ("Horizontal") + Vector2.up * Input.GetAxis ("Vertical");
			transform.Translate (speed * 0.01f * direction);

			if (direction.magnitude < idleThreshold) {
				currentState = State.Idle;
			}

			break;

		default:
			break;

		}

		UpdateAnimator ();

	}

	private IEnumerator GoGhost ()
	{
		currentState = State.GoingGhost;
		float elapsedTime = 0f;
		while (elapsedTime < transformTime) {
			elapsedTime += Time.unscaledDeltaTime;
			yield return null;
		}
		isGhost = true;
		collider.enabled = false;
		currentState = State.Idle;
    }

	private IEnumerator GoReal ()
	{
		currentState = State.GoingReal;
		float elapsedTime = 0f;
		while (elapsedTime < transformTime) {
			elapsedTime += Time.unscaledDeltaTime;
			yield return null;
		}
		isGhost = false;
		collider.enabled = true;
		currentState = State.Idle;

    }

	private void UpdateAnimator () {
		animator.SetBool ("isGhost", isGhost);
		animator.SetInteger ("currentState", (int)currentState);
	}

}
