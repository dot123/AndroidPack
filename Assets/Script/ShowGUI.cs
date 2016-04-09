using UnityEngine;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

using System.Text;
using System.IO;


public class ShowGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() 
	{
		#if A
				GUI.Label(new Rect(20, 140, 300, 20), "宏定义:A");  
		#endif
		#if B
				GUI.Label(new Rect(20, 140, 300, 20), "宏定义:B");  
		#endif
		#if C
				GUI.Label(new Rect(20, 140, 300, 20), "宏定义:C");  
		#endif
		#if TEST
				GUI.Label(new Rect(20, 180, 300, 20), "宏定义:TEST"); 
		#endif
		GUI.Label(new Rect(20, 220, 300, 20), "消息:" + NativeUtils.javaMSG);  


		GUI.Label(new Rect(20, 20, 300, 20), "包名:" + NativeUtils.Instance.getPackageName());  
		GUI.Label(new Rect(20, 60, 300, 20), "版本:" + NativeUtils.Instance.getAppVersionName()); 
		GUI.Label(new Rect(20, 100, 300, 20), "签名:" + NativeUtils.Instance.getSignature()); 
	}


}
