using System;

namespace Rewired
{
	public interface IGamepadTemplate : IControllerTemplate
	{
		// (get) Token: 0x06001038 RID: 4152
		IControllerTemplateButton actionBottomRow1 { get; }

		// (get) Token: 0x06001039 RID: 4153
		IControllerTemplateButton a { get; }

		// (get) Token: 0x0600103A RID: 4154
		IControllerTemplateButton actionBottomRow2 { get; }

		// (get) Token: 0x0600103B RID: 4155
		IControllerTemplateButton b { get; }

		// (get) Token: 0x0600103C RID: 4156
		IControllerTemplateButton actionBottomRow3 { get; }

		// (get) Token: 0x0600103D RID: 4157
		IControllerTemplateButton c { get; }

		// (get) Token: 0x0600103E RID: 4158
		IControllerTemplateButton actionTopRow1 { get; }

		// (get) Token: 0x0600103F RID: 4159
		IControllerTemplateButton x { get; }

		// (get) Token: 0x06001040 RID: 4160
		IControllerTemplateButton actionTopRow2 { get; }

		// (get) Token: 0x06001041 RID: 4161
		IControllerTemplateButton y { get; }

		// (get) Token: 0x06001042 RID: 4162
		IControllerTemplateButton actionTopRow3 { get; }

		// (get) Token: 0x06001043 RID: 4163
		IControllerTemplateButton z { get; }

		// (get) Token: 0x06001044 RID: 4164
		IControllerTemplateButton leftShoulder1 { get; }

		// (get) Token: 0x06001045 RID: 4165
		IControllerTemplateButton leftBumper { get; }

		// (get) Token: 0x06001046 RID: 4166
		IControllerTemplateAxis leftShoulder2 { get; }

		// (get) Token: 0x06001047 RID: 4167
		IControllerTemplateAxis leftTrigger { get; }

		// (get) Token: 0x06001048 RID: 4168
		IControllerTemplateButton rightShoulder1 { get; }

		// (get) Token: 0x06001049 RID: 4169
		IControllerTemplateButton rightBumper { get; }

		// (get) Token: 0x0600104A RID: 4170
		IControllerTemplateAxis rightShoulder2 { get; }

		// (get) Token: 0x0600104B RID: 4171
		IControllerTemplateAxis rightTrigger { get; }

		// (get) Token: 0x0600104C RID: 4172
		IControllerTemplateButton center1 { get; }

		// (get) Token: 0x0600104D RID: 4173
		IControllerTemplateButton back { get; }

		// (get) Token: 0x0600104E RID: 4174
		IControllerTemplateButton center2 { get; }

		// (get) Token: 0x0600104F RID: 4175
		IControllerTemplateButton start { get; }

		// (get) Token: 0x06001050 RID: 4176
		IControllerTemplateButton center3 { get; }

		// (get) Token: 0x06001051 RID: 4177
		IControllerTemplateButton guide { get; }

		// (get) Token: 0x06001052 RID: 4178
		IControllerTemplateThumbStick leftStick { get; }

		// (get) Token: 0x06001053 RID: 4179
		IControllerTemplateThumbStick rightStick { get; }

		// (get) Token: 0x06001054 RID: 4180
		IControllerTemplateDPad dPad { get; }
	}
}
