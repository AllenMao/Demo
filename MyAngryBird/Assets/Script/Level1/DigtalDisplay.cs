using UnityEngine;
using System.Collections;

public class DigtalDisplay : MonoBehaviour 
{
	public string myStringScore;
	public float x;
	public float y;
	public float scale;
	public Color myColor;
	
	public Texture2D[] myNumber = new Texture2D[10];
	
//	private int index = 0;
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
		GUI.color = myColor;
		Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
		
		x = screenPos.x - 70;
		y = Screen.height - screenPos.y - 20;
		
		for(int i = 0; i < myStringScore.Length; ++i)
		{
			GUI.DrawTexture(new Rect(x+i*scale*width, y, scale*width, scale*height), myNumber[myStringScore[i]-'0']);
		}
	}
}
