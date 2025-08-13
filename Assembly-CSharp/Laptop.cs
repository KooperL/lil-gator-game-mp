using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200018A RID: 394
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

	// Token: 0x04000A3F RID: 2623
	public Transform screen;

	// Token: 0x04000A40 RID: 2624
	public float closedAngle = 101.2f;

	// Token: 0x04000A41 RID: 2625
	public float closeSpeed = 30f;

	// Token: 0x04000A42 RID: 2626
	public AudioSource closeSound;
}
