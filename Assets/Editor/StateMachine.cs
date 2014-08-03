using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class StateMachine : ScriptableObject
{
	public List<State> states = new List<State> ();

	public void AddState (string name)
	{
		states.Add (new State (name));
	}

	public void OnGUI ()
	{
		for (int i = 0; i < states.Count; i++) {
			var state = states [i];

			state.windowRect = GUI.Window (i, state.windowRect, (id) => {
				EditorGUILayout.LabelField ("٩(๑❛ᴗ❛๑)۶");

				GUI.DragWindow ();
			}, state.name, Style.state);
		}

	}
}