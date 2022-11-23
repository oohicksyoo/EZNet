using System.Collections.Generic;
using System.IO;
using System.Linq;
using Project.ProjectSettings;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Project.Editor {
	public class ProjectBuilderWindow : OdinEditorWindow {

		#region Private Static Properties
		
		private static PlatformType PlatformType {
			get;
			set;
		}

		private static bool RPG_DEBUG {
			get;
			set;
		}
		
		private static bool RPG_FORCE_STEAM {
			get;
			set;
		}

		#endregion
		

		#region Private Static Functions

		[MenuItem("RPG/Project Builder")]
		private static void OpenWindow() {
			SyncEditor();
			GetWindow<ProjectBuilderWindow>().Show();
		}
		
		[UnityEditor.Callbacks.DidReloadScripts]
		private static void SyncEditor() {
			PlatformType = (PlatformType)EditorUserBuildSettings.activeBuildTarget;
			RPG_DEBUG = EntryIsInDefineSymbols("RPG_DEBUG");
			RPG_FORCE_STEAM = EntryIsInDefineSymbols("RPG_FORCE_STEAM");
		}

		private static List<string> GetDefineSymbols() {
			string defineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
			return defineSymbols.Split(new char[] {';'}, System.StringSplitOptions.RemoveEmptyEntries).ToList();
		}

		private static bool EntryIsInDefineSymbols(string symbol) {
			var defineSymbols = GetDefineSymbols();
			return defineSymbols.Contains(symbol);
		}

		[OnInspectorGUI]
		private void DrawWindow() {
			EditorGUILayout.Space();
			
			//Select Platform
			var platformType = EnumSelector<PlatformType>.DrawEnumField(new GUIContent("Platform Type"), PlatformType);
			if (platformType != PlatformType) {
				EditorUserBuildSettings.SwitchActiveBuildTarget(platformType.GetBuildTargetGroup(), (BuildTarget) platformType);
				PlatformType = platformType;
			}
			
			//Added Define Symbols
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Optional Defines");
			var rpgDebug = EditorGUILayout.Toggle("RPG_DEBUG", RPG_DEBUG);
			if (rpgDebug != RPG_DEBUG) {
				var symbol = "RPG_DEBUG";
				var defineSymbols = RemoveFromDefines(symbol);
				if (rpgDebug) { defineSymbols.Add(symbol); }
				PlayerSettings.SetScriptingDefineSymbolsForGroup (EditorUserBuildSettings.selectedBuildTargetGroup, string.Join(";", defineSymbols.ToArray()));
				RPG_DEBUG = rpgDebug;
			}
			
			var rpgForceSteam = EditorGUILayout.Toggle("RPG_FORCE_STEAM", RPG_FORCE_STEAM);
			if (rpgForceSteam != RPG_FORCE_STEAM) {
				var symbol = "RPG_FORCE_STEAM";
				var defineSymbols = RemoveFromDefines(symbol);
				if (rpgForceSteam) { defineSymbols.Add(symbol); }
				PlayerSettings.SetScriptingDefineSymbolsForGroup (EditorUserBuildSettings.selectedBuildTargetGroup, string.Join(";", defineSymbols.ToArray()));
				RPG_FORCE_STEAM = rpgForceSteam;
			}

			//Define Symbols
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Current Define Symbols");
			foreach (var symbol in GetDefineSymbols()) {
				EditorGUILayout.LabelField($"{symbol}");
			}
			
			//End Buttons
			EditorGUILayout.Space();
			EditorGUILayout.LabelField($"Version {Application.version}");
			if (PlatformType == PlatformType.Android) {
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField($"Android Bundle Version {PlayerSettings.Android.bundleVersionCode}");
				if (GUILayout.Button("+")) {
					PlayerSettings.Android.bundleVersionCode++;
				}
				EditorGUILayout.EndHorizontal();
			}

			if (GUILayout.Button("Start Build")) {
				var scenes = EditorBuildSettings.scenes;
				var path = $"{Application.dataPath}/../../Builds/{PlatformType}/{Application.version}/";
				Directory.CreateDirectory(path);

				BuildPipeline.BuildPlayer(scenes, $"{path}{PlatformType.ToAppExe()}", (BuildTarget) PlatformType, BuildOptions.None);
				System.Diagnostics.Process.Start("explorer.exe", path);
			}
		}

		private List<string> RemoveFromDefines(string defineSymbol) {
			var defineSymbols = GetDefineSymbols();
			for (int i = defineSymbols.Count - 1; i >= 0; i--) {
				if (defineSymbols[i] == defineSymbol) {
					defineSymbols.RemoveAt(i);
				}
			}

			return defineSymbols;
		}

		#endregion
	}
}