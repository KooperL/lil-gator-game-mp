using System;
using UnityEngine;

// Token: 0x02000097 RID: 151
public class ActorPathFollower : MonoBehaviour, IManagedUpdate
{
	// Token: 0x0600021B RID: 539 RVA: 0x0001E098 File Offset: 0x0001C298
	private void OnValidate()
	{
		if (this.actor == null)
		{
			this.actor = base.GetComponent<DialogueActor>();
		}
		if (this.animator == null)
		{
			this.animator = base.GetComponent<Animator>();
		}
		if (!this.groundLayers.Contains(0))
		{
			this.groundLayers = LayerMask.GetMask(new string[] { "default", "Terrain", "Player Collision" });
		}
	}

	// Token: 0x0600021C RID: 540 RVA: 0x0001E118 File Offset: 0x0001C318
	private void Start()
	{
		this.scaledSpeed = this.speed / Mathf.Min(1f, base.transform.localScale.x);
		if (this.randomizeStartPosition)
		{
			this.nodePosition = Random.Range(0f, (float)(this.path.positions.Length - 1));
		}
		Vector3 interpolatedPosition = this.path.GetInterpolatedPosition(this.nodePosition);
		base.transform.position = interpolatedPosition;
		if (this.useTimeStep)
		{
			this.fromPosition = (this.toPosition = interpolatedPosition);
			this.DoNewTimeStep();
			this.t = 0f;
		}
	}

	// Token: 0x0600021D RID: 541 RVA: 0x00002229 File Offset: 0x00000429
	private void InitializePosition()
	{
	}

	// Token: 0x0600021E RID: 542 RVA: 0x0001E1BC File Offset: 0x0001C3BC
	private void OnEnable()
	{
		this.isInRange = this.path.SqrDistance(MainCamera.t.position) <= 3025f;
		if (this.isInRange)
		{
			FastUpdateManager.updateEvery1.Add(this);
			return;
		}
		FastUpdateManager.updateEveryNonFixed.Add(this);
	}

	// Token: 0x0600021F RID: 543 RVA: 0x00003BBE File Offset: 0x00001DBE
	private void OnDisable()
	{
		if (this.isInRange)
		{
			FastUpdateManager.updateEvery1.Remove(this);
			return;
		}
		FastUpdateManager.updateEveryNonFixed.Remove(this);
	}

	// Token: 0x06000220 RID: 544 RVA: 0x00003BE1 File Offset: 0x00001DE1
	private bool IsMoving()
	{
		return (!this.stopWhileTalking || !this.actor.IsInDialogue) && this.moving;
	}

	// Token: 0x06000221 RID: 545 RVA: 0x0001E210 File Offset: 0x0001C410
	public void ManagedUpdate()
	{
		if (this.isInRange != this.path.SqrDistance(MainCamera.t.position) <= 3025f)
		{
			this.isInRange = !this.isInRange;
			if (this.isInRange)
			{
				FastUpdateManager.updateEveryNonFixed.Remove(this);
				FastUpdateManager.updateEvery1.Add(this);
			}
			else
			{
				FastUpdateManager.updateEveryNonFixed.Add(this);
				FastUpdateManager.updateEvery1.Remove(this);
			}
		}
		if (!this.isInRange)
		{
			return;
		}
		float num;
		if (this.IsMoving())
		{
			if (this.waitAtNodeCounter > 0f)
			{
				this.waitAtNodeCounter -= Time.deltaTime;
				num = 0f;
			}
			else
			{
				num = this.speed;
			}
		}
		else
		{
			num = 0f;
		}
		this.smoothSpeed = Mathf.MoveTowards(this.smoothSpeed, num, this.speed * 4f * Time.deltaTime);
		if (this.smoothSpeed > 0f)
		{
			Vector3 position = base.transform.position;
			Vector3 vector;
			Vector3 vector2;
			this.path.Interpolate(ref this.nodePosition, this.smoothSpeed, out vector, out vector2, false);
			if (this.snapToGround)
			{
				RaycastHit raycastHit;
				float num2;
				if ((Player.Position - base.transform.position).magnitude < 30f && Physics.Raycast(vector2 + 0.5f * Vector3.up, Vector3.down, ref raycastHit, 2f, this.groundLayers))
				{
					num2 = raycastHit.point.y - vector2.y;
				}
				else
				{
					num2 = 0f;
				}
				this.smoothGroundOffset = Mathf.SmoothDamp(this.smoothGroundOffset, num2, ref this.smoothGroundVelocity, 0.2f);
				vector2.y += this.smoothGroundOffset;
			}
			base.transform.position = vector2;
			Vector3 vector3 = (vector2 - position).Flat();
			base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, Quaternion.LookRotation(vector3), 720f * Time.deltaTime);
			if (this.waitAtNode > 0f && Mathf.Floor(this.oldNodePosition) != Mathf.Floor(this.nodePosition) && Random.value <= this.waitAtNodeChance)
			{
				this.waitAtNodeCounter = this.waitAtNode;
			}
			this.oldNodePosition = this.nodePosition;
		}
		float num3 = Mathf.InverseLerp(0f, this.speed, this.smoothSpeed);
		float num4 = num3 * this.scaledSpeed;
		this.animator.SetFloat(this.speedID, num4);
		this.animator.SetLayerWeight(1, num3);
	}

	// Token: 0x06000222 RID: 546 RVA: 0x0001E4B8 File Offset: 0x0001C6B8
	private void DoNewTimeStep()
	{
		this.fromPosition = this.toPosition;
		this.path.AddDistance(ref this.nodePosition, this.speed * this.timeStep);
		this.toPosition = this.path.GetInterpolatedPosition(this.nodePosition);
		Debug.DrawLine(this.toPosition + 0.5f * Vector3.up, this.toPosition + 1.5f * Vector3.down, Color.blue, this.timeStep);
		RaycastHit raycastHit;
		if (Physics.Raycast(this.toPosition + 0.5f * Vector3.up, Vector3.down, ref raycastHit, 2f, this.groundLayers))
		{
			this.toPosition = raycastHit.point;
		}
		this.direction = this.toPosition - this.fromPosition;
	}

	// Token: 0x040002F7 RID: 759
	private const int movementLayer = 1;

	// Token: 0x040002F8 RID: 760
	private readonly int speedID = Animator.StringToHash("Speed");

	// Token: 0x040002F9 RID: 761
	public DialogueActor actor;

	// Token: 0x040002FA RID: 762
	public Animator animator;

	// Token: 0x040002FB RID: 763
	public ActorPath path;

	// Token: 0x040002FC RID: 764
	public float speed;

	// Token: 0x040002FD RID: 765
	private float scaledSpeed;

	// Token: 0x040002FE RID: 766
	private float smoothSpeed;

	// Token: 0x040002FF RID: 767
	public float waitAtNode;

	// Token: 0x04000300 RID: 768
	public float waitAtNodeChance = 1f;

	// Token: 0x04000301 RID: 769
	private float waitAtNodeCounter = -1f;

	// Token: 0x04000302 RID: 770
	private float oldNodePosition;

	// Token: 0x04000303 RID: 771
	public float nodePosition;

	// Token: 0x04000304 RID: 772
	private const float rotationSpeed = 720f;

	// Token: 0x04000305 RID: 773
	public bool snapToGround = true;

	// Token: 0x04000306 RID: 774
	private float smoothGroundOffset;

	// Token: 0x04000307 RID: 775
	private float smoothGroundVelocity;

	// Token: 0x04000308 RID: 776
	public LayerMask groundLayers;

	// Token: 0x04000309 RID: 777
	private Vector3 velocity;

	// Token: 0x0400030A RID: 778
	public bool useTimeStep = true;

	// Token: 0x0400030B RID: 779
	public float timeStep = 0.25f;

	// Token: 0x0400030C RID: 780
	private float t = 1f;

	// Token: 0x0400030D RID: 781
	private Vector3 fromPosition;

	// Token: 0x0400030E RID: 782
	private Vector3 toPosition;

	// Token: 0x0400030F RID: 783
	private Vector3 direction;

	// Token: 0x04000310 RID: 784
	public bool moving = true;

	// Token: 0x04000311 RID: 785
	public bool stopWhileTalking = true;

	// Token: 0x04000312 RID: 786
	public bool randomizeStartPosition = true;

	// Token: 0x04000313 RID: 787
	private bool isInRange;

	// Token: 0x04000314 RID: 788
	private const float accM = 4f;
}
