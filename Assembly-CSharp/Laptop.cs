using System;
using System.Collections;
using UnityEngine;

public class Laptop : MonoBehaviour
{
	// Token: 0x06000816 RID: 2070 RVA: 0x00026E2E File Offset: 0x0002502E
	public void Close()
	{
		CoroutineUtil.Start(this.CloseCoroutine());
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x00026E3C File Offset: 0x0002503C
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
