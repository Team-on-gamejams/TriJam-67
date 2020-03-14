using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
	[SerializeField] float moveSpeed = 1.0f;
	[SerializeField] float jumpForce = 1.0f;

	Rigidbody2D rb;

	Vector2 m_Rotation;
	Vector2 m_Look;
	Vector2 m_Move;

	private Vector3 m_Velocity = Vector3.zero;
	bool isGrounded = true;
	bool isFacingRight = true;

	private void Awake() {
		rb = GetComponent<Rigidbody2D>();
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.transform.tag == "Ground" && collision.transform.position.y < transform.position.y) {
			isGrounded = true;
		}
	}


	public void FixedUpdate() {
		Look(m_Look);
		Move(m_Move);
	}

	private void Move(Vector2 direction) {
		Vector3 targetVelocity = new Vector2(m_Move.x * moveSpeed, rb.velocity.y);
		rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, .05f);

		if (m_Move.x > 0 && !isFacingRight) {
			Flip();
		}
		else if (m_Move.x < 0 && isFacingRight) {
			Flip();
		}

		if (isGrounded && m_Move.y >= 0.5f) {
			isGrounded = false;
			rb.AddForce(new Vector2(0f, jumpForce));
		}
	}

	private void Look(Vector2 rotate) {

	}

	public void OnMove(InputAction.CallbackContext context) {
		m_Move = context.ReadValue<Vector2>();
		if (m_Move.y < 0)
			m_Move.y = 0;
	}

	public void OnLook(InputAction.CallbackContext context) {
		m_Look = context.ReadValue<Vector2>();
	}

	private void Flip() {
		isFacingRight = !isFacingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
