using System;
using System.Collections;
using UnityEngine;

public class Laptop : MonoBehaviour
{
	// Token: 0x060009C7 RID: 2503 RVA: 0x000096CC File Offset: 0x000078CC
	public void Close()
	{
		CoroutineUtil.Start(this.CloseCoroutine());
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x000096DA File Offset: 0x000078DA
	private IEnumerator CloseCoroutine()
	{
		float angle = 0f;
		while (angle != this.closedAngle)
		{
			angle = Mathf.MoveTowards(angle, this.closedAngle, this.closeSpeed * Time.deltaTime);
			this.screen.transform.localRotation = Quaternion.Euler(angle, 0f, 0f);
			yield return null;
		}
		this.closeSound.Play();
		yield break;
	}

	public Transform screen;

	public float closedAngle = 101.2f;

	public float closeSpeed = 30f;

	public AudioSource closeSound;
}
