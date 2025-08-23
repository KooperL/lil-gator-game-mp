using System;
using System.Collections;
using Rewired;
using UnityEngine;

[AddComponentMenu("Dialogue Sequence/Wait For Input")]
public class DSWaitForInput : DialogueSequence
{
	// Token: 0x060005DF RID: 1503 RVA: 0x0002FE0C File Offset: 0x0002E00C
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

	// Token: 0x060005E0 RID: 1504 RVA: 0x0002FEF8 File Offset: 0x0002E0F8
	public override void Deactivate()
	{
		this.player.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnMove));
		this.player.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnInteract));
		this.player.RemoveInputEventDelegate(new Action<InputActionEventData>(this.Jump));
		base.Deactivate();
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x000063A6 File Offset: 0x000045A6
	public override YieldInstruction Run()
	{
		return base.StartCoroutine(this.WaitUntilTriggered());
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x000063B4 File Offset: 0x000045B4
	private IEnumerator WaitUntilTriggered()
	{
		this.isTriggered = false;
		while (!this.isTriggered)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x000063C3 File Offset: 0x000045C3
	private void OnInteract(InputActionEventData obj)
	{
		if (this.waitForInteract)
		{
			this.isTriggered = true;
		}
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x000063D4 File Offset: 0x000045D4
	private void Jump(InputActionEventData obj)
	{
		if (this.waitForJump)
		{
			this.isTriggered = true;
		}
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x000063E5 File Offset: 0x000045E5
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
