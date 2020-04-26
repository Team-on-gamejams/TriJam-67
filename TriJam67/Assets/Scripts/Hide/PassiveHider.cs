using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveHider : MonoBehaviour {
	[SerializeField] GameObject tutorial;
	[SerializeField] bool needSlowMoEndTrigger = false;

	private void Awake() {
		tutorial?.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Player") {
			GameManager.instance.player.Hide(this);
			tutorial?.SetActive(true);
			if (needSlowMoEndTrigger)
				Time.timeScale = 0.05f;
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.tag == "Player") {
			GameManager.instance.player.UnHide(this);
			tutorial?.SetActive(false);
		}
	}
}
