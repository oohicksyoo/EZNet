using Project.Managers;
using Project.Nodes.ViewGraph;
using Project.Utility;
using Project.Utility.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.Views {
	public class MainMenuView : ViewBase {
		
		[SerializeField]
		private Button playTownButton;

		#region XNode

		[ViewOutput]
		public EmptyNode PlayGameNode {
			get;
			set;
		}

		#endregion
		
		public override void CleanupButtonListeners() {
			playTownButton.onClick.RemoveAllListeners();
		}

		public override void SetupButtonListeners() {
			playTownButton.onClick.AddListener(() => {
				ApplicationManager.Instance.SetSceneTransition(SceneList.DEMO);
				OnProcessNode("PlayGameNode");
			});
		}
	}
}