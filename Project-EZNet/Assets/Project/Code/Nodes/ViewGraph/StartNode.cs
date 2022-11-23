using System.Collections;
using Assets.Project.Code.Nodes.ViewGraph;

namespace Project.Nodes.ViewGraph {
	public class StartNode : BaseNode {
		[Output]
		public EmptyNode exit;

		public override IEnumerator ProcessNode() {
			yield return RunPort("exit");
		}
	}
}