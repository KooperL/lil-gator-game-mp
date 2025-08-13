using System;
using UnityEngine;

// Token: 0x0200010A RID: 266
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

	// Token: 0x0400077D RID: 1917
	public AudioSource audioSource;

	// Token: 0x0400077E RID: 1918
	public float minSpeed = 5f;

	// Token: 0x0400077F RID: 1919
	public float maxSpeed = 15f;

	// Token: 0x04000780 RID: 1920
	public float maxVolume = 0.5f;

	// Token: 0x04000781 RID: 1921
	public float minPitch = 0.95f;

	// Token: 0x04000782 RID: 1922
	public float maxPitch = 1.1f;

	// Token: 0x04000783 RID: 1923
	private float volumeVelocity;

	// Token: 0x04000784 RID: 1924
	private Vector3 oldPosition;

	// Token: 0x04000785 RID: 1925
	private float speed;

	// Token: 0x04000786 RID: 1926
	private float speedVelocity;
}
