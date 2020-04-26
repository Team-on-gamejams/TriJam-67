using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpotArea : MonoBehaviour {
	[SerializeField] bool isNeedRotating = false;
	[SerializeField] float minAngle = -60f;
	[SerializeField] float maxAngle = 40f;
	[SerializeField] float angularSpeed = 10.0f;
	[SerializeField] float stayTime = 2.0f;
	bool isLookUp;
	float currStayTime = 0.0f;

	[HideInInspector] [SerializeField] EnemySpotter spotter;

	bool isPlayerInSpot = false;
	bool isSeePlayerInSpot = false;

	private void OnValidate() {
		if (spotter == null)
			spotter = GetComponentInParent<EnemySpotter>();
	}

	private void Awake() {
		isLookUp = Random.Range(0, 2) == 1;
	}

	private void Update() {
		if (isNeedRotating) {
			if (currStayTime != 0) {
				currStayTime -= Time.deltaTime;
				if (currStayTime < 0)
					currStayTime = 0;
			}
			else {
				Vector3 euler = transform.localEulerAngles;
				if (euler.z > 180f)
					euler.z -= 360f;

				if (isLookUp)
					euler.z += angularSpeed * Time.deltaTime;
				else
					euler.z -= angularSpeed * Time.deltaTime;

				if ((euler.z < minAngle && !isLookUp) || (maxAngle < euler.z && isLookUp)) {
					euler.z = isLookUp ? maxAngle : minAngle;
					currStayTime = stayTime;
					isLookUp = !isLookUp;
				}

				transform.localEulerAngles = euler;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Player" && !GameManager.instance.player.IsHided()) {
			isPlayerInSpot = true;
		}
	}

	private void OnTriggerStay2D(Collider2D collision) {
		if (collision.tag != "Player")
			return;

		bool isSeeNow = !GameManager.instance.player.IsHided();
		if (isSeeNow != isSeePlayerInSpot) {
			isSeePlayerInSpot = isSeeNow;
			if(isSeePlayerInSpot)
				spotter.OnSpotPlayer();
			else
				spotter.OnPlayerHide();
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.tag != "Player")
			return;
		if (isSeePlayerInSpot)
			spotter.OnPlayerHide();
		isSeePlayerInSpot = isPlayerInSpot = false;
	}
}
