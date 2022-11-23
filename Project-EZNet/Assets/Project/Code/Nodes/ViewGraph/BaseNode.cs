using System.Collections;
using Project.Managers;
using Project.Nodes.ViewGraph;
using UnityEngine;
using XNode;

namespace Assets.Project.Code.Nodes.ViewGraph {
	public abstract class BaseNode : Node {

		public NodeState State {
			get;
			set;
		} = NodeState.Unknown;

		public abstract IEnumerator ProcessNode();

		protected IEnumerator RunPort(string name) {
			this.State = NodeState.Ran;
			var port = GetPort(name);
			if (port.IsConnected) {
				var n = port.Connection.node;
				ViewGraphManager.Instance.CurrentBaseNode = (BaseNode) n;
				return ((BaseNode)n).ProcessNode();
			}
			
			Debug.LogError($"Port ({name}) is not connected in the view graph");
			return null;
		}

	}
}