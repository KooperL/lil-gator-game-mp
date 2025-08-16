using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIDescriptionDisplay : MonoBehaviour
{
	// Token: 0x0600112A RID: 4394 RVA: 0x0000EA27 File Offset: 0x0000CC27
	public void Load(string description, UIDescription parent)
	{
		this.clearTime = -1f;
		this.uiDescription = parent;
		this.descriptionText.text = description;
		this.onLoad.Invoke();
	}

	// Token: 0x0600112B RID: 4395 RVA: 0x0000EA52 File Offset: 0x0000CC52
	public void KeepOpen()
	{
		this.clearTime = -1f;
	}

	// Token: 0x0600112C RID: 4396 RVA: 0x0000EA5F File Offset: 0x0000CC5F
	public void Clear()
	{
		this.clearTime = Time.time + 0.03f;
	}

	// Token: 0x0600112D RID: 4397 RVA: 0x0000EA72 File Offset: 0x0000CC72
	private void Update()
	{
		if (this.clearTime > 0f && Time.time > this.clearTime)
		{
			this.uiDescription.ForgetDescription();
			this.onClear.Invoke();
			this.clearTime = -1f;
		}
	}

	// Token: 0x0600112E RID: 4398 RVA: 0x000049DF File Offset: 0x00002BDF
	public void DestroyObject()
	{
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	public UnityEvent onLoad;

	public UnityEvent onClear;

	public Text descriptionText;

	private UIDescription uiDescription;

	private float clearTime;
}
