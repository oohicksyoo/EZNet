using System;
using Assets.Project.Code.Scriptable;
using Project.Managers;
using Project.Utility;
using Project.Utility.Abstract;
using Project.Utility.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Controllers {
	
	/// <summary>
	/// Main driver/controller for the game level we are in
	/// </summary>
	public class LevelController : AbstractStateBehaviour<LevelController.State> {


		#region Types

		public enum State {
			None = 0,
			Initialization,
			Playing,
			Outro,
		}

		#endregion
		
		
		#region Serialized Fields
		
		

		#endregion


		#region NonSerialized Fields

		[NonSerialized]
		private IState<GameController.State> gameControllerState;

		[NonSerialized]
		private GameController gameController;

		#endregion


		#region Properties

		public IState<GameController.State> GameControllerState {
			get {
				return gameControllerState ??= GetComponent<IState<GameController.State>>();
			}
		}

		public GameController GameController {
			get {
				return gameController ??= GetComponent<GameController>();
			}
		}

		public LevelData LevelData {
			get;
			private set;
		}

		#endregion
		
		
		#region MonoBehaviour

		public void Awake() {
			if (ApplicationManager.Instance) {
				ApplicationManager.Instance.CurrentLevelController = this;
			}
		}

		#endregion


		#region Private Functions

		

		#endregion

		
		#region AbstractStateBehaviour

		protected override void IntroState(State state) {
			switch (state) {
				case State.Initialization:
					//TODO: Initialize Level
					//TODO: Convert passed guid to the select level by the game other wise fallback to a preset mostly used in testing
					this.LevelData = LevelDataScriptable.Instance.GetLevelData("2ab274386bf56444fbd4cd067d367209");
					this.GameControllerState.SetState(Controllers.GameController.State.Initialization);
					break;
				case State.Playing:
					this.GameControllerState.SetState(Controllers.GameController.State.TurnStart);
					break;
				case State.Outro:
					break;
			}
		}
		
		protected override void OutroState(State state) {
			//Not Used
		}

		protected override void UpdateState(State state) {
			switch (state) {
				case State.None:
					SetState(State.Initialization);
					break;
				case State.Initialization:
					if (this.GameControllerState.CurrentState == Controllers.GameController.State.InitializationComplete) {
						SetState(State.Playing);
					}
					break;
				case State.Playing:
					if (Keyboard.current.escapeKey.isPressed) {
						SetState(State.Outro);
					}
					break;
				case State.Outro:
					break;
			}
		}

		#endregion
		
	}
}