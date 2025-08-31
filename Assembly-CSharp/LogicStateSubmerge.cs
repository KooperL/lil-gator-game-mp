using System;

public class LogicStateSubmerge : LogicState
{
	// Token: 0x06000789 RID: 1929 RVA: 0x00025346 File Offset: 0x00023546
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
