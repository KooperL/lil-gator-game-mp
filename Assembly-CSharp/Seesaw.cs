using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000254 RID: 596
public class Seesaw : MonoBehaviour
{
	// Token: 0x06000CDB RID: 3291 RVA: 0x0003E2BA File Offset: 0x0003C4BA
	private float ClosestStaticAngle(float dynamicAngle)
	{
		if (dynamicAngle < 0f)
		{
			return this.angle1;
		}
		return this.angle2;
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x0003E2D4 File Offset: 0x0003C4D4
	private void Start()
	{
		this.angle1 = this.static1.transform.localRotation.eulerAngles.z;
		if (this.angle1 > 180f)
		{
			this.angle1 -= 360f;
		}
		this.angle2 = this.static2.transform.localRotation.eulerAngles.z;
		if (this.angle2 > 180f)
		{
			this.angle2 -= 360f;
		}
		if (this.angle2 < this.angle1)
		{
			float num = this.angle2;
			this.angle2 = this.angle1;
			this.angle1 = num;
			Renderer renderer = this.static2;
			this.static2 = this.static1;
			this.static1 = renderer;
		}
		this.angle = this.dynamic.transform.localRotation.eulerAngles.z;
		this.angle = this.ClosestStaticAngle(this.angle);
		this.UpdateState();
		this.balanceBeam.onEnable.AddListener(new UnityAction(this.OnBeamEnabled));
		this.mount1.onMount.AddListener(new UnityAction(this.OnMount1Enabled));
		this.mount2.onMount.AddListener(new UnityAction(this.OnMount2Enabled));
		base.enabled = false;
	}

	// Token: 0x06000CDD RID: 3293 RVA: 0x0003E440 File Offset: 0x0003C640
	private void OnDisable()
	{
		this.prevAngularVelocity = (this.angularVelocity = 0f);
	}

	// Token: 0x06000CDE RID: 3294 RVA: 0x0003E461 File Offset: 0x0003C661
	private void TrySqueakEffect()
	{
		if (this.squeakEffect != null && Time.time - this.lastSqueakEffectTime > 0.5f)
		{
			this.lastSqueakEffectTime = Time.time;
			this.squeakEffect.Play();
		}
	}

	// Token: 0x06000CDF RID: 3295 RVA: 0x0003E49A File Offset: 0x0003C69A
	public void OnBeamEnabled()
	{
		this.DoInitialPush(Player.RawPosition);
		base.enabled = true;
	}

	// Token: 0x06000CE0 RID: 3296 RVA: 0x0003E4AE File Offset: 0x0003C6AE
	public void OnMount1Enabled()
	{
		this.DoInitialPush(this.mount1.transform.position);
		base.enabled = true;
	}

	// Token: 0x06000CE1 RID: 3297 RVA: 0x0003E4CD File Offset: 0x0003C6CD
	public void OnMount2Enabled()
	{
		this.DoInitialPush(this.mount2.transform.position);
		base.enabled = true;
	}

	// Token: 0x06000CE2 RID: 3298 RVA: 0x0003E4EC File Offset: 0x0003C6EC
	private void DoInitialPush(Vector3 position)
	{
		float positionOnBeam = this.GetPositionOnBeam(position);
		this.angularVelocity -= positionOnBeam * this.initialPush;
	}

	// Token: 0x06000CE3 RID: 3299 RVA: 0x0003E518 File Offset: 0x0003C718
	private float GetPositionOnBeam(Vector3 position)
	{
		Vector3 position2 = this.balanceBeam.GetPosition(0f);
		Vector3 position3 = this.balanceBeam.GetPosition(1f);
		Vector3 vector = Vector3.Lerp(position2, position3, 0.5f);
		Vector3 vector2 = position3 - vector;
		float magnitude = vector2.magnitude;
		vector2 /= magnitude;
		return Mathf.Clamp(Vector3.Dot(position - vector, vector2) / magnitude, -1f, 1f);
	}

	// Token: 0x06000CE4 RID: 3300 RVA: 0x0003E588 File Offset: 0x0003C788
	private void AddPushForPosition(Vector3 position)
	{
		this.AddPushForPosition(this.GetPositionOnBeam(Player.RawPosition));
	}

	// Token: 0x06000CE5 RID: 3301 RVA: 0x0003E59B File Offset: 0x0003C79B
	private void AddPushForPosition(float position)
	{
		this.angularVelocity -= Time.deltaTime * position * this.acceleration;
	}

	// Token: 0x06000CE6 RID: 3302 RVA: 0x0003E5B8 File Offset: 0x0003C7B8
	private void Update()
	{
		bool flag = false;
		if (this.balanceBeam.enabled)
		{
			flag = true;
			this.AddPushForPosition(Player.RawPosition);
		}
		if (this.mount1.isMounted)
		{
			flag = true;
			this.AddPushForPosition(-0.9f);
		}
		if (this.mount2.isMounted)
		{
			flag = true;
			this.AddPushForPosition(0.9f);
		}
		this.angle += this.angularVelocity * Time.deltaTime;
		this.UpdateState();
		if (Mathf.Abs(this.angularVelocity) > 5f && Mathf.Abs(this.prevAngularVelocity) <= 5f)
		{
			this.TrySqueakEffect();
		}
		this.prevAngularVelocity = this.angularVelocity;
		if (!flag && (this.angle == this.angle1 || this.angle == this.angle2))
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000CE7 RID: 3303 RVA: 0x0003E690 File Offset: 0x0003C890
	private void UpdateState()
	{
		this.angle = Mathf.Clamp(this.angle, this.angle1, this.angle2);
		if (this.angle == this.angle1)
		{
			this.angularVelocity = 0f;
			this.angle = this.angle1;
			this.dynamic.enabled = false;
			this.static1.enabled = true;
			this.static2.enabled = false;
		}
		else if (this.angle == this.angle2)
		{
			this.angularVelocity = 0f;
			this.angle = this.angle2;
			this.dynamic.enabled = false;
			this.static1.enabled = false;
			this.static2.enabled = true;
		}
		else
		{
			this.dynamic.enabled = true;
			this.static1.enabled = false;
			this.static2.enabled = false;
		}
		this.dynamic.transform.localRotation = Quaternion.Euler(0f, 0f, this.angle);
	}

	// Token: 0x040010FA RID: 4346
	public BalanceBeam balanceBeam;

	// Token: 0x040010FB RID: 4347
	public ActorMount mount1;

	// Token: 0x040010FC RID: 4348
	public ActorMount mount2;

	// Token: 0x040010FD RID: 4349
	public Renderer dynamic;

	// Token: 0x040010FE RID: 4350
	public Renderer static1;

	// Token: 0x040010FF RID: 4351
	public Renderer static2;

	// Token: 0x04001100 RID: 4352
	private float angle1;

	// Token: 0x04001101 RID: 4353
	private float angle2;

	// Token: 0x04001102 RID: 4354
	private float angle;

	// Token: 0x04001103 RID: 4355
	private float angularVelocity;

	// Token: 0x04001104 RID: 4356
	private float prevAngularVelocity;

	// Token: 0x04001105 RID: 4357
	public float acceleration = 70f;

	// Token: 0x04001106 RID: 4358
	public float initialPush = 30f;

	// Token: 0x04001107 RID: 4359
	public AudioSourceVariance squeakEffect;

	// Token: 0x04001108 RID: 4360
	private float lastSqueakEffectTime = -1f;
}
