using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000200 RID: 512
public class Laptop : MonoBehaviour
{
	// Token: 0x06000980 RID: 2432 RVA: 0x00009364 File Offset: 0x00007564
	public void Close()
	{
		CoroutineUtil.Start(this.CloseCoroutine());
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x00009372 File Offset: 0x00007572
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

	// Token: 0x04000C28 RID: 3112
	public Transform screen;

	// Token: 0x04000C29 RID: 3113
	public float closedAngle = 101.2f;

	// Token: 0x04000C2A RID: 3114
	public float closeSpeed = 30f;

	// Token: 0x04000C2B RID: 3115
	public AudioSource closeSound;
}
