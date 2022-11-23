using System.Collections;
using Assets.Project.Code.Nodes.ViewGraph;

namespace Project.Nodes.ViewGraph {
	public abstract class LogicNode : BaseNode {
		[Input]
		public EmptyNode entry;

		public abstract override IEnumerator ProcessNode();
	}
}