using System;
using UnityEngine;

// Token: 0x020002EB RID: 747
public class UIWobble : MonoBehaviour
{
	// Token: 0x06000FD9 RID: 4057 RVA: 0x0004B9D6 File Offset: 0x00049BD6
	private void Awake()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
		this.seed = new Vector2(10000f * Random.value, 20000f * Random.value);
	}

	// Token: 0x06000FDA RID: 4058 RVA: 0x0004BA05 File Offset: 0x00049C05
	private void Start()
	{
		this.initialPosition = this.rectTransform.anchoredPosition;
	}

	// Token: 0x06000FDB RID: 4059 RVA: 0x0004BA18 File Offset: 0x00049C18
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

	// Token: 0x040014B9 RID: 5305
	public float speed = 0.5f;

	// Token: 0x040014BA RID: 5306
	public float magnitude = 10f;

	// Token: 0x040014BB RID: 5307
	private RectTransform rectTransform;

	// Token: 0x040014BC RID: 5308
	private Vector2 seed;

	// Token: 0x040014BD RID: 5309
	public bool pixelPerfect = true;

	// Token: 0x040014BE RID: 5310
	public bool lowFramerate;

	// Token: 0x040014BF RID: 5311
	[ConditionalHide("lowFramerate", true)]
	public float framerate;

	// Token: 0x040014C0 RID: 5312
	private float updateTime = -1f;

	// Token: 0x040014C1 RID: 5313
	private Vector2 initialPosition;
}
