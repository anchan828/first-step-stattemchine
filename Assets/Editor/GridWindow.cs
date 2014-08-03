using UnityEngine;
using UnityEditor;

public class GridWindow : EditorWindow
{

	StateMachine stateMachine;
	Vector2 scrollPosition = Vector2.zero;
	float magnification = 2f;

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
		var viewPosition = new Rect (0, 0, position.width, position.height);
		var viewRect = new Rect (0, 0, position.width * magnification, position.height * magnification);
		scrollPosition = GUI.BeginScrollView (viewPosition, scrollPosition, viewRect);

		if (Event.current.type == EventType.Repaint)
			Style.backgorund.Draw (new Rect (0, 0, position.width * magnification, position.height * magnification),
			                           false, false, false, false);

		DrawGrid (12);

		BeginWindows ();
		stateMachine.OnGUI ();
		EndWindows ();

		GUI.EndScrollView ();
		Toolbar.OnGUI (stateMachine);
	}

	private void DrawGrid (float gridSize)
	{
		DrawGridLines (gridSize, EditorGUIUtility.isProSkin ? new Color32 (32, 32, 32, 255) : new Color32 (60, 60, 60, 255));
		DrawGridLines (gridSize * 10, Color.black);
	}

	private void DrawGridLines (float gridSize, Color gridColor)
	{
		float xMin = 0;
		float yMin = 0;
		float xMax = position.width * magnification;
		float yMax = position.height * magnification;

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
