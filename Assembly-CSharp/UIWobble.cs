using System;
using UnityEngine;

// Token: 0x020003DC RID: 988
public class UIWobble : MonoBehaviour
{
	// Token: 0x06001305 RID: 4869 RVA: 0x00010252 File Offset: 0x0000E452
	private void Awake()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
		this.seed = new Vector2(10000f * Random.value, 20000f * Random.value);
	}

	// Token: 0x06001306 RID: 4870 RVA: 0x00010281 File Offset: 0x0000E481
	private void Start()
	{
		this.initialPosition = this.rectTransform.anchoredPosition;
	}

	// Token: 0x06001307 RID: 4871 RVA: 0x0005CEF4 File Offset: 0x0005B0F4
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
		Vector2 vector;
		vector..ctor(Mathf.PerlinNoise(this.seed.x, Time.time * this.speed) - 0.5f, Mathf.PerlinNoise(this.seed.y, Time.time * this.speed) - 0.5f);
		Vector2 vector2 = this.initialPosition + vector * this.magnitude;
		if (this.pixelPerfect)
		{
			vector2.x = Mathf.Round(vector2.x);
			vector2.y = Mathf.Round(vector2.y);
		}
		this.rectTransform.anchoredPosition = vector2;
	}

	// Token: 0x04001878 RID: 6264
	public float speed = 0.5f;

	// Token: 0x04001879 RID: 6265
	public float magnitude = 10f;

	// Token: 0x0400187A RID: 6266
	private RectTransform rectTransform;

	// Token: 0x0400187B RID: 6267
	private Vector2 seed;

	// Token: 0x0400187C RID: 6268
	public bool pixelPerfect = true;

	// Token: 0x0400187D RID: 6269
	public bool lowFramerate;

	// Token: 0x0400187E RID: 6270
	[ConditionalHide("lowFramerate", true)]
	public float framerate;

	// Token: 0x0400187F RID: 6271
	private float updateTime = -1f;

	// Token: 0x04001880 RID: 6272
	private Vector2 initialPosition;
}
