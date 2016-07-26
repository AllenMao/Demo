using UnityEngine;
using System.Collections;

public class XDisplay : MonoBehaviour 
{
	public float x = 88;
	public float y = 20;
	
	public float scale = 0;
	public Color myColor ;
	
	public Texture xTexture;
	
	private int width = 33;
	private int height = 41;
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
		GUI.color = myColor;
		GUI.DrawTexture(new Rect(x, y, scale*width, scale*height), xTexture);
		
	}
}
