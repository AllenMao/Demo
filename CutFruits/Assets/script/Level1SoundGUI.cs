using UnityEngine;
using System.Collections;

public class Level1SoundGUI : MonoBehaviour 
{
	public GUISkin mySkin;
	
	private bool isSound1Button = false;
	private bool isSound2Button = true;
	private AudioSource sound;
	// Use this for initialization
	void Start () 
	{
		sound = gameObject.GetComponent("AudioSource") as AudioSource;
	}
	
	void OnGUI()
	{
		GUI.skin = mySkin;
		if(isSound1Button)
		{
			if(GUI.Button(new Rect(12,430,20,31),"",GUI.skin.GetStyle("Sound1Button")))
			{
				sound.Play();
				isSound1Button = false;
				isSound2Button = true;
			}	
		}
		
		if(isSound2Button)
		{
			if(GUI.Button(new Rect(12,430,37,31),"",GUI.skin.GetStyle("Sound2Button")))
			{
				sound.Stop();
				isSound1Button = true;
				isSound2Button = false;
			}	
		}
	}
	// Update is called once per frame
	void Update () 
	{
	
	}
}
