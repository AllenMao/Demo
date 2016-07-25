using UnityEngine;
using System.Collections;

public class TraceLine : MonoBehaviour 
{
	public int lengthOfLineRenderer = 1;
	public GameObject particle;
	public Color myColor;
	
	private LineRenderer linerenderer;
	private int index = 0;
	private Vector3 position;
	
	private bool isStop = false;
	private GameObject myParticle;
	
	// Use this for initialization
	void Start () 
	{
		linerenderer = gameObject.AddComponent("LineRenderer") as LineRenderer;
		Material material = new Material(Shader.Find("Transparent/Diffuse"));//("Particles/Additive"));
        material.color = Color.white;
		linerenderer.material = material;
		//linerenderer.material.color = myColor;
		//linerenderer.SetColors(Color.white,Color.white);
		linerenderer.SetWidth(0.02f, 0.02f);
		linerenderer.SetVertexCount(1);//一个一个点的画，因为默认在原点
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isStop)
		{
			transform.gameObject.rigidbody.velocity = new Vector3(0.2f, transform.gameObject.rigidbody.velocity.y,
															transform.gameObject.rigidbody.velocity.z);
			//this.transform.localRotation.z = 0;//new Quaternion(transform.localRotation.x, transform.localRotation.y, 0);
		}
		
		linerenderer = GetComponent("LineRenderer") as LineRenderer;
		
		position = transform.position;
		if(SlingShot.isDrawing)
		{
			linerenderer.SetPosition(index, position);
			if(index >= (lengthOfLineRenderer - 1))
				return;
			else
			{
				++ index;
				linerenderer.SetVertexCount(index + 1);
				linerenderer.SetPosition(index, position);
			}
		}
	}
	
	void OnCollisionEnter(Collision collision)
	{
		SlingShot.isDrawing = false;
		isStop = true;
		
		if(myParticle == null)
			myParticle = Instantiate(particle,transform.position,Quaternion.identity)as GameObject;
		
		StartCoroutine(waitTime());//2s后
		
	}
	
	IEnumerator waitTime()
	{
		yield return new WaitForSeconds(5.0f);
		gameObject.renderer.enabled = false;
	}
}
