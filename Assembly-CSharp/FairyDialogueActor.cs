using System;
using UnityEngine;

// Token: 0x020000F1 RID: 241
public class FairyDialogueActor : DialogueActor
{
	// Token: 0x0600048C RID: 1164 RVA: 0x0002B004 File Offset: 0x00029204
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

	// Token: 0x0600048D RID: 1165 RVA: 0x0002B090 File Offset: 0x00029290
	private Vector3 GetPerlinOffset()
	{
		float num = this.perlinSpeed * Time.time;
		Vector3 vector;
		vector..ctor(Mathf.PerlinNoise(this.perlinSeed, num), Mathf.PerlinNoise(this.perlinSeed, 10000f + num), Mathf.PerlinNoise(this.perlinSeed, 20000f + num));
		vector *= 2f;
		vector -= Vector3.one;
		return this.perlinRadius * Vector3.ClampMagnitude(vector, 1f);
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x0002B110 File Offset: 0x00029310
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

	// Token: 0x0600048F RID: 1167 RVA: 0x0002B200 File Offset: 0x00029400
	protected override void UpdateMouthFlap()
	{
		float num = Mathf.SmoothStep(0f, 1f, this.mouthControl);
		this.fairySprite.transform.localScale = Mathf.Lerp(this.mouthClosedSize, this.mouthOpenSize, this.mouth * num) * Vector3.one;
		this.fairySprite.color = Color.Lerp(this.mouthClosedColor, this.mouthOpenColor, this.mouth * num);
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x000054D5 File Offset: 0x000036D5
	public override void MouthOpen()
	{
		this.mouthOpen = true;
		base.enabled = true;
		this.mouthOpenTime = Time.time;
		PlayAudio.p.PlayVoice(base.transform.position, this.voicePitchMultiplier, this.voiceVarianceMultiplier);
	}

	// Token: 0x0400066B RID: 1643
	[Header("Fairy Settings (Ignore everything above)")]
	public SpriteRenderer fairySprite;

	// Token: 0x0400066C RID: 1644
	public float mouthClosedSize = 0.2f;

	// Token: 0x0400066D RID: 1645
	public float mouthOpenSize = 0.5f;

	// Token: 0x0400066E RID: 1646
	public Color mouthClosedColor = Color.grey;

	// Token: 0x0400066F RID: 1647
	public Color mouthOpenColor = Color.white;

	// Token: 0x04000670 RID: 1648
	public float perlinRadius = 0.5f;

	// Token: 0x04000671 RID: 1649
	public float perlinSpeed = 0.25f;

	// Token: 0x04000672 RID: 1650
	private float perlinSeed;

	// Token: 0x04000673 RID: 1651
	public float acceleration = 5f;

	// Token: 0x04000674 RID: 1652
	public float smoothTime = 0.2f;

	// Token: 0x04000675 RID: 1653
	private const float maxSpeed = 15f;

	// Token: 0x04000676 RID: 1654
	private const float maxDistance = 5f;

	// Token: 0x04000677 RID: 1655
	private Vector3 anchorPoint;

	// Token: 0x04000678 RID: 1656
	public Vector3 talkingAnchorPoint;

	// Token: 0x04000679 RID: 1657
	private Vector3 position;

	// Token: 0x0400067A RID: 1658
	private Vector3 velocity;

	// Token: 0x0400067B RID: 1659
	private Transform originalParent;
}
