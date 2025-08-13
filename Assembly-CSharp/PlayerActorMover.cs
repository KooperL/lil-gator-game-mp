using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000F6 RID: 246
public class PlayerActorMover : MonoBehaviour, IActorMover, ICustomPlayerMovement
{
	// Token: 0x060004A6 RID: 1190 RVA: 0x000055CA File Offset: 0x000037CA
	private void OnDisable()
	{
		this.path.Clear();
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x0002B470 File Offset: 0x00029670
	public void MovementUpdate(Vector3 input, ref Vector3 position, ref Vector3 velocity, ref Vector3 direction, ref Vector3 up, ref float animationIndex)
	{
		if (this.pathArray == null)
		{
			base.enabled = false;
			return;
		}
		Vector3 vector = this.pathArray[this.pathArray.Length - 1];
		float num = Vector3.Distance(position, this.path[0]);
		float num2 = Vector3.Distance(position, vector);
		float num3 = Mathf.InverseLerp(0.5f, 0f, num2);
		if (this.t >= (float)(this.path.Count - 1))
		{
			position = vector;
			direction = this.markRotation * Vector3.forward;
			velocity = Vector3.zero;
			base.enabled = false;
			UnityEvent unityEvent = this.onReachMark;
			Player.movement.ClearMods();
			if (unityEvent != null)
			{
				unityEvent.Invoke();
			}
			this.onReachMark = new UnityEvent();
			return;
		}
		float num4 = Mathf.Max(0.05f, Mathf.Sqrt(2f * num2 * this.acc / 3f));
		this.smoothSpeed = Mathf.MoveTowards(this.smoothSpeed, this.targetSpeed, this.acc * Time.deltaTime);
		this.smoothSpeed = Mathf.Min(this.smoothSpeed, num4);
		this.t = this.MoveAlongPath(this.t, Time.deltaTime * this.smoothSpeed);
		position = iTween.PointOnPath(this.pathArray, this.t / (float)(this.pathArray.Length - 1));
		Vector3 vector2 = (position - this.lastPosition).normalized;
		this.lastPosition = position;
		velocity = this.smoothSpeed * vector2;
		if (this.snapMovementToGround)
		{
			LayerUtil.SnapToGround(ref position, 5f);
		}
		if (num3 > 0f)
		{
			vector2 = MathUtils.SlerpFlat(vector2, this.markRotation * Vector3.forward, num3, false);
		}
		if (num < 0.5f)
		{
			direction = MathUtils.SlerpFlat(direction, vector2, Mathf.InverseLerp(0f, 0.5f, num), false);
			return;
		}
		direction = vector2;
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x0002B690 File Offset: 0x00029890
	public float MoveAlongPath(float t, float distance)
	{
		if (distance == 0f)
		{
			return t;
		}
		if (t == 0f && distance < 0f)
		{
			return t;
		}
		if (t == (float)(this.path.Count - 1) && distance > 0f)
		{
			return t;
		}
		Vector3 vector = iTween.PointOnPath(this.pathArray, t / (float)(this.pathArray.Length - 1));
		Vector3 vector2 = iTween.PointOnPath(this.pathArray, (t + Mathf.Sign(distance) * 0.01f) / (float)(this.pathArray.Length - 1));
		float num = Vector3.Distance(vector, vector2);
		if (num == 0f)
		{
			return t;
		}
		float num2 = 0.01f / num;
		return Mathf.Clamp(t + distance * num2, 0f, (float)(this.path.Count - 1));
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x0002B748 File Offset: 0x00029948
	public void SetMark(Vector3[] positions, Quaternion rotation, float speed, UnityEvent onReachMark, bool skipToStart, bool disableInteraction = false, bool playFootsteps = true)
	{
		this.path.Clear();
		if (!skipToStart)
		{
			this.path.Add(base.transform.position);
		}
		this.onReachMark = onReachMark;
		if (speed == 0f)
		{
			this.targetSpeed = this.speed;
		}
		else
		{
			this.targetSpeed = speed;
		}
		this.path.AddRange(positions);
		if (this.snapPathToGround)
		{
			for (int i = 0; i < this.path.Count; i++)
			{
				Vector3 vector = this.path[i];
				LayerUtil.SnapToGround(ref vector, 5f);
				this.path[i] = vector;
			}
		}
		this.markRotation = rotation;
		this.pathArray = this.path.ToArray();
		if (this.mountedActor == null)
		{
			this.mountedActor = base.GetComponent<MountedActor>();
		}
		if (skipToStart)
		{
			if (this.mountedActor != null)
			{
				this.mountedActor.CancelMount();
			}
			Player.movement.SetPosition(positions[0]);
			Vector3 vector2 = iTween.PointOnPath(this.pathArray, 0.02f) - positions[0];
			Player.movement.SetRotation(Quaternion.LookRotation(vector2.Flat()));
			this.StartMoving();
			return;
		}
		if (this.mountedActor == null)
		{
			this.StartMoving();
			return;
		}
		this.mountedActor.onGottenOut.AddListener(new UnityAction(this.StartMoving));
		this.mountedActor.GetOut();
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x0002B8C8 File Offset: 0x00029AC8
	private void StartMoving()
	{
		this.path[0] = (this.pathArray[0] = base.transform.position);
		this.t = 0f;
		this.smoothSpeed = 0f;
		this.lastPosition = base.transform.position;
		Player.movement.isModified = true;
		Player.movement.modIgnoreReady = true;
		Player.movement.modNoInteractions = true;
		Player.movement.moddedWithoutControl = true;
		Player.movement.modCustomMovement = true;
		Player.movement.modCustomMovementScript = this;
		Player.movement.modDisableCollision = true;
		Player.movement.modNoClimbing = true;
		Player.movement.modJumpRule = PlayerMovement.ModRule.Locked;
		Player.movement.modGlideRule = PlayerMovement.ModRule.Locked;
		Player.movement.modSecondaryRule = PlayerMovement.ModRule.Locked;
		Player.movement.modPrimaryRule = PlayerMovement.ModRule.Allowed;
		Player.movement.modItemRule = PlayerMovement.ModRule.Locked;
		Player.movement.modIgnoreGroundedness = true;
		Player.actor.Position = 0;
		base.enabled = true;
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x000055D7 File Offset: 0x000037D7
	public void Cancel()
	{
		this.onReachMark = new UnityEvent();
		base.enabled = false;
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x000055EB File Offset: 0x000037EB
	public void CancelMove()
	{
		if (!base.enabled)
		{
			return;
		}
		Player.movement.ClearMods();
	}

	// Token: 0x04000689 RID: 1673
	private MountedActor mountedActor;

	// Token: 0x0400068A RID: 1674
	public bool snapPathToGround = true;

	// Token: 0x0400068B RID: 1675
	public bool snapMovementToGround;

	// Token: 0x0400068C RID: 1676
	public float speed = 4f;

	// Token: 0x0400068D RID: 1677
	public float acc = 15f;

	// Token: 0x0400068E RID: 1678
	private float targetSpeed;

	// Token: 0x0400068F RID: 1679
	private float smoothSpeed;

	// Token: 0x04000690 RID: 1680
	private List<Vector3> path = new List<Vector3>();

	// Token: 0x04000691 RID: 1681
	private Vector3[] pathArray;

	// Token: 0x04000692 RID: 1682
	private Quaternion markRotation;

	// Token: 0x04000693 RID: 1683
	private float t;

	// Token: 0x04000694 RID: 1684
	private const float targetRotationLerpDistance = 0.5f;

	// Token: 0x04000695 RID: 1685
	private Vector3 lastPosition;

	// Token: 0x04000696 RID: 1686
	private UnityEvent onReachMark;

	// Token: 0x04000697 RID: 1687
	private const float speedStepProbe = 0.01f;
}
