package com.example.a;

import android.os.Bundle;
import android.os.Handler;
import android.widget.Toast;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

public class MainActivity extends UnityPlayerActivity {

	@Override
	protected void onCreate(Bundle arg0) {
		// TODO Auto-generated method stub
		super.onCreate(arg0);

		new Handler().postDelayed(new Runnable() {

			@Override
			public void run() {
				// TODO Auto-generated method stub
				// UnityPlayer.UnitySendMessage()
				// 参数1表示发送游戏对象的名称，参数2表示对象绑定的脚本接收该消息的方法，参数3表示本条消息发送的字符串信息
				UnityPlayer.UnitySendMessage("NativeUtils", "javaCallUnity","i am this com.example.a");
			}
		}, 3000);
	}

	@Override
	protected void onResume() {
		// TODO Auto-generated method stub
		super.onResume();
		Toast.makeText(this, "You are using the com.example.a !",Toast.LENGTH_LONG).show();
	}
}
