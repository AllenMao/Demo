using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour 
{
	public AudioClip gameWin;
	public static int myScore;
	
	public GameObject digit;
	public Texture2D highScore;
	public Texture2D score;
		
	private GameObject myDigtal1;
	private GameObject myDigtal2;
	// Use this for initialization
	void Start () 
	{
		myDigtal1 = Instantiate(digit, new Vector3(6.07f,4.3f,-9.3f), Quaternion.identity) as GameObject;//数字显示的位置
		
		DigtalDisplay myScript1 = myDigtal1.transform.GetComponent("DigtalDisplay") as DigtalDisplay;//克隆DigtalDisplay脚本
		//读取本地信息
		if(PlayerPrefs.HasKey("HighScore"))
			myScript1.myStringScore = PlayerPrefs.GetInt("HighScore").ToString();
		else
			myScript1.myStringScore = "";
		
		myDigtal2 = Instantiate(digit, new Vector3(6.07f,3.9f,-9.3f), Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		DigtalDisplay myScript2 = myDigtal2.transform.GetComponent("DigtalDisplay") as DigtalDisplay;
		
		if(myScore == 0)
			myScript2.myStringScore = "";
		else
			myScript2.myStringScore = myScore.ToString();
		
		GameObject pigObject = GameObject.FindWithTag("pig");
		if(pigObject == null)
		{
			if(PlayerPrefs.GetInt("HighScore") < myScore)
				PlayerPrefs.SetInt("HighScore", myScore);
			StartCoroutine(PlaySound(gameWin));
		}
	}
	void OnGUI()
	{
		GUI.Label(new Rect(305+320,35,70,54), score);
		GUI.Label(new Rect(270+320,0,185,57),highScore);
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
			myScore = 0;
			Application.LoadLevel(2);
		}
	}
}
