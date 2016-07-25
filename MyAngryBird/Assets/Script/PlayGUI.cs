using UnityEngine;
using System.Collections;

public class PlayGUI : MonoBehaviour 
{
	public GUISkin mySkin;
	public Texture2D quit;
	public Texture2D okButton;
	public Texture2D closeButton;
	
	private Rect closeButtonPosition = new Rect(28.52f, 93.1f, 115.0f, 115.0f);
	private Rect okButtonPosition = new Rect(280.68f, 93.1f, 115.0f, 115.0f);
	
	private Rect myWindow = new Rect(400-250, 300-60, 431, 215);//对话框窗口
	private bool showWindow = false;
//	private bool isQuit =false;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}
	
	void OnGUI()
	{
		GUI.skin = mySkin;
		if(showWindow)
		{//注册窗口
			myWindow = GUI.Window(0, myWindow, DoMyWindow, "");
		}
		else
		{
			if(GUI.Button(new Rect(Screen.width/2.0f-221, Screen.height/2.0f-100, 
							442 * 0.8f, 230 * 0.8f), "", GUI.skin.GetStyle("PlayButton")))
				Application.LoadLevel(2);
			if(GUI.Button(new Rect(Screen.width-120, Screen.height-120, 
							110.0f, 110.0f), "", GUI.skin.GetStyle("HomeButton")))
				showWindow = true;
		}
	}
	
	void DoMyWindow(int windowID)
	{
		GUI.DrawTexture(new Rect(0, 0, 431, 215), quit);
		if(GUI.Button(closeButtonPosition, closeButton))
			showWindow = false;
		if(GUI.Button(okButtonPosition, okButton))
			Application.Quit();
	}
}
