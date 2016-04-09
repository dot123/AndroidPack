using UnityEngine;
using System.Collections;

public class NativeUtils : MonoBehaviour {

	//安卓*.jar中的类名
	private static string className = "com.example.utils.NativeUtils";
	
	// Use this for initialization
	public void Start()
	{
		if (SdkJavaClass () == null) {
			Debug.Log ("init SdkInstance failed.");
		} 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private static NativeUtils _instance;
	public static NativeUtils Instance
	{
		get
		{
			if (!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(NativeUtils)) as NativeUtils;
				if (!_instance)
				{
					GameObject container = new GameObject();
					container.name = "NativeUtils";
					_instance = container.AddComponent(typeof(NativeUtils)) as NativeUtils;
					container.AddComponent(typeof(NativeUtils));
					DontDestroyOnLoad(_instance);
				}
			}
			return _instance;
		}
	}
	
	void OnApplicationFocus(bool focusStatus) {
		Debug.Log ("called with OnApplicationFocus. " + focusStatus);
		if (SdkJavaClass () != null && !focusStatus) {
			SdkJavaClass ().Call("onStop");
		}
	}
	
	void OnApplicationPause(bool pauseStatus) {
		Debug.Log ("called with OnApplicationPause. " + pauseStatus);
		if (SdkJavaClass () != null && pauseStatus) {
			Debug.Log ("called with onPause.");
			SdkJavaClass ().Call("onPause");
		} else if (SdkJavaClass () != null && !pauseStatus) {
			SdkJavaClass ().Call("onResume");
		}
	}
	
	void OnApplicationQuit() {
		Debug.Log ("called with OnApplicationQuit.");
		if (SdkJavaClass () != null) {
			SdkJavaClass ().Call("onDestroy");
		}
	}

	private static AndroidJavaObject SdkInstance = null;
	private static AndroidJavaObject activityContext = null;
	private static AndroidJavaObject SdkJavaClass()
	{
		#if UNITY_EDITOR
			return null;
		#endif
		if(SdkInstance == null) {
			Debug.Log ("start init SdkInstance!!!");
			//获得一个java类
			using(AndroidJavaClass activityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer")) {
				activityContext = activityClass.GetStatic<AndroidJavaObject> ("currentActivity");//获得当前Activity的对象
				Debug.Log ("activityContext init success!!!");
			}
			//AndroidJavaClass表示一个java类,AndroidJavaObject表示一个java对象
			using(AndroidJavaClass pluginClass = new AndroidJavaClass(className)) {
				if(pluginClass != null) {
					SdkInstance = pluginClass.CallStatic<AndroidJavaObject> ("getInstance");//调用静态方法
					Debug.Log ("call getInstance success!!!");
					
					SdkInstance.Call("onCreate", activityContext);
					SdkInstance.Call("onResume");
				} else {
					Debug.Log ("called onCreate failed.");
				}
			}
		}
		return SdkInstance;
	}

	public string getAppVersionName()
	{
		#if UNITY_EDITOR
			return "";
		#endif
		string appVersionName = "";
		if (SdkJavaClass () != null) {
			appVersionName = SdkJavaClass ().Call<string> ("getAppVersionName");
		} 
		return appVersionName;
	}

	public string getPackageName()
	{
		#if UNITY_EDITOR
			return "";
		#endif
		string packageName = "";
		if (SdkJavaClass () != null) {
			packageName = SdkJavaClass ().Call<string> ("getPackageName",activityContext);
		} 
		return packageName;
	}

	public string getSignature()
	{
		#if UNITY_EDITOR
			return "";
		#endif
		try
		{
			//获得一个java类
			using(AndroidJavaClass genSingature = new AndroidJavaClass("com.example.gensignature.SignUtils")) {
				return genSingature.CallStatic<string>("getSignature",activityContext,getPackageName());//调用静态方法
			}
		}
		catch
		{

		}
		return "";
	}
	
	public static string javaMSG = "";
	public void javaCallUnity(string msg)
	{
		javaMSG = msg;
	}
	
}
