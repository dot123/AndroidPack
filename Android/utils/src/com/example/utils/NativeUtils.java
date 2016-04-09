package com.example.utils;

import android.content.Context;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;

import com.example.utils.Helper.LogHelper;

public class NativeUtils extends SDKManageHelper {

	private static NativeUtils c = new NativeUtils();
	private static boolean isFirststart = false;

	public NativeUtils() {
		// TODO Auto-generated constructor stub

	}

	public static NativeUtils getInstance() {
		return c;
	}

	@Override
	public void onCreate(Context paramContext) {
		// TODO Auto-generated method stub
		context = paramContext;

	}

	@Override
	public void onResume() {
		// TODO Auto-generated method stub

	}

	@Override
	public void onPause() {
		// TODO Auto-generated method stub

	}

	@Override
	public void onStop() {
		// TODO Auto-generated method stub

	}

	@Override
	public void onDestroy() {
		// TODO Auto-generated method stub

	}

	/**
	 * 根据包名获取版本号
	 * 
	 * @param context
	 * @param appName
	 * @return
	 * @throws NameNotFoundException
	 */
	public String getAppVersionName() {

		String versionName = null;
		int versionCode = 0;
		int flags = 0;
		try {
			PackageManager pm = context.getPackageManager();
			PackageInfo pi = pm.getPackageInfo(context.getPackageName(), flags);
			versionName = pi.versionName;
			versionCode = pi.versionCode;
			if (versionName == null || versionName.length() <= 0) {
				return null;
			}
		} catch (NameNotFoundException e) {
			LogHelper.e("getAppVersionName error:", "" + e);
		}
		return versionName;
	}

	public String getPackageName(Context context) {
		try {
			return context.getPackageName();
		} catch (Exception e) {
			// TODO: handle exception
		}
		return "";
	}
}
