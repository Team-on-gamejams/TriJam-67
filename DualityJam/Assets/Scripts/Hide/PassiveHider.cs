using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveHider : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Player")
			GameManager.instance.player.Hide();
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.tag == "Player")
			GameManager.instance.player.UnHide();
	}
}
