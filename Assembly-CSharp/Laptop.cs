using System;
using System.Collections;
using UnityEngine;

public class Laptop : MonoBehaviour
{
	// Token: 0x060009C6 RID: 2502 RVA: 0x000096C2 File Offset: 0x000078C2
	public void Close()
	{
		CoroutineUtil.Start(this.CloseCoroutine());
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x000096D0 File Offset: 0x000078D0
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
