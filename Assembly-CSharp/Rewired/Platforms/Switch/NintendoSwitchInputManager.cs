using System;
using System.Collections.Generic;
using Rewired.Utils.Interfaces;
using UnityEngine;

namespace Rewired.Platforms.Switch
{
	// Token: 0x02000408 RID: 1032
	[AddComponentMenu("Rewired/Nintendo Switch Input Manager")]
	[RequireComponent(typeof(InputManager))]
	public sealed class NintendoSwitchInputManager : MonoBehaviour, IExternalInputManager
	{
		// Token: 0x06001575 RID: 5493 RVA: 0x0000614F File Offset: 0x0000434F
		object IExternalInputManager.Initialize(Platform platform, object configVars)
		{
			return null;
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x00002229 File Offset: 0x00000429
		void IExternalInputManager.Deinitialize()
		{
		}

		// Token: 0x04001AE1 RID: 6881
		[SerializeField]
		private NintendoSwitchInputManager.UserData _userData = new NintendoSwitchInputManager.UserData();

		// Token: 0x02000409 RID: 1033
		[Serializable]
		private class UserData : IKeyedData<int>
		{
			// Token: 0x170003F2 RID: 1010
			// (get) Token: 0x06001578 RID: 5496 RVA: 0x00010E6D File Offset: 0x0000F06D
			// (set) Token: 0x06001579 RID: 5497 RVA: 0x00010E75 File Offset: 0x0000F075
			public int allowedNpadStyles
			{
				get
				{
					return this._allowedNpadStyles;
				}
				set
				{
					this._allowedNpadStyles = value;
				}
			}

			// Token: 0x170003F3 RID: 1011
			// (get) Token: 0x0600157A RID: 5498 RVA: 0x00010E7E File Offset: 0x0000F07E
			// (set) Token: 0x0600157B RID: 5499 RVA: 0x00010E86 File Offset: 0x0000F086
			public int joyConGripStyle
			{
				get
				{
					return this._joyConGripStyle;
				}
				set
				{
					this._joyConGripStyle = value;
				}
			}

			// Token: 0x170003F4 RID: 1012
			// (get) Token: 0x0600157C RID: 5500 RVA: 0x00010E8F File Offset: 0x0000F08F
			// (set) Token: 0x0600157D RID: 5501 RVA: 0x00010E97 File Offset: 0x0000F097
			public bool adjustIMUsForGripStyle
			{
				get
				{
					return this._adjustIMUsForGripStyle;
				}
				set
				{
					this._adjustIMUsForGripStyle = value;
				}
			}

			// Token: 0x170003F5 RID: 1013
			// (get) Token: 0x0600157E RID: 5502 RVA: 0x00010EA0 File Offset: 0x0000F0A0
			// (set) Token: 0x0600157F RID: 5503 RVA: 0x00010EA8 File Offset: 0x0000F0A8
			public int handheldActivationMode
			{
				get
				{
					return this._handheldActivationMode;
				}
				set
				{
					this._handheldActivationMode = value;
				}
			}

			// Token: 0x170003F6 RID: 1014
			// (get) Token: 0x06001580 RID: 5504 RVA: 0x00010EB1 File Offset: 0x0000F0B1
			// (set) Token: 0x06001581 RID: 5505 RVA: 0x00010EB9 File Offset: 0x0000F0B9
			public bool assignJoysticksByNpadId
			{
				get
				{
					return this._assignJoysticksByNpadId;
				}
				set
				{
					this._assignJoysticksByNpadId = value;
				}
			}

			// Token: 0x170003F7 RID: 1015
			// (get) Token: 0x06001582 RID: 5506 RVA: 0x00010EC2 File Offset: 0x0000F0C2
			// (set) Token: 0x06001583 RID: 5507 RVA: 0x00010ECA File Offset: 0x0000F0CA
			public bool useVibrationThread
			{
				get
				{
					return this._useVibrationThread;
				}
				set
				{
					this._useVibrationThread = value;
				}
			}

			// Token: 0x170003F8 RID: 1016
			// (get) Token: 0x06001584 RID: 5508 RVA: 0x00010ED3 File Offset: 0x0000F0D3
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo1
			{
				get
				{
					return this._npadNo1;
				}
			}

			// Token: 0x170003F9 RID: 1017
			// (get) Token: 0x06001585 RID: 5509 RVA: 0x00010EDB File Offset: 0x0000F0DB
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo2
			{
				get
				{
					return this._npadNo2;
				}
			}

			// Token: 0x170003FA RID: 1018
			// (get) Token: 0x06001586 RID: 5510 RVA: 0x00010EE3 File Offset: 0x0000F0E3
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo3
			{
				get
				{
					return this._npadNo3;
				}
			}

			// Token: 0x170003FB RID: 1019
			// (get) Token: 0x06001587 RID: 5511 RVA: 0x00010EEB File Offset: 0x0000F0EB
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo4
			{
				get
				{
					return this._npadNo4;
				}
			}

			// Token: 0x170003FC RID: 1020
			// (get) Token: 0x06001588 RID: 5512 RVA: 0x00010EF3 File Offset: 0x0000F0F3
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo5
			{
				get
				{
					return this._npadNo5;
				}
			}

			// Token: 0x170003FD RID: 1021
			// (get) Token: 0x06001589 RID: 5513 RVA: 0x00010EFB File Offset: 0x0000F0FB
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo6
			{
				get
				{
					return this._npadNo6;
				}
			}

			// Token: 0x170003FE RID: 1022
			// (get) Token: 0x0600158A RID: 5514 RVA: 0x00010F03 File Offset: 0x0000F103
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo7
			{
				get
				{
					return this._npadNo7;
				}
			}

			// Token: 0x170003FF RID: 1023
			// (get) Token: 0x0600158B RID: 5515 RVA: 0x00010F0B File Offset: 0x0000F10B
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo8
			{
				get
				{
					return this._npadNo8;
				}
			}

			// Token: 0x17000400 RID: 1024
			// (get) Token: 0x0600158C RID: 5516 RVA: 0x00010F13 File Offset: 0x0000F113
			private NintendoSwitchInputManager.NpadSettings_Internal npadHandheld
			{
				get
				{
					return this._npadHandheld;
				}
			}

			// Token: 0x17000401 RID: 1025
			// (get) Token: 0x0600158D RID: 5517 RVA: 0x00010F1B File Offset: 0x0000F11B
			public NintendoSwitchInputManager.DebugPadSettings_Internal debugPad
			{
				get
				{
					return this._debugPad;
				}
			}

			// Token: 0x17000402 RID: 1026
			// (get) Token: 0x0600158E RID: 5518 RVA: 0x0005ED7C File Offset: 0x0005CF7C
			private Dictionary<int, object[]> delegates
			{
				get
				{
					if (this.__delegates != null)
					{
						return this.__delegates;
					}
					Dictionary<int, object[]> dictionary = new Dictionary<int, object[]>();
					dictionary.Add(0, new object[]
					{
						new Func<int>(() => this.allowedNpadStyles),
						new Action<int>(delegate(int x)
						{
							this.allowedNpadStyles = x;
						})
					});
					dictionary.Add(1, new object[]
					{
						new Func<int>(() => this.joyConGripStyle),
						new Action<int>(delegate(int x)
						{
							this.joyConGripStyle = x;
						})
					});
					dictionary.Add(2, new object[]
					{
						new Func<bool>(() => this.adjustIMUsForGripStyle),
						new Action<bool>(delegate(bool x)
						{
							this.adjustIMUsForGripStyle = x;
						})
					});
					dictionary.Add(3, new object[]
					{
						new Func<int>(() => this.handheldActivationMode),
						new Action<int>(delegate(int x)
						{
							this.handheldActivationMode = x;
						})
					});
					dictionary.Add(4, new object[]
					{
						new Func<bool>(() => this.assignJoysticksByNpadId),
						new Action<bool>(delegate(bool x)
						{
							this.assignJoysticksByNpadId = x;
						})
					});
					Dictionary<int, object[]> dictionary2 = dictionary;
					int num = 5;
					object[] array = new object[2];
					array[0] = new Func<object>(() => this.npadNo1);
					dictionary2.Add(num, array);
					Dictionary<int, object[]> dictionary3 = dictionary;
					int num2 = 6;
					object[] array2 = new object[2];
					array2[0] = new Func<object>(() => this.npadNo2);
					dictionary3.Add(num2, array2);
					Dictionary<int, object[]> dictionary4 = dictionary;
					int num3 = 7;
					object[] array3 = new object[2];
					array3[0] = new Func<object>(() => this.npadNo3);
					dictionary4.Add(num3, array3);
					Dictionary<int, object[]> dictionary5 = dictionary;
					int num4 = 8;
					object[] array4 = new object[2];
					array4[0] = new Func<object>(() => this.npadNo4);
					dictionary5.Add(num4, array4);
					Dictionary<int, object[]> dictionary6 = dictionary;
					int num5 = 9;
					object[] array5 = new object[2];
					array5[0] = new Func<object>(() => this.npadNo5);
					dictionary6.Add(num5, array5);
					Dictionary<int, object[]> dictionary7 = dictionary;
					int num6 = 10;
					object[] array6 = new object[2];
					array6[0] = new Func<object>(() => this.npadNo6);
					dictionary7.Add(num6, array6);
					Dictionary<int, object[]> dictionary8 = dictionary;
					int num7 = 11;
					object[] array7 = new object[2];
					array7[0] = new Func<object>(() => this.npadNo7);
					dictionary8.Add(num7, array7);
					Dictionary<int, object[]> dictionary9 = dictionary;
					int num8 = 12;
					object[] array8 = new object[2];
					array8[0] = new Func<object>(() => this.npadNo8);
					dictionary9.Add(num8, array8);
					Dictionary<int, object[]> dictionary10 = dictionary;
					int num9 = 13;
					object[] array9 = new object[2];
					array9[0] = new Func<object>(() => this.npadHandheld);
					dictionary10.Add(num9, array9);
					Dictionary<int, object[]> dictionary11 = dictionary;
					int num10 = 14;
					object[] array10 = new object[2];
					array10[0] = new Func<object>(() => this.debugPad);
					dictionary11.Add(num10, array10);
					dictionary.Add(15, new object[]
					{
						new Func<bool>(() => this.useVibrationThread),
						new Action<bool>(delegate(bool x)
						{
							this.useVibrationThread = x;
						})
					});
					return this.__delegates = dictionary;
				}
			}

			// Token: 0x0600158F RID: 5519 RVA: 0x0005EFCC File Offset: 0x0005D1CC
			bool IKeyedData<int>.TryGetValue<T>(int key, out T value)
			{
				object[] array;
				if (!this.delegates.TryGetValue(key, out array))
				{
					value = default(T);
					return false;
				}
				Func<T> func = array[0] as Func<T>;
				if (func == null)
				{
					value = default(T);
					return false;
				}
				value = func();
				return true;
			}

			// Token: 0x06001590 RID: 5520 RVA: 0x0005F014 File Offset: 0x0005D214
			bool IKeyedData<int>.TrySetValue<T>(int key, T value)
			{
				object[] array;
				if (!this.delegates.TryGetValue(key, out array))
				{
					return false;
				}
				Action<T> action = array[1] as Action<T>;
				if (action == null)
				{
					return false;
				}
				action(value);
				return true;
			}

			// Token: 0x04001AE2 RID: 6882
			[SerializeField]
			private int _allowedNpadStyles = -1;

			// Token: 0x04001AE3 RID: 6883
			[SerializeField]
			private int _joyConGripStyle = 1;

			// Token: 0x04001AE4 RID: 6884
			[SerializeField]
			private bool _adjustIMUsForGripStyle = true;

			// Token: 0x04001AE5 RID: 6885
			[SerializeField]
			private int _handheldActivationMode;

			// Token: 0x04001AE6 RID: 6886
			[SerializeField]
			private bool _assignJoysticksByNpadId = true;

			// Token: 0x04001AE7 RID: 6887
			[SerializeField]
			private bool _useVibrationThread = true;

			// Token: 0x04001AE8 RID: 6888
			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo1 = new NintendoSwitchInputManager.NpadSettings_Internal(0);

			// Token: 0x04001AE9 RID: 6889
			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo2 = new NintendoSwitchInputManager.NpadSettings_Internal(1);

			// Token: 0x04001AEA RID: 6890
			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo3 = new NintendoSwitchInputManager.NpadSettings_Internal(2);

			// Token: 0x04001AEB RID: 6891
			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo4 = new NintendoSwitchInputManager.NpadSettings_Internal(3);

			// Token: 0x04001AEC RID: 6892
			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo5 = new NintendoSwitchInputManager.NpadSettings_Internal(4);

			// Token: 0x04001AED RID: 6893
			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo6 = new NintendoSwitchInputManager.NpadSettings_Internal(5);

			// Token: 0x04001AEE RID: 6894
			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo7 = new NintendoSwitchInputManager.NpadSettings_Internal(6);

			// Token: 0x04001AEF RID: 6895
			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo8 = new NintendoSwitchInputManager.NpadSettings_Internal(7);

			// Token: 0x04001AF0 RID: 6896
			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadHandheld = new NintendoSwitchInputManager.NpadSettings_Internal(0);

			// Token: 0x04001AF1 RID: 6897
			[SerializeField]
			private NintendoSwitchInputManager.DebugPadSettings_Internal _debugPad = new NintendoSwitchInputManager.DebugPadSettings_Internal(0);

			// Token: 0x04001AF2 RID: 6898
			private Dictionary<int, object[]> __delegates;
		}

		// Token: 0x0200040A RID: 1034
		[Serializable]
		private sealed class NpadSettings_Internal : IKeyedData<int>
		{
			// Token: 0x17000403 RID: 1027
			// (get) Token: 0x060015A8 RID: 5544 RVA: 0x00010FD9 File Offset: 0x0000F1D9
			// (set) Token: 0x060015A9 RID: 5545 RVA: 0x00010FE1 File Offset: 0x0000F1E1
			private bool isAllowed
			{
				get
				{
					return this._isAllowed;
				}
				set
				{
					this._isAllowed = value;
				}
			}

			// Token: 0x17000404 RID: 1028
			// (get) Token: 0x060015AA RID: 5546 RVA: 0x00010FEA File Offset: 0x0000F1EA
			// (set) Token: 0x060015AB RID: 5547 RVA: 0x00010FF2 File Offset: 0x0000F1F2
			private int rewiredPlayerId
			{
				get
				{
					return this._rewiredPlayerId;
				}
				set
				{
					this._rewiredPlayerId = value;
				}
			}

			// Token: 0x17000405 RID: 1029
			// (get) Token: 0x060015AC RID: 5548 RVA: 0x00010FFB File Offset: 0x0000F1FB
			// (set) Token: 0x060015AD RID: 5549 RVA: 0x00011003 File Offset: 0x0000F203
			private int joyConAssignmentMode
			{
				get
				{
					return this._joyConAssignmentMode;
				}
				set
				{
					this._joyConAssignmentMode = value;
				}
			}

			// Token: 0x060015AE RID: 5550 RVA: 0x0001100C File Offset: 0x0000F20C
			internal NpadSettings_Internal(int playerId)
			{
				this._rewiredPlayerId = playerId;
			}

			// Token: 0x17000406 RID: 1030
			// (get) Token: 0x060015AF RID: 5551 RVA: 0x0005F0FC File Offset: 0x0005D2FC
			private Dictionary<int, object[]> delegates
			{
				get
				{
					if (this.__delegates != null)
					{
						return this.__delegates;
					}
					return this.__delegates = new Dictionary<int, object[]>
					{
						{
							0,
							new object[]
							{
								new Func<bool>(() => this.isAllowed),
								new Action<bool>(delegate(bool x)
								{
									this.isAllowed = x;
								})
							}
						},
						{
							1,
							new object[]
							{
								new Func<int>(() => this.rewiredPlayerId),
								new Action<int>(delegate(int x)
								{
									this.rewiredPlayerId = x;
								})
							}
						},
						{
							2,
							new object[]
							{
								new Func<int>(() => this.joyConAssignmentMode),
								new Action<int>(delegate(int x)
								{
									this.joyConAssignmentMode = x;
								})
							}
						}
					};
				}
			}

			// Token: 0x060015B0 RID: 5552 RVA: 0x0005F1AC File Offset: 0x0005D3AC
			bool IKeyedData<int>.TryGetValue<T>(int key, out T value)
			{
				object[] array;
				if (!this.delegates.TryGetValue(key, out array))
				{
					value = default(T);
					return false;
				}
				Func<T> func = array[0] as Func<T>;
				if (func == null)
				{
					value = default(T);
					return false;
				}
				value = func();
				return true;
			}

			// Token: 0x060015B1 RID: 5553 RVA: 0x0005F1F4 File Offset: 0x0005D3F4
			bool IKeyedData<int>.TrySetValue<T>(int key, T value)
			{
				object[] array;
				if (!this.delegates.TryGetValue(key, out array))
				{
					return false;
				}
				Action<T> action = array[1] as Action<T>;
				if (action == null)
				{
					return false;
				}
				action(value);
				return true;
			}

			// Token: 0x04001AF3 RID: 6899
			[Tooltip("Determines whether this Npad id is allowed to be used by the system.")]
			[SerializeField]
			private bool _isAllowed = true;

			// Token: 0x04001AF4 RID: 6900
			[Tooltip("The Rewired Player Id assigned to this Npad id.")]
			[SerializeField]
			private int _rewiredPlayerId;

			// Token: 0x04001AF5 RID: 6901
			[Tooltip("Determines how Joy-Cons should be handled.\n\nUnmodified: Joy-Con assignment mode will be left at the system default.\nDual: Joy-Cons pairs are handled as a single controller.\nSingle: Joy-Cons are handled as individual controllers.")]
			[SerializeField]
			private int _joyConAssignmentMode = -1;

			// Token: 0x04001AF6 RID: 6902
			private Dictionary<int, object[]> __delegates;
		}

		// Token: 0x0200040B RID: 1035
		[Serializable]
		private sealed class DebugPadSettings_Internal : IKeyedData<int>
		{
			// Token: 0x17000407 RID: 1031
			// (get) Token: 0x060015B8 RID: 5560 RVA: 0x0001105C File Offset: 0x0000F25C
			// (set) Token: 0x060015B9 RID: 5561 RVA: 0x00011064 File Offset: 0x0000F264
			private int rewiredPlayerId
			{
				get
				{
					return this._rewiredPlayerId;
				}
				set
				{
					this._rewiredPlayerId = value;
				}
			}

			// Token: 0x17000408 RID: 1032
			// (get) Token: 0x060015BA RID: 5562 RVA: 0x0001106D File Offset: 0x0000F26D
			// (set) Token: 0x060015BB RID: 5563 RVA: 0x00011075 File Offset: 0x0000F275
			private bool enabled
			{
				get
				{
					return this._enabled;
				}
				set
				{
					this._enabled = value;
				}
			}

			// Token: 0x060015BC RID: 5564 RVA: 0x0001107E File Offset: 0x0000F27E
			internal DebugPadSettings_Internal(int playerId)
			{
				this._rewiredPlayerId = playerId;
			}

			// Token: 0x17000409 RID: 1033
			// (get) Token: 0x060015BD RID: 5565 RVA: 0x0005F22C File Offset: 0x0005D42C
			private Dictionary<int, object[]> delegates
			{
				get
				{
					if (this.__delegates != null)
					{
						return this.__delegates;
					}
					return this.__delegates = new Dictionary<int, object[]>
					{
						{
							0,
							new object[]
							{
								new Func<bool>(() => this.enabled),
								new Action<bool>(delegate(bool x)
								{
									this.enabled = x;
								})
							}
						},
						{
							1,
							new object[]
							{
								new Func<int>(() => this.rewiredPlayerId),
								new Action<int>(delegate(int x)
								{
									this.rewiredPlayerId = x;
								})
							}
						}
					};
				}
			}

			// Token: 0x060015BE RID: 5566 RVA: 0x0005F2B0 File Offset: 0x0005D4B0
			bool IKeyedData<int>.TryGetValue<T>(int key, out T value)
			{
				object[] array;
				if (!this.delegates.TryGetValue(key, out array))
				{
					value = default(T);
					return false;
				}
				Func<T> func = array[0] as Func<T>;
				if (func == null)
				{
					value = default(T);
					return false;
				}
				value = func();
				return true;
			}

			// Token: 0x060015BF RID: 5567 RVA: 0x0005F2F8 File Offset: 0x0005D4F8
			bool IKeyedData<int>.TrySetValue<T>(int key, T value)
			{
				object[] array;
				if (!this.delegates.TryGetValue(key, out array))
				{
					return false;
				}
				Action<T> action = array[1] as Action<T>;
				if (action == null)
				{
					return false;
				}
				action(value);
				return true;
			}

			// Token: 0x04001AF7 RID: 6903
			[Tooltip("Determines whether the Debug Pad will be enabled.")]
			[SerializeField]
			private bool _enabled;

			// Token: 0x04001AF8 RID: 6904
			[Tooltip("The Rewired Player Id to which the Debug Pad will be assigned.")]
			[SerializeField]
			private int _rewiredPlayerId;

			// Token: 0x04001AF9 RID: 6905
			private Dictionary<int, object[]> __delegates;
		}
	}
}
