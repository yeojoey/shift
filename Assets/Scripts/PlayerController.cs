using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public enum State
	{
		Idle,
		Walking_Right,
		Walking_Left,
		Going_Ghost,
		Going_Real

	}


	public State currentState;
	public float speed = 5f;
	public bool isGhost;
	public float transformTime = 0.5f;

	private BoxCollider2D collider;
    private SpriteRenderer spriteR;
    private Sprite[] sprites;

    void Start ()
	{
		isGhost = false;
		collider = GetComponent<BoxCollider2D> ();
		currentState = State.Idle;
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("doggo");
    }

	void OnCollisionEnter2D  (Collision2D other) {
		if (other.gameObject.CompareTag ("Obstacle")) {
			other.gameObject.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
		}
	}

	void OnCollisionExit2D (Collision2D other) {
		if (other.gameObject.CompareTag ("Obstacle")) {
			other.gameObject.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Static;
		}
	}


	void Update ()
	{

		transform.Translate (speed * 0.01f * (Vector2.right * Input.GetAxis ("Horizontal") + Vector2.up * Input.GetAxis ("Vertical")));


		switch (currentState) {

		case State.Idle:
		case State.Walking_Left:
		case State.Walking_Right:
			if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift)) {
				if (!isGhost) {
					StartCoroutine (GoGhost ());
				} else {
					StartCoroutine (GoReal ());
				}
			}
			break;

		default:
			break;

		}

	}

	private IEnumerator GoGhost ()
	{
		currentState = State.Going_Ghost;
		float elapsedTime = 0f;
		while (elapsedTime < transformTime) {
			elapsedTime += Time.unscaledDeltaTime;
			yield return null;
		}
		isGhost = true;
		collider.enabled = false;
		currentState = State.Idle;
        spriteR.sprite = sprites[1];
    }

	private IEnumerator GoReal ()
	{
		currentState = State.Going_Real;
		float elapsedTime = 0f;
		while (elapsedTime < transformTime) {
			elapsedTime += Time.unscaledDeltaTime;
			yield return null;
		}
		isGhost = false;
		collider.enabled = true;
		currentState = State.Idle;
        spriteR.sprite = sprites[20];

    }

}
