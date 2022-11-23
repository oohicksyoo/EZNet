using System.Collections;
using Project.Managers;
using Project.Utility;

namespace Project.Nodes.ViewGraph.Process {
	public class RunLoadSceneNode : LogicNode {
		
		[Output]
		public EmptyNode exitNode;
		
		public override IEnumerator ProcessNode() {
			this.State = NodeState.Running;
			
			//Clear top view main menu or in game
			ViewGraphManager.Instance.PopTopView();
			
			bool isLoading = true;
			
			//Remove views off stack since we will be seeing preloader
			SceneManager.Instance.LoadScene(ApplicationManager.Instance.LevelScene, sceneName => {
				SceneManager.Instance.HidePreloader(true);
				isLoading = false;
			}, true);

			while (isLoading) {
				yield return null;
			}
			
			yield return RunPort("exitNode");
		}
	}
}