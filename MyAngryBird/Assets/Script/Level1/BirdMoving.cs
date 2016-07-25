using UnityEngine;
using System.Collections;

public class BirdMoving : MonoBehaviour 
{
	private bool isGround = false;
	private float randomNumber;
	
	public float seconds;
	// Use this for initialization
	void Start () 
	{
		InvokeRepeating("Move", 2, seconds);//2s后执行，每隔（seconds）调用
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!isGround)
		{
			if(randomNumber>0.5)
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 
										transform.localEulerAngles.y, transform.localEulerAngles.z + Time.deltaTime * 400);
			else
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 
										transform.localEulerAngles.y, transform.localEulerAngles.z - Time.deltaTime * 400);
		}
		
		if(SlingShot.isJump)
		{
			StartCoroutine(waiTime());
		}
	}
	void Move()
	{
		transform.rigidbody.velocity= new Vector3(0, 2.0f, 0);
		isGround = false;
		randomNumber = Random.Range(0, 1.0f);
	}
	
	void OnCollisionEnter( Collision collision)
	{
		isGround = true;
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 
										transform.localEulerAngles.y, 0);

	}
	
	IEnumerator waiTime()
	{
		yield return new WaitForSeconds(1.0f);
		if(gameObject.name == "Bird1" && SlingShot.birdNum == 0)
		{
			transform.animation.Play();
			transform.gameObject.collider.isTrigger = true;
			
			SlingShot.isJump = false;
			
			yield return new WaitForSeconds(1.0f);
			SlingShot.myBird.SetActive(true);
			//SlingShot.myBird.active = true;
			SlingShot.myBird.transform.animation.Stop();
			
			Destroy(gameObject);//产生clone 去掉Bird
		}
		
		if(gameObject.name == "Bird2" && SlingShot.birdNum == 1)
		{
			transform.animation.Play();
			transform.gameObject.collider.isTrigger = true;
			SlingShot.isJump = false;
			yield return new WaitForSeconds(1.0f);
			SlingShot.myBird.SetActive(true);
			SlingShot.myBird.transform.animation.Stop();
			Destroy(gameObject);//产生clone 去掉Bird
		}
	}
}
