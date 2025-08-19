using System;
using UnityEngine;

public class UIWobble : MonoBehaviour
{
	// Token: 0x06001365 RID: 4965 RVA: 0x00010659 File Offset: 0x0000E859
	private void Awake()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
		this.seed = new Vector2(10000f * global::UnityEngine.Random.value, 20000f * global::UnityEngine.Random.value);
	}

	// Token: 0x06001366 RID: 4966 RVA: 0x00010688 File Offset: 0x0000E888
	private void Start()
	{
		this.initialPosition = this.rectTransform.anchoredPosition;
	}

	// Token: 0x06001367 RID: 4967 RVA: 0x0005EEF8 File Offset: 0x0005D0F8
	private void Update()
	{
		if (this.lowFramerate)
		{
			if (Time.time < this.updateTime)
			{
				return;
			}
			this.updateTime = Time.time + 1f / this.updateTime;
		}
		Vector2 vector = new Vector2(Mathf.PerlinNoise(this.seed.x, Time.time * this.speed) - 0.5f, Mathf.PerlinNoise(this.seed.y, Time.time * this.speed) - 0.5f);
		Vector2 vector2 = this.initialPosition + vector * this.magnitude;
		if (this.pixelPerfect)
		{
			vector2.x = Mathf.Round(vector2.x);
			vector2.y = Mathf.Round(vector2.y);
		}
		this.rectTransform.anchoredPosition = vector2;
	}

	public float speed = 0.5f;

	public float magnitude = 10f;

	private RectTransform rectTransform;

	private Vector2 seed;

	public bool pixelPerfect = true;

	public bool lowFramerate;

	[ConditionalHide("lowFramerate", true)]
	public float framerate;

	private float updateTime = -1f;

	private Vector2 initialPosition;
}
