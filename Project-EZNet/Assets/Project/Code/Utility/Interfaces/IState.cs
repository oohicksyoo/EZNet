using System;

namespace Project.Utility.Interfaces {
	public interface IState<T> where T : Enum {
		T CurrentState {
			get;
		}

		void SetState(T state);
	}
}