using UnityEngine;
using System.Collections;

public class DigtalDisplay : MonoBehaviour 
{
	public string myStringScore;
	public float x = 85;
	public float y = 19;
	public float scale = 0.35f;
	public Color myColor;
	
	public Texture2D[] myNumber = new Texture2D[10];
	//private int index = 0;
	private int width = 50;
	private int height = 72;

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
		if(myStringScore == null) return;
		
		GUI.color = myColor;
		
		for(int i = 0; i < myStringScore.Length; ++i)
		{
			GUI.DrawTexture(new Rect(x+i*scale*width, y, scale*width, scale*height), myNumber[myStringScore[i]-'0']);
		}
	}

}
