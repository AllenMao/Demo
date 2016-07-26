using UnityEngine;
using System.Collections;

public class end : MonoBehaviour 
{

	public GUISkin mySkin;
	private bool isSound1Button = false;
	private bool isSound2Button = true;
	private AudioSource sound;
	// Use this for initialization
	void Start () 
	{
		sound = gameObject.GetComponent("AudioSource") as  AudioSource;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnGUI()
	{
		GUI.skin = mySkin;
		if(GUI.Button(new Rect(180,215,220,66), "", GUI.skin.GetStyle("MainButton")))
		{
			Application.LoadLevel(0);
		}
		if(GUI.Button(new Rect(160,286,260,66), "", GUI.skin.GetStyle("MoreButton")))
		{
			Application.OpenURL("http://www.hao123.com/");
		}
		if(GUI.Button(new Rect(180,357,220,66), "", GUI.skin.GetStyle("CreditButton")))
		{
			
		}
		
		if(isSound1Button)
		{
			if(GUI.Button(new Rect(25,430,20,31), "", GUI.skin.GetStyle("Sound1Button")))
			{
				sound.Play();
				isSound1Button = false;
				isSound2Button = true;
			}
		}
		
		if(isSound2Button)
		{
			if(GUI.Button(new Rect(25,430,37,31), "", GUI.skin.GetStyle("Sound2Button")))
			{
				sound.Stop();
				isSound1Button = true;
				isSound2Button = false;
			}
		}
	}
}
