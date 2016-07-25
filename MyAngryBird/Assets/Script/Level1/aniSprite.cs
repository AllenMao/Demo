using UnityEngine;
using System.Collections;

public class aniSprite : MonoBehaviour 
{
	public bool isUsed = false;
	public float timeLength = 0;
	public int columnSize;
	public int rowSize;
	public int colStart;
	public int rowStart;
	public int totalFrames = 1;
	public int framePerSecond;
	public float totalTime = 1;
	
	private float myTime = 0;
	private float myTimeLength = 0;
	//private bool isPlay = true;
	private Vector2 size;
	private Vector2 offsetSize;
	private int u;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isUsed)
		{
			isUsed = aniSprite1(true);
		}
		myTimeLength += Time.deltaTime;
		if(timeLength != 0 && myTimeLength > timeLength)
		{
			Destroy(gameObject);
		}
	}
	
	bool aniSprite1(bool moveDirection)
	{
		myTime += Time.deltaTime;
		if(totalTime !=0 && myTime > totalTime)
		{
			//isPlay = false;
			myTime = 0;
			
			return false;
		}
		int index = (int)myTime * (framePerSecond - 1);
		index %= totalFrames;
		
		int v = index / columnSize;
		
		if(moveDirection)
		{
			size = new Vector2(1.0f/columnSize, 1.0f/rowSize);
			u = index % columnSize;
		}
		else
		{
			size = new Vector2(-1.0f/columnSize, 1.0f/rowSize);
			u = -index % columnSize;
		}
		offsetSize = new Vector2((u + colStart) * size.x, (1.0f - size.y)-(v + rowStart) * size.y);
		renderer.material.mainTextureOffset = offsetSize;
		renderer.material.mainTextureScale = size;
		
		return true;
	}
}
