using System;
using UnityEngine;

// Token: 0x0200015F RID: 351
public class Wooshing : MonoBehaviour
{
	// Token: 0x0600068B RID: 1675 RVA: 0x00006BC5 File Offset: 0x00004DC5
	private void OnEnable()
	{
		this.speed = 0f;
		this.speedVelocity = 0f;
		this.oldPosition = base.transform.position;
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x00031364 File Offset: 0x0002F564
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

	// Token: 0x040008D1 RID: 2257
	public AudioSource audioSource;

	// Token: 0x040008D2 RID: 2258
	public float minSpeed = 5f;

	// Token: 0x040008D3 RID: 2259
	public float maxSpeed = 15f;

	// Token: 0x040008D4 RID: 2260
	public float maxVolume = 0.5f;

	// Token: 0x040008D5 RID: 2261
	public float minPitch = 0.95f;

	// Token: 0x040008D6 RID: 2262
	public float maxPitch = 1.1f;

	// Token: 0x040008D7 RID: 2263
	private float volumeVelocity;

	// Token: 0x040008D8 RID: 2264
	private Vector3 oldPosition;

	// Token: 0x040008D9 RID: 2265
	private float speed;

	// Token: 0x040008DA RID: 2266
	private float speedVelocity;
}
