using UnityEngine;
using System.Collections;

public class Level1ScoreGUI : MonoBehaviour 
{
	public GUISkin mySkin;
	public Texture xTexture;
	public Texture kiwiTexture;
	public Color myColor;
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
		GUI.color = myColor;
		GUI.DrawTexture(new Rect(60,10,33,41),xTexture);
		GUI.DrawTexture(new Rect(5,5,57,53),kiwiTexture);
	}
}
