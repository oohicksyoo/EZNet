using System.Collections;

namespace Project.Nodes.ViewGraph {
	public class PassThroughNode : LogicNode {
		
		[Output]
		public EmptyNode onComplete;
		
		public override IEnumerator ProcessNode() {
			this.State = NodeState.Running;
			yield return RunPort("onComplete");
		}
		
	}
}