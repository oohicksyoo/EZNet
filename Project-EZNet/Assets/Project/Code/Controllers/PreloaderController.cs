using System.Collections;
using UnityEngine;

namespace Project.Controllers {
	public class PreloaderController : MonoBehaviour {

		[SerializeField]
		private GameObject preloaderContainer;

		[SerializeField]
		private AnimationCurve fadeCurve;

		[SerializeField]
		private CanvasGroup canvasGroup;

		public void Awake() {
			preloaderContainer.SetActive(false);
		}

		public IEnumerator ShowPreloader(bool fadeOut) {
			preloaderContainer.SetActive(true);
			canvasGroup.alpha = 0;
			if (fadeOut) {
				yield return FadeTo(0, 1, 0.5f);
			} else {
				canvasGroup.alpha = 1;
			}
		}

		public IEnumerator HidePreloader(bool fadeIn = true) {
			preloaderContainer.SetActive(true);
			canvasGroup.alpha = 1;
			if (fadeIn) {
				yield return FadeTo(1, 0, 1.5f);
			}
			preloaderContainer.SetActive(false);
		}

		private IEnumerator FadeTo(float start, float end, float time) {
			canvasGroup.alpha = start;

			for (float i = 0; i < time; i += Time.deltaTime) {
				float t = fadeCurve.Evaluate(i / time);
				canvasGroup.alpha = Mathf.Lerp(start, end, t);
				yield return null;
			}
			
			canvasGroup.alpha = end;
		}
	}
}