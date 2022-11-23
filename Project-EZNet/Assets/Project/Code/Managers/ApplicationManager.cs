using Assets.Project.Code.Scriptable;
using Project.Controllers;
using Project.Utility;

namespace Project.Managers {
	public class ApplicationManager : Singleton<ApplicationManager> {

		#region Properties

		public LevelController CurrentLevelController {
			get;
			set;
		}

		public string LastScene {
			get;
			set;
		}

		public string LevelScene {
			get;
			set;
		}

		#endregion


		#region MonoBehaviour

		public override void Awake() {
			base.Awake();
		}

		#endregion


		#region Public Methods

		public void SetSceneTransition(string scene) {
			this.LastScene = this.LevelScene;
			this.LevelScene = scene;
		}

		#endregion
		
	}
}