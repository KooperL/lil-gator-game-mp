using System;

public class LogicStateSubmerge : LogicState
{
	// Token: 0x06000929 RID: 2345 RVA: 0x00008DF6 File Offset: 0x00006FF6
	private void FixedUpdate()
	{
		if (Player.movement.IsSwimming)
		{
			this.swimmingCounter++;
		}
		else
		{
			this.swimmingCounter = 0;
		}
		if (this.swimmingCounter > 10)
		{
			base.LogicCompleted();
		}
	}

	private int swimmingCounter;
}
