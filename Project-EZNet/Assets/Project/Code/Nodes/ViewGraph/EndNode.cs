using System.Collections;

namespace Project.Nodes.ViewGraph {
	public class EndNode : LogicNode {
		public override IEnumerator ProcessNode() {
			this.State = NodeState.Ran;
			yield return null;
		}
	}
}