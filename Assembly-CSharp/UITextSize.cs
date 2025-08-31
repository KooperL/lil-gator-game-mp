using System;
using UnityEngine;
using UnityEngine.UI;

public class UITextSize : MonoBehaviour
{
	// Token: 0x06000FD4 RID: 4052 RVA: 0x0004B98E File Offset: 0x00049B8E
	private void Awake()
	{
		this.text = base.GetComponent<Text>();
		this.parentTransform = base.transform.parent as RectTransform;
	}

	// Token: 0x06000FD5 RID: 4053 RVA: 0x0004B9B2 File Offset: 0x00049BB2
	private void Start()
	{
	}

	private Text text;

	private RectTransform parentTransform;

	public Vector2 wrapping;
}
