using UnityEngine;
using System.Collections;
using Project.Controllers;
using Project.Managers;
using Project.Utility;

namespace Project.Nodes.ViewGraph.Process {
	public class RunInGameNode : LogicNode {
		
		[Output]
		public EmptyNode exitNode;
		
		public override IEnumerator ProcessNode() {
			this.State = NodeState.Running;

			LevelController levelController = ApplicationManager.Instance.CurrentLevelController;

			//Wait for levelController to be set
			while (levelController == null) {
				yield return null;
			}

			Debug.Log($"[RunInGameNode] LevelController has been found, waiting for outro state");

			//Wait for the levelController to tell us we are good to exit the game
			while (levelController.CurrentState != LevelController.State.Outro) {
				yield return null;
			}
			
			SceneManager.Instance.UnloadScene(SceneList.DEMO);
			CameraManager.Instance.FreeCamera();

			yield return RunPort("exitNode");
		}
	}
}