using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Gameplay.Board;
using Project.Utility.Abstract;
using Project.Utility.Attributes;
using UnityEngine;

namespace Project.Controllers {
	public class GameController : AbstractStateBehaviour<GameController.State> {


		#region Types

		public enum State {
			None = 0,
			Waiting,
			Initialization,
			InitializationComplete,
			TurnStart,
			TurnInProgress,
			TurnCalculations,
			TurnOver,
			Complete,
		}

		public enum PlayerType {
			Player,
			AI
		}

		#endregion


		#region Serialized Fields

		[SerializeField]
		private GameBoard gameBoard;

		#endregion


		#region Non Serialized Fields

		[NonSerialized]
		private List<GemLocation> chain;

		#endregion


		#region Properties

		[field: SerializeField]
		[field: GreyOut]
		private PlayerType CurrentPlayer {
			get;
			set;
		}
		
		private List<GemLocation> Chain {
			get {
				return chain ??= new List<GemLocation>();
			}
		}

		#endregion


		#region MonoBehaviour

		public void Awake() {
			ApplyState(State.None);
		}

		#endregion
		
		
		#region AbstractStateBehaviour

		protected override void IntroState(State state) {
			switch (state) {
				case State.None:
					break;
				case State.Waiting:
					break;
				case State.Initialization:
					gameBoard.FillBoard();
					this.CurrentPlayer = PlayerType.Player; //TODO: Maybe this is coin flip?
					SetState(State.InitializationComplete);
					break;
				case State.InitializationComplete:
					break;
				case State.TurnStart:
					//TODO: Initialize anything in the turn
					SetState(State.TurnInProgress);
					break;
				case State.TurnInProgress:
					if (this.CurrentPlayer == PlayerType.AI) {
						AITurn();
					}
					break;
				case State.TurnCalculations:
					//TODO: Run chain calculations here
					SetState(State.TurnOver);
					break;
				case State.TurnOver:
					this.CurrentPlayer = (this.CurrentPlayer == PlayerType.Player) ? PlayerType.AI : PlayerType.Player;
					SetState(State.TurnStart);
					//TODO: Check if the game is over
					break;
				case State.Complete:
					break;
			}
		}
		
		protected override void OutroState(State state) {
			switch (state) {
				case State.None:
					break;
				case State.Waiting:
					break;
				case State.Initialization:
					break;
				case State.InitializationComplete:
					break;
				case State.TurnStart:
					break;
				case State.TurnInProgress:
					break;
				case State.TurnCalculations:
					ClearChain();
					break;
				case State.TurnOver:
					break;
				case State.Complete:
					break;
			}
		}

		protected override void UpdateState(State state) {
			switch (state) {
				case State.None:
					SetState(State.Waiting);
					break;
				case State.Waiting:
					break;
				case State.Initialization:
					break;
				case State.InitializationComplete:
					break;
				case State.TurnStart:
					break;
				case State.TurnInProgress:
					break;
				case State.TurnCalculations:
					break;
				case State.TurnOver:
					break;
				case State.Complete:
					break;
			}
		}

		#endregion


		#region Public Methods

		public void AddGemToChain(PlayerType player, GemLocation gemLocation) {
			if (this.CurrentState != State.TurnInProgress || this.CurrentPlayer != player || gemLocation.Type == GemLocation.GemType.None) {
				return;
			}
			
			if (this.Chain.Count > 0) {
				GemLocation gem = this.Chain[this.Chain.Count - 1];
				if (this.Chain.All(x => x != gemLocation) && gem.IsANeighbour(gemLocation) && gem.IsOfSameType(gemLocation)) {
					this.Chain.Add(gemLocation);
					gemLocation.TurnOn();
				}
			} else {
				this.Chain.Add(gemLocation);
				gemLocation.TurnOn();
			}
		}

		public void EndGemChain(PlayerType player) {
			if (this.CurrentState != State.TurnInProgress || this.CurrentPlayer != player) {
				return;
			}

			foreach (var gemLocation in this.Chain) {
				gemLocation.TurnOff();

				if (this.Chain.Count >= 3) {
					gemLocation.SetState(GemLocation.GemType.None);
				}
			}

			if (this.Chain.Count >= 3) {
				SetState(State.TurnCalculations);
			} else {
				ClearChain();
			}
		}

		#endregion


		#region Private Methods

		private void ClearChain() {
			this.Chain.Clear();
		}

		private void AITurn() {
			StartCoroutine(RunAITurn());
		}

		//TODO: Move this to a generalized AI Class so different AI can attack the board in different ways
		private IEnumerator RunAITurn() {
			for (float i = 0; i < 3; i += Time.deltaTime) {
				yield return null;
			}
			
			
			//TODO: Ideally AI would call EndGemChain() to start calculations and attacking
			SetState(State.TurnCalculations);
		}

		#endregion
		
	}
}