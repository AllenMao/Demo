using UnityEngine;
using System.Collections;

public class fruitCreate : MonoBehaviour 
{
	public GameObject[] fruit = new GameObject[6];
	
	public int index;
	
	private GameObject myApple;
	// Use this for initialization
	void Start () 
	{
		Physics.gravity = new Vector3(0,-20,0);
		InvokeRepeating("Move",0,2.5f);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void Move()
	{
		float x = Random.Range(-7,7);
		index = Random.Range(0,6);
		myApple = Instantiate(fruit[index],new Vector3(x,-7,-9),Quaternion.identity) as GameObject;
		if(x > 0)
			myApple.transform.rigidbody.velocity = new Vector3(Random.Range(-5,0),21,0);
		else
			myApple.transform.rigidbody.velocity = new Vector3(Random.Range(0,5),21,0);
		
		myApple.transform.localEulerAngles = new Vector3(myApple.transform.localEulerAngles.x,
								myApple.transform.localEulerAngles.y,
								Random.Range(-120,120));
	}
}
