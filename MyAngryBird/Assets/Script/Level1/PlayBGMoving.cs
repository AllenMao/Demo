using UnityEngine;
using System.Collections;

public class PlayBGMoving : MonoBehaviour 
{
	public float speed = 0.2f;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(Vector3.left * speed * Time.deltaTime);
		if(transform.position.x <= 4.389f)
			transform.position = new Vector3(6.3849f, transform.position.y, transform.position.z);
	}
}
