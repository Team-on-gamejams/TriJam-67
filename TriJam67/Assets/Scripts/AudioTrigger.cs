using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour {
	[SerializeField] bool isPlayingControllOutside = false;
	[SerializeField] bool isSmoothFade = false;
	[SerializeField] AudioSource source;
	[SerializeField] AudioClip clip;

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.tag == "Player" && (isPlayingControllOutside || !source.isPlaying || source.volume == 0.0f)) {
			source.clip = clip;
			source.Play();

			if (isSmoothFade) {
				LeanTween.cancel(gameObject, false);
				LeanTween.value(gameObject, 0.0f, 1.0f, 1.0f)
				.setOnUpdate((float f)=> {
					source.volume = f;
				});
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.tag == "Player" && (isPlayingControllOutside || source.isPlaying || source.volume != 0.0f) && source.clip == clip) {
			if (isSmoothFade) {
				LeanTween.cancel(gameObject, false);
				LeanTween.value(gameObject, source.volume, 0.0f, 1.0f)
				.setOnUpdate((float f) => {
					source.volume = f;
				})
				.setOnComplete(()=> { 
					source.Stop();
				});
			}
			else {
				source.Stop();
			}
		}
	}
}
