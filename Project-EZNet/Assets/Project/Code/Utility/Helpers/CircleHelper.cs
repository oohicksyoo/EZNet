using UnityEngine;

#if UNITY_EDITOR
	using UnityEditor;
#endif

namespace Project.Utility.Helpers {
	public class CircleHelper : MonoBehaviour {

		[SerializeField]
		private Color color = Color.white;

		[SerializeField]
		private float radius = 0.5f;
		
		#if UNITY_EDITOR
		public void OnDrawGizmos() {
			Handles.color = color;
			Handles.DrawWireDisc(transform.position, Vector3.forward, radius);
			Handles.DrawLine(transform.position, transform.position + transform.up * radius);
		}
		#endif

	}
}