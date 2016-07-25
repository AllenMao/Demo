using UnityEngine;
using System.Collections;

public class SelectionGUI : MonoBehaviour 
{
	public GUISkin mySkin;
	private Rect backPosition = new Rect(40, 500, 100, 100);
	private Rect level1_Position = new Rect(125, 60, 150, 150);
	private Rect level2_Position = new Rect(325, 60, 150, 150);
	private Rect level3_Position = new Rect(525, 60, 150, 150);
	
	private Rect level4_Position = new Rect(125, 260, 150, 150);
	private Rect level5_Position = new Rect(325, 260, 150, 150);
	private Rect level6_Position = new Rect(525, 260, 150, 150);
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnGUI()
	{
		GUI.skin = mySkin;
		if(GUI.Button(level1_Position, "", GUI.skin.GetStyle("Level1Button")))
			Application.LoadLevel(3);
		if(GUI.Button(level2_Position, "", GUI.skin.GetStyle("Level2Button")))
			Application.LoadLevel(3);
		if(GUI.Button(level3_Position, "", GUI.skin.GetStyle("Level3Button")))
			Application.LoadLevel(3);
		if(GUI.Button(level4_Position, "", GUI.skin.GetStyle("Level4Button")))
			Application.LoadLevel(3);
		if(GUI.Button(level5_Position, "", GUI.skin.GetStyle("Level5Button")))
			Application.LoadLevel(3);
		if(GUI.Button(level6_Position, "", GUI.skin.GetStyle("Level6Button")))
			Application.LoadLevel(3);
		if(GUI.Button(backPosition, "", GUI.skin.GetStyle("BackButton")))
			Application.LoadLevel(1);
	}
}
