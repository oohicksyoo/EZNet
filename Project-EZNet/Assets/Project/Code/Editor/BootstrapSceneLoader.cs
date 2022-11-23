using Project.Utility;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Project.Editor {
	public class BootstrapSceneLoader {
		[MenuItem("RPG/Bootstrap")]
		public static void OpenBootstrapScene() {
			EditorSceneManager.OpenScene(SceneList.BOOTSTRAP);
		}
	}
}