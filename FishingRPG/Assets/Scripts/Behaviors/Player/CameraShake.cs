using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	private Transform camTransform;

	public float shakeDuration = 0f;
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;
	Vector3 originalPos;

	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = transform;
		}
	}

	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void FixedUpdate()
	{
		if (shakeDuration > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

			shakeDuration -= Time.fixedDeltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			camTransform.localPosition = originalPos;
		}
	}
}
