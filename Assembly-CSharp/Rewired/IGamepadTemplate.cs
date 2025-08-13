using System;

namespace Rewired
{
	// Token: 0x02000302 RID: 770
	public interface IGamepadTemplate : IControllerTemplate
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06001038 RID: 4152
		IControllerTemplateButton actionBottomRow1 { get; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06001039 RID: 4153
		IControllerTemplateButton a { get; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600103A RID: 4154
		IControllerTemplateButton actionBottomRow2 { get; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600103B RID: 4155
		IControllerTemplateButton b { get; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600103C RID: 4156
		IControllerTemplateButton actionBottomRow3 { get; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600103D RID: 4157
		IControllerTemplateButton c { get; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600103E RID: 4158
		IControllerTemplateButton actionTopRow1 { get; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600103F RID: 4159
		IControllerTemplateButton x { get; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06001040 RID: 4160
		IControllerTemplateButton actionTopRow2 { get; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06001041 RID: 4161
		IControllerTemplateButton y { get; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06001042 RID: 4162
		IControllerTemplateButton actionTopRow3 { get; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06001043 RID: 4163
		IControllerTemplateButton z { get; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06001044 RID: 4164
		IControllerTemplateButton leftShoulder1 { get; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06001045 RID: 4165
		IControllerTemplateButton leftBumper { get; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06001046 RID: 4166
		IControllerTemplateAxis leftShoulder2 { get; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06001047 RID: 4167
		IControllerTemplateAxis leftTrigger { get; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06001048 RID: 4168
		IControllerTemplateButton rightShoulder1 { get; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06001049 RID: 4169
		IControllerTemplateButton rightBumper { get; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600104A RID: 4170
		IControllerTemplateAxis rightShoulder2 { get; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600104B RID: 4171
		IControllerTemplateAxis rightTrigger { get; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600104C RID: 4172
		IControllerTemplateButton center1 { get; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600104D RID: 4173
		IControllerTemplateButton back { get; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600104E RID: 4174
		IControllerTemplateButton center2 { get; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600104F RID: 4175
		IControllerTemplateButton start { get; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06001050 RID: 4176
		IControllerTemplateButton center3 { get; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06001051 RID: 4177
		IControllerTemplateButton guide { get; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06001052 RID: 4178
		IControllerTemplateThumbStick leftStick { get; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06001053 RID: 4179
		IControllerTemplateThumbStick rightStick { get; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06001054 RID: 4180
		IControllerTemplateDPad dPad { get; }
	}
}
