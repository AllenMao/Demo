using UnityEngine;
using System.Collections;

public class LifeController : MonoBehaviour 
{
	public static int life;
	
	public Color color;
	
	public GameObject X1;
	public GameObject X2;
	public GameObject X3;
	public GameObject X4;
	public GameObject X5;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(life)
		{
		case 5:
			(X1.transform.GetComponent("XDisplay") as XDisplay).myColor = color;
			break;
		case 4:
			(X2.transform.GetComponent("XDisplay") as XDisplay).myColor = color;
			break;
		case 3:
			(X3.transform.GetComponent("XDisplay") as XDisplay).myColor = color;
			break;
		case 2:
			(X4.transform.GetComponent("XDisplay") as XDisplay).myColor = color;
			break;
		case 1:
			(X5.transform.GetComponent("XDisplay") as XDisplay).myColor = color;
			break;
		}
	}
}
