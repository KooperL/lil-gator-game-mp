using System;
using UnityEngine;

// Token: 0x020000B3 RID: 179
public class FairyDialogueActor : DialogueActor
{
	// Token: 0x060003DF RID: 991 RVA: 0x00016C44 File Offset: 0x00014E44
	protected override void Start()
	{
		this.perlinSeed = 1000f * Random.value;
		this.anchorPoint = base.transform.localPosition;
		this.position = base.transform.position;
		this.mouth = 0f;
		this.UpdateMouthFlap();
		base.Start();
		base.enabled = true;
		this.originalParent = base.transform.parent;
		base.transform.parent = base.transform.parent.parent;
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x00016CD0 File Offset: 0x00014ED0
	private Vector3 GetPerlinOffset()
	{
		float num = this.perlinSpeed * Time.time;
		Vector3 vector = new Vector3(Mathf.PerlinNoise(this.perlinSeed, num), Mathf.PerlinNoise(this.perlinSeed, 10000f + num), Mathf.PerlinNoise(this.perlinSeed, 20000f + num));
		vector *= 2f;
		vector -= Vector3.one;
		return this.perlinRadius * Vector3.ClampMagnitude(vector, 1f);
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x00016D50 File Offset: 0x00014F50
	public override void LateUpdate()
	{
		base.LateUpdate();
		base.enabled = true;
		Vector3 vector = this.originalParent.TransformPoint(this.anchorPoint) + this.GetPerlinOffset();
		float num = this.smoothTime;
		float num2 = this.acceleration;
		if (DialogueManager.currentlySpeakingActor == this || DialogueManager.currentlySpeakingBubbleActor == this)
		{
			vector = this.originalParent.TransformPoint(this.talkingAnchorPoint);
			num = Mathf.Min(num, 0.2f);
			this.acceleration = Mathf.Max(this.acceleration, 20f);
		}
		this.position = Vector3.MoveTowards(vector, this.position, 5f);
		this.position = MathUtils.SmoothDampAcc(this.position, vector, ref this.velocity, num, this.acceleration);
		this.velocity = Vector3.ClampMagnitude(this.velocity, 15f);
		base.transform.position = this.position;
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x00016E40 File Offset: 0x00015040
	protected override void UpdateMouthFlap()
	{
		float num = Mathf.SmoothStep(0f, 1f, this.mouthControl);
		this.fairySprite.transform.localScale = Mathf.Lerp(this.mouthClosedSize, this.mouthOpenSize, this.mouth * num) * Vector3.one;
		this.fairySprite.color = Color.Lerp(this.mouthClosedColor, this.mouthOpenColor, this.mouth * num);
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x00016EBA File Offset: 0x000150BA
	public override void MouthOpen()
	{
		this.mouthOpen = true;
		base.enabled = true;
		this.mouthOpenTime = Time.time;
		PlayAudio.p.PlayVoice(base.transform.position, this.voicePitchMultiplier, this.voiceVarianceMultiplier);
	}

	// Token: 0x0400055C RID: 1372
	[Header("Fairy Settings (Ignore everything above)")]
	public SpriteRenderer fairySprite;

	// Token: 0x0400055D RID: 1373
	public float mouthClosedSize = 0.2f;

	// Token: 0x0400055E RID: 1374
	public float mouthOpenSize = 0.5f;

	// Token: 0x0400055F RID: 1375
	public Color mouthClosedColor = Color.grey;

	// Token: 0x04000560 RID: 1376
	public Color mouthOpenColor = Color.white;

	// Token: 0x04000561 RID: 1377
	public float perlinRadius = 0.5f;

	// Token: 0x04000562 RID: 1378
	public float perlinSpeed = 0.25f;

	// Token: 0x04000563 RID: 1379
	private float perlinSeed;

	// Token: 0x04000564 RID: 1380
	public float acceleration = 5f;

	// Token: 0x04000565 RID: 1381
	public float smoothTime = 0.2f;

	// Token: 0x04000566 RID: 1382
	private const float maxSpeed = 15f;

	// Token: 0x04000567 RID: 1383
	private const float maxDistance = 5f;

	// Token: 0x04000568 RID: 1384
	private Vector3 anchorPoint;

	// Token: 0x04000569 RID: 1385
	public Vector3 talkingAnchorPoint;

	// Token: 0x0400056A RID: 1386
	private Vector3 position;

	// Token: 0x0400056B RID: 1387
	private Vector3 velocity;

	// Token: 0x0400056C RID: 1388
	private Transform originalParent;
}
