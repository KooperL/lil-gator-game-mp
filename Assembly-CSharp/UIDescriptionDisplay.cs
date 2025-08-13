using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200036C RID: 876
public class UIDescriptionDisplay : MonoBehaviour
{
	// Token: 0x060010CE RID: 4302 RVA: 0x0000E6BA File Offset: 0x0000C8BA
	public void Load(string description, UIDescription parent)
	{
		this.clearTime = -1f;
		this.uiDescription = parent;
		this.descriptionText.text = description;
		this.onLoad.Invoke();
	}

	// Token: 0x060010CF RID: 4303 RVA: 0x0000E6E5 File Offset: 0x0000C8E5
	public void KeepOpen()
	{
		this.clearTime = -1f;
	}

	// Token: 0x060010D0 RID: 4304 RVA: 0x0000E6F2 File Offset: 0x0000C8F2
	public void Clear()
	{
		this.clearTime = Time.time + 0.03f;
	}

	// Token: 0x060010D1 RID: 4305 RVA: 0x0000E705 File Offset: 0x0000C905
	private void Update()
	{
		if (this.clearTime > 0f && Time.time > this.clearTime)
		{
			this.uiDescription.ForgetDescription();
			this.onClear.Invoke();
			this.clearTime = -1f;
		}
	}

	// Token: 0x060010D2 RID: 4306 RVA: 0x000047FB File Offset: 0x000029FB
	public void DestroyObject()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x040015D7 RID: 5591
	public UnityEvent onLoad;

	// Token: 0x040015D8 RID: 5592
	public UnityEvent onClear;

	// Token: 0x040015D9 RID: 5593
	public Text descriptionText;

	// Token: 0x040015DA RID: 5594
	private UIDescription uiDescription;

	// Token: 0x040015DB RID: 5595
	private float clearTime;
}
