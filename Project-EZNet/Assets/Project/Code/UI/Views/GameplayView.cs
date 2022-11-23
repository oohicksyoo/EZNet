using Project.Nodes.ViewGraph;
using Project.Utility.Attributes;

namespace Project.UI.Views {
	public class GameplayView : ViewBase {

		#region XNode

		[ViewOutput]
		public EmptyNode PlayGameNode {
			get;
			set;
		}

		#endregion
		
		public override void CleanupButtonListeners() {
			
		}

		public override void SetupButtonListeners() {
		}
	}
}