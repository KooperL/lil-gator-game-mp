using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000292 RID: 658
public class UIDescriptionDisplay : MonoBehaviour
{
	// Token: 0x06000E06 RID: 3590 RVA: 0x00043D11 File Offset: 0x00041F11
	public void Load(string description, UIDescription parent)
	{
		this.clearTime = -1f;
		this.uiDescription = parent;
		this.descriptionText.text = description;
		this.onLoad.Invoke();
	}

	// Token: 0x06000E07 RID: 3591 RVA: 0x00043D3C File Offset: 0x00041F3C
	public void KeepOpen()
	{
		this.clearTime = -1f;
	}

	// Token: 0x06000E08 RID: 3592 RVA: 0x00043D49 File Offset: 0x00041F49
	public void Clear()
	{
		this.clearTime = Time.time + 0.03f;
	}

	// Token: 0x06000E09 RID: 3593 RVA: 0x00043D5C File Offset: 0x00041F5C
	private void Update()
	{
		if (this.clearTime > 0f && Time.time > this.clearTime)
		{
			this.uiDescription.ForgetDescription();
			this.onClear.Invoke();
			this.clearTime = -1f;
		}
	}

	// Token: 0x06000E0A RID: 3594 RVA: 0x00043D99 File Offset: 0x00041F99
	public void DestroyObject()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04001279 RID: 4729
	public UnityEvent onLoad;

	// Token: 0x0400127A RID: 4730
	public UnityEvent onClear;

	// Token: 0x0400127B RID: 4731
	public Text descriptionText;

	// Token: 0x0400127C RID: 4732
	private UIDescription uiDescription;

	// Token: 0x0400127D RID: 4733
	private float clearTime;
}
