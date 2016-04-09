package com.example.gensignature;

import java.io.ByteArrayInputStream;
import java.security.MessageDigest;
import java.security.cert.CertificateException;
import java.security.cert.CertificateFactory;
import java.security.cert.X509Certificate;

import android.content.Context;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.content.pm.Signature;
import android.util.Log;

public class SignUtils {
	public static void getSignatureInfo(Context context) {
		try {
			PackageInfo packageInfo = context.getPackageManager().getPackageInfo(context.getPackageName(),PackageManager.GET_SIGNATURES);
			Signature[] signatures = packageInfo.signatures;
			Signature signature = signatures[0];
			parseSignature(signature.toByteArray());
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public static void parseSignature(byte[] signature) {
		try {
			CertificateFactory certFactory = CertificateFactory.getInstance("X.509");
			X509Certificate cert = (X509Certificate) certFactory.generateCertificate(new ByteArrayInputStream(signature));
			String pubKey = cert.getPublicKey().toString();
			String signNumber = cert.getSerialNumber().toString();

			System.out.println("signName:" + cert.getSigAlgName());
			System.out.println("pubKey:" + pubKey);
			System.out.println("signNumber:" + signNumber);
			System.out.println("subjectDN:" + cert.getSubjectDN().toString());

		} catch (CertificateException e) {
			e.printStackTrace();
		}
	}

	/**
	 * 根据包名获取版本号
	 * 
	 * @param context
	 * @param appName
	 * @return
	 * @throws NameNotFoundException
	 */
	public static String getAppVersionName(Context context) {

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
			Log.e("getAppVersionName error:", "" + e);
		}
		return versionName;
	}
	
	/**
	 * 获取对应包名的应用的签名串
	 * 
	 * @param context
	 * @param paramString
	 *            应用程序包名
	 * @return
	 */
	public static String getSignature(Context context, String paramString) {
		if (context == null) {
			throw new IllegalAccessError("Context is null!!!");
		}
		Signature[] arrayOfSignature = getRawSignature(context, paramString);
		if ((arrayOfSignature == null) || (arrayOfSignature.length == 0)) {
			Log.d("SIGN", "signs is null");
			return null;
		}
		int i = arrayOfSignature.length;
		if (i > 0) {
			return getMessageDigest(arrayOfSignature[0].toByteArray());
		}
		return null;
	}

	private static Signature[] getRawSignature(Context paramContext,
			String paramString) {
		if ((paramString == null) || (paramString.length() == 0)) {
			Log.d("SIGN", "getSignature, packageName is null");
			return null;
		}
		PackageManager localPackageManager = paramContext.getPackageManager();
		PackageInfo localPackageInfo;
		try {
			localPackageInfo = localPackageManager.getPackageInfo(paramString,64);
			if (localPackageInfo == null) {
				Log.d("SIGN", "info is null, packageName = " + paramString);
				return null;
			}
		} catch (PackageManager.NameNotFoundException localNameNotFoundException) {
			Log.d("SIGN", "NameNotFoundException");
			return null;
		}
		return localPackageInfo.signatures;
	}

	private static final String getMessageDigest(byte[] paramArrayOfByte) {
		char[] arrayOfChar1 = { 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 97, 98,
				99, 100, 101, 102 };
		try {
			MessageDigest localMessageDigest = MessageDigest.getInstance("MD5");
			localMessageDigest.update(paramArrayOfByte);
			byte[] arrayOfByte = localMessageDigest.digest();
			int i = arrayOfByte.length;
			char[] arrayOfChar2 = new char[i * 2];
			int j = 0;
			int k = 0;
			while (true) {
				if (j >= i)
					return new String(arrayOfChar2);
				int m = arrayOfByte[j];
				int n = k + 1;
				arrayOfChar2[k] = arrayOfChar1[(0xF & m >>> 4)];
				k = n + 1;
				arrayOfChar2[n] = arrayOfChar1[(m & 0xF)];
				j++;
			}
		} catch (Exception localException) {
		}
		return null;
	}
}