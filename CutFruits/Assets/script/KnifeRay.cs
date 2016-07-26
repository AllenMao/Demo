using UnityEngine;
using System.Collections;

public class KnifeRay : MonoBehaviour 
{
	public Color myColor;
	public GameObject myRay;
	public AudioClip knifeSound;
	
	public bool isHit = false;
	public bool isRay = false;
	public Vector3 rayPosition;
	public GameObject firstFruit;
	public GameObject secondFruit; 
	
	public GameObject splash;
	public GameObject splashH;
	public GameObject splashV;
	
	private GameObject myFirstFruit;
	private GameObject mySecondFruit;
	
	private GameObject mySplash;
	private GameObject mySplashH;
	private GameObject mySplashV;
	
	public Vector3 firstPosition;
	public Vector3 secondPosition;
	public Vector3 middlePosition;
	
	private bool isClicked = false;
	
	public LineRenderer lineRenderer;
	public GameObject rayGameObject;
	
	private float angle;
	
	// Use this for initialization
	void Start () 
	{
		lineRenderer = gameObject.AddComponent("LineRenderer") as LineRenderer;
		lineRenderer.material.color = myColor;
		lineRenderer.SetWidth(0.1f,0.1f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		bool isMouseDown = Input.GetMouseButton(0);
		
		if(isHit)
		{
			if(isMouseDown && !isClicked)
			{
				firstPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 1));
				
				lineRenderer.SetVertexCount(1);
				lineRenderer.enabled = true;
				lineRenderer.SetPosition(0, firstPosition);
				
				isClicked = true;
			}
			else if(isMouseDown)
			{
				secondPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 1));
				
				lineRenderer.SetVertexCount(2);
				lineRenderer.SetPosition(1, secondPosition);
			}
			else if(Input.GetMouseButtonUp(0))
			{
				isRay = true;
				isClicked = false;
				isHit = false;
				
				secondPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 1));
				
				if(secondPosition.x != firstPosition.x)
					angle = Mathf.Atan((secondPosition.y - firstPosition.y) / (secondPosition.x - firstPosition.x));
				else
					angle = 0;
				
				float angle1 = angle*180/3.14f;
				if((angle1 < 30 && angle1 > 0) || (angle1 < 0 && angle1 > -30))
				{
					mySplashH = (GameObject) Instantiate(splashH,rayPosition + new Vector3(1,0,0),Quaternion.identity);
					setColor(mySplashH);
					Destroy(mySplashH, 1.0f);
				}
				else if((angle1 < 90 && angle1 > 60) || (angle1 < -60 && angle1 > -90))
				{
					mySplashV = (GameObject) Instantiate(splashV,rayPosition + new Vector3(1,0,0),Quaternion.identity);
					setColor(mySplashV);
					Destroy(mySplashV, 1.0f);
				}
				else
				{
					mySplash = (GameObject) Instantiate(splash,rayPosition + new Vector3(1,0,0),Quaternion.identity);
					setColor(mySplash);
					Destroy(mySplash, 1.0f);
				}
				middlePosition = (firstPosition + secondPosition) / 2.0f;
				
				lineRenderer.SetVertexCount(2);
				lineRenderer.SetPosition(1, firstPosition);
				
				rayGameObject = Instantiate(myRay,rayPosition,Quaternion.AngleAxis(angle*180/3.14f,Vector3.forward))
									as GameObject;
				
				myFirstFruit = Instantiate(firstFruit,transform.position,Quaternion.AngleAxis(Random.Range(50,180)*180/3.14f,Vector3.forward))
									as GameObject;
				mySecondFruit = Instantiate(secondFruit,transform.position,Quaternion.AngleAxis(Random.Range(80,150)*180/3.14f,Vector3.forward))
									as GameObject;
				
				if(Random.Range(1,10)>5.0f)
				{
					myFirstFruit.rigidbody.velocity = new Vector2(5,10);
					mySecondFruit.rigidbody.velocity = new Vector2(-8,-10);
				}
				else
				{
					myFirstFruit.rigidbody.velocity = new Vector2(-5,10);
					mySecondFruit.rigidbody.velocity = new Vector2(8,-10);
				}
				Physics.gravity = new Vector3(0, -20, 0);
				
				Destroy(rayGameObject,0.2f);
				Destroy(myFirstFruit,2.0f);
				Destroy(mySecondFruit,2.0f);
				
				if(audio.isPlaying)
					audio.Stop();
				PlaySound(knifeSound);
			}
		}
		else
		{
			isRay = false;
		}
	}
	
	void PlaySound(AudioClip soundName)
	{
		if(!audio.isPlaying)
		{
			AudioSource.PlayClipAtPoint(soundName, new Vector3(0, 0, -10));
		}
	}
	
	void setColor(GameObject myGameObject)
	{
		if(gameObject.name == "Apple00(Clone)")
		{
			myGameObject.transform.renderer.material.color = Color.cyan;
		}
		else if(gameObject.name == "Banana00(Clone)")
		{
			myGameObject.transform.renderer.material.color = Color.black;
		}
		else if(gameObject.name == "GoldApple00(Clone)")
		{
			myGameObject.transform.renderer.material.color = Color.yellow;
		}
		else if(gameObject.name == "Hamster00(Clone)")
		{
			myGameObject.transform.renderer.material.color = Color.red;
		}
		else if(gameObject.name == "waterMelon00(Clone)")
		{
			myGameObject.transform.renderer.material.color = Color.green;
		}
	}
	IEnumerator waiTime()//StartCoroutine(waiTime());
	{
		yield return new WaitForSeconds(1.0f);
	}

}