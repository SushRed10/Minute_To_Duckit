using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeviceRotation 
{

	private static bool gyroIntialized = false;

	public static bool HasGyroScope
	{
		get
		{
			return SystemInfo.supportsGyroscope;
		}
	}

	public static Quaternion Get()
	{
		if (!gyroIntialized)
		{
			InitGyro();
		}

		return HasGyroScope ? ReadGyroscopeRotation() : Quaternion.identity;
	}

	private static void InitGyro()
	{
		if (HasGyroScope)
		{
			Input.gyro.enabled = true;
			Input.gyro.updateInterval = 0.0167f;
		}
		gyroIntialized = true;
	}

	private static Quaternion ReadGyroscopeRotation()
	{
		return new Quaternion(0.5f, 0.5f, -0.5f, 0.5f) * Input.gyro.attitude * new Quaternion(0,0,1,0);
	}
}
