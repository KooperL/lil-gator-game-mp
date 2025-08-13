using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200031A RID: 794
public class Seesaw : MonoBehaviour
{
	// Token: 0x06000F88 RID: 3976 RVA: 0x0000D7D8 File Offset: 0x0000B9D8
	private float ClosestStaticAngle(float dynamicAngle)
	{
		if (dynamicAngle < 0f)
		{
			return this.angle1;
		}
		return this.angle2;
	}

	// Token: 0x06000F89 RID: 3977 RVA: 0x00050EC4 File Offset: 0x0004F0C4
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

	// Token: 0x06000F8A RID: 3978 RVA: 0x00051030 File Offset: 0x0004F230
	private void OnDisable()
	{
		this.prevAngularVelocity = (this.angularVelocity = 0f);
	}

	// Token: 0x06000F8B RID: 3979 RVA: 0x0000D7EF File Offset: 0x0000B9EF
	private void TrySqueakEffect()
	{
		if (this.squeakEffect != null && Time.time - this.lastSqueakEffectTime > 0.5f)
		{
			this.lastSqueakEffectTime = Time.time;
			this.squeakEffect.Play();
		}
	}

	// Token: 0x06000F8C RID: 3980 RVA: 0x0000D828 File Offset: 0x0000BA28
	public void OnBeamEnabled()
	{
		this.DoInitialPush(Player.RawPosition);
		base.enabled = true;
	}

	// Token: 0x06000F8D RID: 3981 RVA: 0x0000D83C File Offset: 0x0000BA3C
	public void OnMount1Enabled()
	{
		this.DoInitialPush(this.mount1.transform.position);
		base.enabled = true;
	}

	// Token: 0x06000F8E RID: 3982 RVA: 0x0000D85B File Offset: 0x0000BA5B
	public void OnMount2Enabled()
	{
		this.DoInitialPush(this.mount2.transform.position);
		base.enabled = true;
	}

	// Token: 0x06000F8F RID: 3983 RVA: 0x00051054 File Offset: 0x0004F254
	private void DoInitialPush(Vector3 position)
	{
		float positionOnBeam = this.GetPositionOnBeam(position);
		this.angularVelocity -= positionOnBeam * this.initialPush;
	}

	// Token: 0x06000F90 RID: 3984 RVA: 0x00051080 File Offset: 0x0004F280
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

	// Token: 0x06000F91 RID: 3985 RVA: 0x0000D87A File Offset: 0x0000BA7A
	private void AddPushForPosition(Vector3 position)
	{
		this.AddPushForPosition(this.GetPositionOnBeam(Player.RawPosition));
	}

	// Token: 0x06000F92 RID: 3986 RVA: 0x0000D88D File Offset: 0x0000BA8D
	private void AddPushForPosition(float position)
	{
		this.angularVelocity -= Time.deltaTime * position * this.acceleration;
	}

	// Token: 0x06000F93 RID: 3987 RVA: 0x000510F0 File Offset: 0x0004F2F0
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

	// Token: 0x06000F94 RID: 3988 RVA: 0x000511C8 File Offset: 0x0004F3C8
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

	// Token: 0x04001416 RID: 5142
	public BalanceBeam balanceBeam;

	// Token: 0x04001417 RID: 5143
	public ActorMount mount1;

	// Token: 0x04001418 RID: 5144
	public ActorMount mount2;

	// Token: 0x04001419 RID: 5145
	public Renderer dynamic;

	// Token: 0x0400141A RID: 5146
	public Renderer static1;

	// Token: 0x0400141B RID: 5147
	public Renderer static2;

	// Token: 0x0400141C RID: 5148
	private float angle1;

	// Token: 0x0400141D RID: 5149
	private float angle2;

	// Token: 0x0400141E RID: 5150
	private float angle;

	// Token: 0x0400141F RID: 5151
	private float angularVelocity;

	// Token: 0x04001420 RID: 5152
	private float prevAngularVelocity;

	// Token: 0x04001421 RID: 5153
	public float acceleration = 70f;

	// Token: 0x04001422 RID: 5154
	public float initialPush = 30f;

	// Token: 0x04001423 RID: 5155
	public AudioSourceVariance squeakEffect;

	// Token: 0x04001424 RID: 5156
	private float lastSqueakEffectTime = -1f;
}
