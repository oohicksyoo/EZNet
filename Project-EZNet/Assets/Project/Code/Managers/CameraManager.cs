using Project.Utility;
using UnityEngine;

namespace Project.Managers {
	public class CameraManager : Singleton<CameraManager> {

		[SerializeField]
		private UnityEngine.Camera defaultCamera;

		public UnityEngine.Camera Current {
			get;
			private set;
		}

		public override void Awake() {
			base.Awake();

			FreeCamera();
		}

		public void TakeOverCamera(UnityEngine.Camera camera) {
			if (this.Current) {
				this.Current.gameObject.SetActive(false);
			}
			
			this.Current = camera;
		}

		public void FreeCamera() {
			defaultCamera.gameObject.SetActive(true);
			TakeOverCamera(defaultCamera);
		}

	}
}