using UnityEngine;

[System.Serializable]
public class State
{
	public string name;
	public Rect windowRect;
	
	public State (string name)
	{
		this.name = name;
		this.windowRect = new Rect (100, 200, 100, 50);
	}
}