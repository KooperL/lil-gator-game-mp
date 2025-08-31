using System;
using UnityEngine;

public class Wooshing : MonoBehaviour
{
	// Token: 0x06000573 RID: 1395 RVA: 0x0001CD77 File Offset: 0x0001AF77
	private void OnEnable()
	{
		this.speed = 0f;
		this.speedVelocity = 0f;
		this.oldPosition = base.transform.position;
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x0001CDA0 File Offset: 0x0001AFA0
	private void Update()
	{
		Vector3 position = base.transform.position;
		float num = Vector3.Distance(position, this.oldPosition) / Time.deltaTime;
		num = Mathf.Clamp(num, 0f, this.maxSpeed);
		this.speed = Mathf.SmoothDamp(this.speed, num, ref this.speedVelocity, 0.25f);
		float num2 = Mathf.InverseLerp(this.minSpeed, this.maxSpeed, this.speed);
		this.audioSource.volume = Mathf.Lerp(0f, this.maxVolume, num2);
		this.audioSource.pitch = Mathf.Lerp(this.minPitch, this.maxPitch, num2);
		if (this.audioSource.isPlaying != this.speed > this.minSpeed)
		{
			if (this.speed > this.minSpeed)
			{
				this.audioSource.Play();
			}
			else
			{
				this.audioSource.Stop();
			}
		}
		this.oldPosition = position;
	}

	public AudioSource audioSource;

	public float minSpeed = 5f;

	public float maxSpeed = 15f;

	public float maxVolume = 0.5f;

	public float minPitch = 0.95f;

	public float maxPitch = 1.1f;

	private float volumeVelocity;

	private Vector3 oldPosition;

	private float speed;

	private float speedVelocity;
}
