using System;
using Assets.Project.Code.Scriptable;
using Project.Controllers;
using Project.Managers;
using Project.Utility.Attributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Project.Gameplay.Board {
	[RequireComponent(typeof(SpriteRenderer))]
	public class GemLocation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler {


		#region Types

		public enum GemType {
			None, 
			Red,
			Green,
			Blue,
			Blocker,
			Poison,
			Rainbow
		}

		#endregion
		

		#region Serialized Fields

		[Header("Neighbours")]
		[SerializeField]
		private GemLocation upGemLocation;
		
		[SerializeField]
		private GemLocation rightGemLocation;
		
		[SerializeField]
		private GemLocation downGemLocation;
		
		[SerializeField]
		private GemLocation leftGemLocation;

		#endregion


		#region Non Serialized Fields

		[NonSerialized]
		[SerializeField]
		private SpriteRenderer spriteRenderer;

		#endregion


		#region Properties

		[field: SerializeField]
		[field: GreyOut]
		public bool IsHighlighted {
			get;
			private set;
		}

		[field: SerializeField]
		[field: GreyOut]
		public GemType Type {
			get;
			private set;
		}

		private SpriteRenderer SpriteRenderer {
			get {
				return spriteRenderer ??= GetComponent<SpriteRenderer>();
			}
		}

		#endregion


		#region MonoBehaviour

		public void OnDrawGizmosSelected() {
			Gizmos.color = Color.red;
			if (upGemLocation != null) {
				Gizmos.DrawLine(transform.position, upGemLocation.transform.position);
			}
			
			if (rightGemLocation != null) {
				Gizmos.DrawLine(transform.position, rightGemLocation.transform.position);
			}
			
			if (downGemLocation != null) {
				Gizmos.DrawLine(transform.position, downGemLocation.transform.position);
			}
			
			if (leftGemLocation != null) {
				Gizmos.DrawLine(transform.position, leftGemLocation.transform.position);
			}
		}

		#endregion


		#region Public Methods

		public void SetState(GemType type) {
			var gemAsset = GemAssetScriptable.Instance.GetAssetByType(type);
			if (gemAsset != null && this.SpriteRenderer != null) {
				this.Type = gemAsset.Type;
				this.SpriteRenderer.sprite = (this.IsHighlighted) ? gemAsset.Highlighted : gemAsset.Normal;
			}
		}

		public void TurnOff() {
			this.IsHighlighted = false;
			SetState(this.Type);
		}

		public void TurnOn() {
			this.IsHighlighted = true;
			SetState(this.Type);
		}

		public bool IsANeighbour(GemLocation gem) {
			return upGemLocation == gem || rightGemLocation == gem || downGemLocation == gem || leftGemLocation == gem;
		}

		/// <summary>
		/// Returns if the gem is the same type or a special type that can be used 
		/// </summary>
		public bool IsOfSameType(GemLocation gem) {
			return this.Type == gem.Type;
		}

		#endregion


		#region IPointer Interfaces

		public void OnPointerDown(PointerEventData eventData) {
			ApplicationManager.Instance.CurrentLevelController.GameController.AddGemToChain(GameController.PlayerType.Player, this);
		}
		
		public void OnPointerUp(PointerEventData eventData) {
			ApplicationManager.Instance.CurrentLevelController.GameController.EndGemChain(GameController.PlayerType.Player);
		}

		public void OnPointerEnter(PointerEventData eventData) {
			if (Mouse.current.leftButton.isPressed) {
				OnPointerDown(eventData);
			}
		}
		
		public void OnPointerExit(PointerEventData eventData) {
			
		}

		#endregion
		
	}
}