using UnityEngine;
using System.Collections;

public class HitByKnife : MonoBehaviour 
{
	public GameObject goldFruit;
	public AudioClip fallSound;
	public AudioClip hamsterSound;
	
	private GameObject myGoldFruit;
	private GameObject myKnifeScript;
	private bool isClicked = false;
	
	private static int myScore = 0;
	private static int myLife = 6;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		bool isMouseDown = Input.GetMouseButton(0);
		if(!isClicked)
		{
			if(isMouseDown)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				
				//JS::if(collider.Raycast(ray, hit, 100.0f))
				if (Physics.Raycast(ray, out hit))
				{
					if(hit.collider != null)
					{
						//KnifeRay.isHit = true; need to be static
						(transform.transform.GetComponent("KnifeRay") as KnifeRay).isHit = true;
						(transform.GetComponent("KnifeRay") as KnifeRay).rayPosition = hit.transform.position;
						isClicked = true;
					}
				}
			}
		}
		else if((transform.GetComponent("KnifeRay") as KnifeRay).isRay)
		{
			if(gameObject.name != "Hamster00(Clone)")
			{
				if(gameObject.name == "GoldApple00(Clone)")
				{
					myGoldFruit = Instantiate(goldFruit,transform.position,Quaternion.identity) as GameObject;
					
					Destroy(myGoldFruit,1.0f);
					
					myScore += 20;
				}
				gameObject.SetActive(false);
				Destroy(gameObject,1.0f);
				myScore++;
			}
			else
			{
				if(audio.isPlaying)
					audio.Stop();
				PlaySound(hamsterSound);
				
				Destroy(gameObject);
			}
		}
		
		if(transform.position.y <= -7.1 && gameObject.name != "Hamster00(Clone)")
		{
			if(audio.isPlaying)
				audio.Stop();
			PlaySound(fallSound);
			
			Destroy(gameObject);
			
			myLife--;
		}
		
		Score.score = myScore;
		LifeController.life = myLife;
		
		if(myLife == 1)
		{
			Application.LoadLevel(2);
		}
	}
	
	void PlaySound(AudioClip soundName)
	{
		if(!audio.isPlaying)
		{
			AudioSource.PlayClipAtPoint(soundName, new Vector3(0, 0, -10));
		}
	}
}
