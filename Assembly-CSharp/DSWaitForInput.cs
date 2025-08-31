using System;
using System.Collections;
using Rewired;
using UnityEngine;

[AddComponentMenu("Dialogue Sequence/Wait For Input")]
public class DSWaitForInput : DialogueSequence
{
	// Token: 0x060004A8 RID: 1192 RVA: 0x00019C08 File Offset: 0x00017E08
	public override void Activate()
	{
		if (this.player == null)
		{
			this.player = ReInput.players.GetPlayer(0);
		}
		this.player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMove), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("Move Horizontal"));
		this.player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMove), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("Move Vertical"));
		this.player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnInteract), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("Interact"));
		this.player.AddInputEventDelegate(new Action<InputActionEventData>(this.Jump), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("Jump"));
		if (this.waitUntilTriggered == null)
		{
			this.waitUntilTriggered = new WaitUntil(() => this.isTriggered);
		}
		base.Activate();
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x00019CF4 File Offset: 0x00017EF4
	public override void Deactivate()
	{
		this.player.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnMove));
		this.player.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnInteract));
		this.player.RemoveInputEventDelegate(new Action<InputActionEventData>(this.Jump));
		base.Deactivate();
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x00019D4C File Offset: 0x00017F4C
	public override YieldInstruction Run()
	{
		return base.StartCoroutine(this.WaitUntilTriggered());
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x00019D5A File Offset: 0x00017F5A
	private IEnumerator WaitUntilTriggered()
	{
		this.isTriggered = false;
		while (!this.isTriggered)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x00019D69 File Offset: 0x00017F69
	private void OnInteract(InputActionEventData obj)
	{
		if (this.waitForInteract)
		{
			this.isTriggered = true;
		}
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x00019D7A File Offset: 0x00017F7A
	private void Jump(InputActionEventData obj)
	{
		if (this.waitForJump)
		{
			this.isTriggered = true;
		}
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x00019D8B File Offset: 0x00017F8B
	private void OnMove(InputActionEventData obj)
	{
		if (this.waitForMove)
		{
			this.isTriggered = true;
		}
	}

	private global::Rewired.Player player;

	public bool waitForMove;

	public bool waitForInteract;

	public bool waitForJump;

	private bool isTriggered;

	private WaitUntil waitUntilTriggered;
}
