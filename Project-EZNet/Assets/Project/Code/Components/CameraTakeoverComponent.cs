using Project.Managers;
using UnityEngine;

namespace Project.Components {
	public class CameraTakeoverComponent : MonoBehaviour {

		private UnityEngine.Camera internalCamera;
		
		private UnityEngine.Camera Camera {
			get {
				return internalCamera = internalCamera ?? GetComponent<UnityEngine.Camera>();
			}
		}

		public void OnEnable() {
			if (CameraManager.Instance) {
				CameraManager.Instance.TakeOverCamera(this.Camera);
			}
		}
	}
}