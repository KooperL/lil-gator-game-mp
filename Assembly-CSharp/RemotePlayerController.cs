using System;
using System.Collections.Generic;
using UnityEngine;

public class RemotePlayerController : MonoBehaviour
{
	public void PushState(Vector3 pos, Quaternion rot, Vector3 vel, double remoteTime)
	{
		int num = this._buffer.Count;
		while (num > 0 && this._buffer[num - 1].remoteTime > remoteTime)
		{
			num--;
		}
		this._buffer.Insert(num, new RemotePlayerController.NetState
		{
			pos = pos,
			rot = rot,
			vel = vel,
			remoteTime = remoteTime
		});
		if (this._buffer.Count > 64)
		{
			this._buffer.RemoveAt(0);
		}
	}

	private void Awake()
	{
		if (!this.animator)
		{
			this.animator = base.GetComponentInChildren<Animator>();
		}
		if (!this.rb)
		{
			this.rb = base.GetComponent<Rigidbody>();
		}
		if (this.rb)
		{
			this.rb.isKinematic = true;
			this.rb.interpolation = RigidbodyInterpolation.Interpolate;
		}
	}

	private void Update()
	{
		if (this._buffer.Count == 0)
		{
			return;
		}
		double num = Time.unscaledTimeAsDouble - (double)this.interpolationBackTime;
		while (this._buffer.Count >= 2 && this._buffer[1].remoteTime <= num)
		{
			this._buffer.RemoveAt(0);
		}
		Vector3 vector;
		Quaternion quaternion;
		if (this._buffer.Count >= 2 && this._buffer[0].remoteTime <= num && num <= this._buffer[1].remoteTime)
		{
			RemotePlayerController.NetState netState = this._buffer[0];
			RemotePlayerController.NetState netState2 = this._buffer[1];
			float num2 = 0f;
			double num3 = netState2.remoteTime - netState.remoteTime;
			if (num3 > 1E-05)
			{
				num2 = (float)((num - netState.remoteTime) / num3);
			}
			vector = Vector3.LerpUnclamped(netState.pos, netState2.pos, num2);
			quaternion = Quaternion.Slerp(netState.rot, netState2.rot, num2);
		}
		else
		{
			RemotePlayerController.NetState netState3 = this._buffer[this._buffer.Count - 1];
			float num4 = Mathf.Clamp((float)(num - netState3.remoteTime), -0.25f, 0.25f);
			vector = netState3.pos + netState3.vel * num4;
			quaternion = netState3.rot;
		}
		if (Vector3.Distance(base.transform.position, vector) > this.snapDistance || !this._hasVisualPos)
		{
			this.SetTransform(vector, quaternion);
			this._lastVisualPos = vector;
			this._hasVisualPos = true;
			this.UpdateAnimator(0f);
			return;
		}
		float num5 = this.maxChaseSpeed * Time.deltaTime;
		Vector3 vector2 = Vector3.MoveTowards(base.transform.position, vector, num5);
		float num6 = this.rotationDegreesPerSec * Time.deltaTime;
		Quaternion quaternion2 = Quaternion.RotateTowards(base.transform.rotation, quaternion, num6);
		this.SetTransform(vector2, quaternion2);
		Vector3 vector3 = (vector2 - this._lastVisualPos) / Mathf.Max(Time.deltaTime, 1E-05f);
		vector3.y = 0f;
		this.UpdateAnimator(vector3.magnitude);
		this._lastVisualPos = vector2;
	}

	private void SetTransform(Vector3 pos, Quaternion rot)
	{
		if (this.rb)
		{
			this.rb.MovePosition(pos);
			this.rb.MoveRotation(rot);
			return;
		}
		base.transform.SetPositionAndRotation(pos, rot);
	}

	private void UpdateAnimator(float speed)
	{
		if (!this.animator)
		{
			return;
		}
		this.animator.SetFloat(this.speedParam, speed);
		if (!string.IsNullOrEmpty(this.movingBoolParam))
		{
			this.animator.SetBool(this.movingBoolParam, speed > this.movingThreshold);
		}
	}

	[Header("References")]
	public Animator animator;

	public Rigidbody rb;

	[Header("Smoothing")]
	[Tooltip("How far behind real-time to interpolate (sec). 0.1–0.2 is typical.")]
	public float interpolationBackTime = 0.12f;

	[Tooltip("Snap instantly if we drift further than this (meters).")]
	public float snapDistance = 5f;

	[Tooltip("Max move speed used to approach target (m/s).")]
	public float maxChaseSpeed = 20f;

	[Tooltip("How quickly to slerp rotation toward target (deg/sec).")]
	public float rotationDegreesPerSec = 360f;

	[Header("Animation")]
	public string speedParam = "Speed";

	public string movingBoolParam = "IsMoving";

	public float movingThreshold = 0.05f;

	private readonly List<RemotePlayerController.NetState> _buffer = new List<RemotePlayerController.NetState>(64);

	private Vector3 _lastVisualPos;

	private bool _hasVisualPos;

	private struct NetState
	{
		public double remoteTime;

		public Vector3 pos;

		public Quaternion rot;

		public Vector3 vel;
	}
}
