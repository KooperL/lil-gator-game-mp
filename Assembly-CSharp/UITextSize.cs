using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003DA RID: 986
public class UITextSize : MonoBehaviour
{
	// Token: 0x06001300 RID: 4864 RVA: 0x0001021C File Offset: 0x0000E41C
	private void Awake()
	{
		this.text = base.GetComponent<Text>();
		this.parentTransform = base.transform.parent as RectTransform;
	}

	// Token: 0x06001301 RID: 4865 RVA: 0x00002229 File Offset: 0x00000429
	private void Start()
	{
	}

	// Token: 0x04001875 RID: 6261
	private Text text;

	// Token: 0x04001876 RID: 6262
	private RectTransform parentTransform;

	// Token: 0x04001877 RID: 6263
	public Vector2 wrapping;
}
