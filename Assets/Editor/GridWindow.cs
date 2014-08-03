using UnityEngine;
using UnityEditor;

public class GridWindow : EditorWindow
{

	StateMachine stateMachine;

	[MenuItem("Window/GridWindow")]
	static void Open ()
	{
		GetWindow<GridWindow> ();
	}

	void OnEnable ()
	{
		var path = "Assets/Editor/StateMachine.asset";
		stateMachine = AssetDatabase.LoadAssetAtPath (path, typeof(StateMachine)) as StateMachine;

		if (stateMachine == null) {
			stateMachine = CreateInstance<StateMachine> ();
			AssetDatabase.CreateAsset (stateMachine, path);
		}
		Repaint ();
	}

	void OnGUI ()
	{
		if (Event.current.type == EventType.Repaint)
			Style.backgorund.Draw (new Rect (0, 0, position.width, position.height),
			                           false, false, false, false);

		DrawGrid (12);

		Toolbar.OnGUI (stateMachine);

		BeginWindows ();
		stateMachine.OnGUI ();
		EndWindows ();
	}

	private void DrawGrid (float gridSize)
	{
		DrawGridLines (gridSize, EditorGUIUtility.isProSkin ? new Color32 (32, 32, 32, 255) : new Color32 (60, 60, 60, 255));
		DrawGridLines (gridSize * 10, Color.black);
	}

	private void DrawGridLines (float gridSize, Color gridColor)
	{
		float xMax = position.width * 5, xMin = 0, yMax = position.height * 5, yMin = 0;
		Handles.color = gridColor;
		float x = xMin - xMin % gridSize;
		while (x < xMax) {
			Handles.DrawLine (new Vector2 (x, yMin), new Vector2 (x, yMax));
			x += gridSize;
		}
		float y = yMin - yMin % gridSize;
		while (y < yMax) {
			Handles.DrawLine (new Vector3 (xMin, y), new Vector3 (xMax, y));
			y += gridSize;
		}
	}
}
