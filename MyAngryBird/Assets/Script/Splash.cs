using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour 
{
	public int timeLength;
	public int nextLevel;
	private float myTime;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		myTime += Time.deltaTime;
		if(myTime > timeLength)
		{
			Application.LoadLevel(nextLevel);
		}
	}
}
