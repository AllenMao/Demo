using UnityEngine;
using System.Collections;

public class SlingShot : MonoBehaviour 
{
	public GameObject bird;
	public AudioClip SlingShotSound;
	public  AudioClip flyingSound;
	public  Color myColor;
	
	private bool isClicked = false;
	private GameObject shoted;
	private Vector3 lastPosition;
	private Vector3 direction;
	private float magnitude;
	
	public static bool isDrawing = false;
	public static bool isJump = false;
	public static GameObject myBird;
	public static int birdNum;
	
	
	private LineRenderer linerenderer;
	//private int index = 0;
	private Vector3 position;
	
	// Use this for initialization//添加对象  注意js中的不同
	void Start () 
	{
		birdNum = -1;
		linerenderer = gameObject.AddComponent("LineRenderer") as LineRenderer;
		linerenderer.material.color = myColor;
		linerenderer.SetWidth(0.1f, 0.1f);
		linerenderer.SetVertexCount(3);
	}
	
	// Update is called once per frame
	void Update () 
	{
		bool isMouseDown = Input.GetMouseButton(0);
		if(!isClicked)
		{
			if(isMouseDown)
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if(Physics.Raycast(ray,out hit, 10.0f))//Collider.Raycast js中
				{
					isClicked = true;
					if(birdNum <= 1)
					{
						shoted = Instantiate(bird, transform.position, Quaternion.identity) as GameObject;
						Physics.IgnoreCollision(shoted.collider, collider);//忽略SlingShot的collider与shoted之间的碰撞
						shoted.rigidbody.isKinematic =  true;
						
						myBird = GameObject.FindWithTag("Bird");
						myBird.SetActive(false);
					}
				}
			}
		}
		else if(isMouseDown)
		{
			Vector3 lastDirection = direction;
			//屏幕点转为世界坐标点
			lastPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.8f));
			direction = transform.position - lastPosition;
			direction.Normalize();//单位化
			magnitude = Mathf.Min(Vector3.Distance(transform.position, lastPosition), 1.5f);
			shoted.transform.position = transform.position + direction * (-magnitude);
			
			linerenderer.enabled = true;
			linerenderer.SetPosition(0, new Vector3(-4.16f, -1.53f, -9.3f));
			linerenderer.SetPosition(1, shoted.transform.position);
			linerenderer.SetPosition(2, new Vector3(-4.58f, -1.65f, -9.3f));
			
			if(direction != lastDirection)
				StartCoroutine(PlaySound(SlingShotSound));//与js区别，如何调用IEnumerator PlaySound(AudioClip soundName)
		}
		else
		{
			birdNum++;
			if(birdNum <= 2)
			{
				GameObject birdObject = GameObject.FindWithTag("BirdClone");
				
				if(birdObject != null)
					Destroy(birdObject, 1);
				
				linerenderer.enabled = false;
				
				shoted.collider.isTrigger = false;
				shoted.rigidbody.isKinematic = false;
				shoted.rigidbody.velocity = direction * 7.0f * magnitude;
				shoted.rigidbody.useGravity = true;
				
				if(audio.isPlaying)
				{
					audio.Stop();
				}
				StartCoroutine(PlaySound(flyingSound));
				shoted.tag = "BirdClone";
				isClicked = false;
				
				isDrawing = true;
				isJump = true;
				
			}
		}
	}
	
	IEnumerator PlaySound(AudioClip soundName)
	{
		if(!audio.isPlaying)
		{
			audio.clip = soundName;
			audio.Play();
			//Debug.Log("SB");
			yield return new WaitForSeconds(audio.clip.length);
			//yield WaitForSeconds();
		}
	}
}
