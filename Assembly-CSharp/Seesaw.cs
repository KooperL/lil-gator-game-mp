using System;
using UnityEngine;
using UnityEngine.Events;

public class Seesaw : MonoBehaviour
{
	// Token: 0x06000FE3 RID: 4067 RVA: 0x0000DB2C File Offset: 0x0000BD2C
	private float ClosestStaticAngle(float dynamicAngle)
	{
		if (dynamicAngle < 0f)
		{
			return this.angle1;
		}
		return this.angle2;
	}

	// Token: 0x06000FE4 RID: 4068 RVA: 0x00052C54 File Offset: 0x00050E54
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

	// Token: 0x06000FE5 RID: 4069 RVA: 0x00052DC0 File Offset: 0x00050FC0
	private void OnDisable()
	{
		this.prevAngularVelocity = (this.angularVelocity = 0f);
	}

	// Token: 0x06000FE6 RID: 4070 RVA: 0x0000DB43 File Offset: 0x0000BD43
	private void TrySqueakEffect()
	{
		if (this.squeakEffect != null && Time.time - this.lastSqueakEffectTime > 0.5f)
		{
			this.lastSqueakEffectTime = Time.time;
			this.squeakEffect.Play();
		}
	}

	// Token: 0x06000FE7 RID: 4071 RVA: 0x0000DB7C File Offset: 0x0000BD7C
	public void OnBeamEnabled()
	{
		this.DoInitialPush(Player.RawPosition);
		base.enabled = true;
	}

	// Token: 0x06000FE8 RID: 4072 RVA: 0x0000DB90 File Offset: 0x0000BD90
	public void OnMount1Enabled()
	{
		this.DoInitialPush(this.mount1.transform.position);
		base.enabled = true;
	}

	// Token: 0x06000FE9 RID: 4073 RVA: 0x0000DBAF File Offset: 0x0000BDAF
	public void OnMount2Enabled()
	{
		this.DoInitialPush(this.mount2.transform.position);
		base.enabled = true;
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x00052DE4 File Offset: 0x00050FE4
	private void DoInitialPush(Vector3 position)
	{
		float positionOnBeam = this.GetPositionOnBeam(position);
		this.angularVelocity -= positionOnBeam * this.initialPush;
	}

	// Token: 0x06000FEB RID: 4075 RVA: 0x00052E10 File Offset: 0x00051010
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

	// Token: 0x06000FEC RID: 4076 RVA: 0x0000DBCE File Offset: 0x0000BDCE
	private void AddPushForPosition(Vector3 position)
	{
		this.AddPushForPosition(this.GetPositionOnBeam(Player.RawPosition));
	}

	// Token: 0x06000FED RID: 4077 RVA: 0x0000DBE1 File Offset: 0x0000BDE1
	private void AddPushForPosition(float position)
	{
		this.angularVelocity -= Time.deltaTime * position * this.acceleration;
	}

	// Token: 0x06000FEE RID: 4078 RVA: 0x00052E80 File Offset: 0x00051080
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

	// Token: 0x06000FEF RID: 4079 RVA: 0x00052F58 File Offset: 0x00051158
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

	public BalanceBeam balanceBeam;

	public ActorMount mount1;

	public ActorMount mount2;

	public Renderer dynamic;

	public Renderer static1;

	public Renderer static2;

	private float angle1;

	private float angle2;

	private float angle;

	private float angularVelocity;

	private float prevAngularVelocity;

	public float acceleration = 70f;

	public float initialPush = 30f;

	public AudioSourceVariance squeakEffect;

	private float lastSqueakEffectTime = -1f;
}
