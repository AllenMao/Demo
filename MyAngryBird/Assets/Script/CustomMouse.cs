using UnityEngine;
using System.Collections;

public class CustomMouse : MonoBehaviour
{
	public Texture2D myMouse;
	public Texture2D myMouseClik;
	public float MyMouse_W,MyMouse_H;
	
	private bool showClikMouse = false;
	// Use this for initialization
	void Start () 
	{
		Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButton(0))
			showClikMouse = true;
		else
			showClikMouse = false;
	}
	
	void OnGUI()
	{
		if(showClikMouse)
			GUI.DrawTexture(new Rect(Input.mousePosition.x-MyMouse_W/2.0f,
				Screen.height-Input.mousePosition.y - MyMouse_H/2.0f, MyMouse_W, MyMouse_H),
				myMouseClik);
		else
			GUI.DrawTexture(new Rect(Input.mousePosition.x-MyMouse_W/2.0f,
				Screen.height-Input.mousePosition.y - MyMouse_H/2.0f, MyMouse_W, MyMouse_H),
				myMouse);
		GUI.depth = -1;//小心再新窗口下面
	}
}
