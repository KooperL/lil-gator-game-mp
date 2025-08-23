using System;
using UnityEngine;
using UnityEngine.UI;

public class UITextSize : MonoBehaviour
{
	// Token: 0x06001361 RID: 4961 RVA: 0x00010623 File Offset: 0x0000E823
	private void Awake()
	{
		this.text = base.GetComponent<Text>();
		this.parentTransform = base.transform.parent as RectTransform;
	}

	// Token: 0x06001362 RID: 4962 RVA: 0x00002229 File Offset: 0x00000429
	private void Start()
	{
	}

	private Text text;

	private RectTransform parentTransform;

	public Vector2 wrapping;
}
