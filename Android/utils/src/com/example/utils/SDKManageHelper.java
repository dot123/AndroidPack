package com.example.utils;

import android.content.Context;

public abstract class SDKManageHelper {

	protected Context context = null;
	protected static String TAG = SDKManageHelper.class.getSimpleName();

	public abstract void onCreate(Context paramContext);

	public abstract void onResume();

	public abstract void onPause();

	public abstract void onStop();

	public abstract void onDestroy();
}
