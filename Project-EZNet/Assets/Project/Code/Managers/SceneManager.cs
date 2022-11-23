﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Controllers;
using Project.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Managers {
	public class SceneManager : Singleton<SceneManager> {
		
		#region Serialized Fields

		[SerializeField]
		private PreloaderController preloaderController;

		#endregion


		#region Non Serialized Fields

		[NonSerialized]
		private List<string> loadedScenes;

		[NonSerialized]
		private List<SceneLoadingData> scenesLoading;

		#endregion


		#region Properties

		private List<string> LoadedScenes {
			get {
				return loadedScenes = loadedScenes ?? new List<string>();
			}
		}
		
		private List<SceneLoadingData> ScenesLoading {
			get {
				return scenesLoading = scenesLoading ?? new List<SceneLoadingData>();
			}
		}

		#endregion


		#region MonoBehaviour

		public void Update() {
			for (int i = this.ScenesLoading.Count - 1; i >= 0; i--) {
				if (this.ScenesLoading[i] == null) {
					this.ScenesLoading.RemoveAt(i);
					continue;
				}

				if (this.ScenesLoading[i].asyncOperation.isDone) {
					this.ScenesLoading[i].asyncOperation.allowSceneActivation = true;
					this.ScenesLoading[i].sceneLoadedAction(this.ScenesLoading[i].sceneName);
					this.LoadedScenes.Add(this.ScenesLoading[i].sceneName);
					this.ScenesLoading.RemoveAt(i);
				}
			}
		}

		#endregion
		

		#region Public Functions

		public void LoadScene(string sceneName, Action<string> sceneLoadedAction, bool showPreloader = false, bool fadePreloaderTransition = false) {
			bool isAlreadyLoaded = this.LoadedScenes.Any(x => x == sceneName);

			if (isAlreadyLoaded) {
				Debug.LogWarning($"[ Scene Manager ] Scene (${sceneName}) is already loaded.");
				return;
			}

			StartCoroutine(RunLoadScene(sceneName, sceneLoadedAction, showPreloader, fadePreloaderTransition));
		}

		private IEnumerator RunLoadScene(string sceneName, Action<string> sceneLoadedAction, bool showPreloader, bool fadePreloaderTransition) {
			if (showPreloader) {
				yield return preloaderController.ShowPreloader(fadePreloaderTransition);
			}
			
			//Start scene loading
			SceneLoadingData sceneLoadingData = new SceneLoadingData();
			sceneLoadingData.sceneName = sceneName;
			sceneLoadingData.asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
			sceneLoadingData.sceneLoadedAction = sceneLoadedAction;
			this.ScenesLoading.Add(sceneLoadingData);
		}

		public void UnloadScene(string sceneName) {
			if (this.LoadedScenes.Any(x => x == sceneName)) {
				UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
				this.LoadedScenes.Remove(sceneName);
			}
		}

		//TODO: Method calls for SceneControllers for will transitionin and didtransitionin
		public void ShowPreloader(bool fadeTransition) {
			StartCoroutine(preloaderController.ShowPreloader(fadeTransition));
		}

		public void HidePreloader(bool fadeTransition) {
			StartCoroutine(preloaderController.HidePreloader(fadeTransition));
		}

		public IEnumerator RunCyclePreloader(Action transitionAction) {
			yield return preloaderController.ShowPreloader(true);
			transitionAction();
			for (float i = 0; i < 0.5f; i += Time.deltaTime) {
				yield return null;
			}
			yield return preloaderController.HidePreloader(true);
		}

		#endregion


		#region Level Loading Data

		private class SceneLoadingData {
			public string sceneName;
			public AsyncOperation asyncOperation;
			public Action<string> sceneLoadedAction;
		}

		#endregion

		
	}
}