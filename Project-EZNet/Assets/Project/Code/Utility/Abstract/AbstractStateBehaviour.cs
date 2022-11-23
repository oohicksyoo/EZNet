using System;
using System.Collections;
using System.Collections.Generic;
using Project.Utility.Attributes;
using Project.Utility.Interfaces;
using UnityEngine;

namespace Project.Utility.Abstract {
	public abstract class AbstractStateBehaviour<T> : MonoBehaviour , IState<T> where T : Enum {

		
		#region IState

		[field: SerializeField]
		[field: GreyOut]
		public T CurrentState {
			get;
			private set;
		}

		#endregion


		#region MonoBehaviour

		public virtual void Update() {
			UpdateState(this.CurrentState);
		}

		#endregion


		#region Abstract Methods

		protected abstract void IntroState(T state);
		protected abstract void OutroState(T state);
		protected abstract void UpdateState(T state);

		#endregion


		#region Virtual Methods

		public virtual void SetState(T state) {
			if (!EqualityComparer<T>.Default.Equals(this.CurrentState, state)) {
				OutroState(this.CurrentState);
				ApplyState(state);
				IntroState(this.CurrentState);
			}
		}

		#endregion


		#region Protected Methods

		protected void ApplyState(T state) {
			this.CurrentState = state;
		}

		#endregion

	}
}