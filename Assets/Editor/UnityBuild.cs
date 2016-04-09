using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class UnityBuild 
{
	//包名
	static string[] PackageName = {
		"com.example.a",
		"com.example.b",
		"com.example.c"
	};

	//宏定义
	static string[] DefineSymbols = {
		"A",
		"B",	
		"C",
	};
	
	static string OtherDefine = "TEST;";//其他宏定义
	static string bundleVersion = "1.0.0";//版本
	static string shortBundleVersion = "1.0";//短版本

	//得到工程中所有场景名称
	static string[] SCENES = FindEnabledEditorScenes();

	//一系列批量build的操作
	[MenuItem ("Custom/Build Android TEST A")]
	static void PerformAndroidTESTBuild()
	{  
		BuildTargetTo(0,"Android");
	}

	[MenuItem ("Custom/Build Android TEST B")]
	static void PerformAndroidTMGPBuild()
	{  
		BuildTargetTo(1,"Android");
	}

	[MenuItem ("Custom/Build Android TEST C")]
	static void PerformAndroidZAIYEBuild()
	{  
		BuildTargetTo(2,"Android");
	}

	[MenuItem ("Custom/Build Android ALL")]
	static void PerformAndroidALLBuild()
	{  
		for(int i = 0;i < PackageName.Length;++i)
		{
			if(i == 3)//过滤
			{

			}
			else
			{
				BuildTargetTo(i,"Android");
			}
		}
	}
	
	[MenuItem ("Custom/Delete Android Folder")]
	static void DeleteAndroidFolder()
	{
		FileUtils.DeleteFolder("Plugins/Android/");
	}
	
	static void BuildTargetTo(int index,string target,Boolean other = true,Boolean sign = true)
	{
		FileUtils.initAndroidResources(PackageName[index], other, sign);//初始化安卓资源
		System.DateTime now = System.DateTime.Now;//获取系统时间
		string package_name = PackageName[index];
		string target_folder = "TargetAndroid/" + now.Year + "" + string.Format("{0:D2}",now.Month) + "" + string.Format("{0:D2}",now.Day);//输出目标文件夹为 “TargetAndroid/当前日期”
		string target_dir = "";
		string target_name = package_name + ".apk";
		BuildTargetGroup targetGroup = BuildTargetGroup.Android;
		BuildTarget buildTarget = BuildTarget.Android;
		string applicationPath = Application.dataPath.Replace("/Assets","");

		if(target == "Android")
		{
			target_dir = applicationPath + "/" + target_folder;
			target_name = package_name + ".apk";
			targetGroup = BuildTargetGroup.Android;
		}
		//每次build删除之前的apk
		if(Directory.Exists(target_folder))
		{
			if (File.Exists(target_folder + "/" + target_name))
			{
				File.Delete(target_folder + "/" + target_name);
			}
		}
		else
		{
			Directory.CreateDirectory(target_folder);
		}

		PlayerSettings.bundleIdentifier = package_name;//包名
		PlayerSettings.bundleVersion = bundleVersion;//版本号
		PlayerSettings.shortBundleVersion = shortBundleVersion;//短版本
		PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup,OtherDefine + DefineSymbols[index]);//宏定义
		PlayerSettings.companyName = "example";//公司名称
		PlayerSettings.productName = "example";//游戏名称

		//开始Build场景
		GenericBuild(SCENES, target_dir + "/" + target_name, buildTarget,BuildOptions.None);
	}
	
	static string[] FindEnabledEditorScenes() 
	{
		List<string> EditorScenes = new List<string>();
		foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
			if (!scene.enabled) continue;
			EditorScenes.Add(scene.path);
		}
		return EditorScenes.ToArray();
	}
	
	static void GenericBuild(string[] scenes, string target_dir, BuildTarget build_target, BuildOptions build_options)
	{  
		EditorUserBuildSettings.SwitchActiveBuildTarget(build_target);
		string res = BuildPipeline.BuildPlayer(scenes,target_dir,build_target,build_options);
		
		if (res.Length > 0) {
			throw new Exception("BuildPlayer failure: " + res);
		}
	}
}