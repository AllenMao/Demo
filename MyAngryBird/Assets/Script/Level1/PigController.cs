using UnityEngine;
using System.Collections;

public class PigController : MonoBehaviour 
{
	public GameObject digit10000;
	
	private GameObject myDisplay;
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
			if(myDisplay == null)
			{
				ScoreController.myScore += 10000;
				myDisplay = Instantiate(digit10000, transform.position, Quaternion.identity) as GameObject;
				Destroy(myDisplay, 1.0f);
			}
			Destroy(gameObject, 0.2f);
		}
	}
}
