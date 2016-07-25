using UnityEngine;
using System.Collections;

public class WoodController : MonoBehaviour 
{
	public GameObject digit100;
	
	private GameObject myDisplay;
	private int index = 1;//index控制木头只能碰撞一次
	private float y;
	// Use this for initialization
	void Start ()
	{
		y = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Mathf.Abs(transform.position.y - y) >= 0.2f )
		{
			if(myDisplay == null && index ==1)
			{
				ScoreController.myScore += 100;
				myDisplay = Instantiate(digit100, transform.position, Quaternion.identity) as GameObject;
				
				Destroy(myDisplay, 1.0f);
				++ index;
			}
		}
	}
	
}
