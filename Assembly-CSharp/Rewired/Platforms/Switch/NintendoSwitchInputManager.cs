using System;
using System.Collections.Generic;
using Rewired.Utils.Interfaces;
using UnityEngine;

namespace Rewired.Platforms.Switch
{
	[AddComponentMenu("Rewired/Nintendo Switch Input Manager")]
	[RequireComponent(typeof(InputManager))]
	public sealed class NintendoSwitchInputManager : MonoBehaviour, IExternalInputManager
	{
		// Token: 0x060015D5 RID: 5589 RVA: 0x00006415 File Offset: 0x00004615
		object IExternalInputManager.Initialize(Platform platform, object configVars)
		{
			return null;
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00002229 File Offset: 0x00000429
		void IExternalInputManager.Deinitialize()
		{
		}

		[SerializeField]
		private NintendoSwitchInputManager.UserData _userData = new NintendoSwitchInputManager.UserData();

		[Serializable]
		private class UserData : IKeyedData<int>
		{
			// (get) Token: 0x060015D8 RID: 5592 RVA: 0x00011255 File Offset: 0x0000F455
			// (set) Token: 0x060015D9 RID: 5593 RVA: 0x0001125D File Offset: 0x0000F45D
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

			// (get) Token: 0x060015DA RID: 5594 RVA: 0x00011266 File Offset: 0x0000F466
			// (set) Token: 0x060015DB RID: 5595 RVA: 0x0001126E File Offset: 0x0000F46E
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

			// (get) Token: 0x060015DC RID: 5596 RVA: 0x00011277 File Offset: 0x0000F477
			// (set) Token: 0x060015DD RID: 5597 RVA: 0x0001127F File Offset: 0x0000F47F
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

			// (get) Token: 0x060015DE RID: 5598 RVA: 0x00011288 File Offset: 0x0000F488
			// (set) Token: 0x060015DF RID: 5599 RVA: 0x00011290 File Offset: 0x0000F490
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

			// (get) Token: 0x060015E0 RID: 5600 RVA: 0x00011299 File Offset: 0x0000F499
			// (set) Token: 0x060015E1 RID: 5601 RVA: 0x000112A1 File Offset: 0x0000F4A1
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

			// (get) Token: 0x060015E2 RID: 5602 RVA: 0x000112AA File Offset: 0x0000F4AA
			// (set) Token: 0x060015E3 RID: 5603 RVA: 0x000112B2 File Offset: 0x0000F4B2
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

			// (get) Token: 0x060015E4 RID: 5604 RVA: 0x000112BB File Offset: 0x0000F4BB
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo1
			{
				get
				{
					return this._npadNo1;
				}
			}

			// (get) Token: 0x060015E5 RID: 5605 RVA: 0x000112C3 File Offset: 0x0000F4C3
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo2
			{
				get
				{
					return this._npadNo2;
				}
			}

			// (get) Token: 0x060015E6 RID: 5606 RVA: 0x000112CB File Offset: 0x0000F4CB
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo3
			{
				get
				{
					return this._npadNo3;
				}
			}

			// (get) Token: 0x060015E7 RID: 5607 RVA: 0x000112D3 File Offset: 0x0000F4D3
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo4
			{
				get
				{
					return this._npadNo4;
				}
			}

			// (get) Token: 0x060015E8 RID: 5608 RVA: 0x000112DB File Offset: 0x0000F4DB
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo5
			{
				get
				{
					return this._npadNo5;
				}
			}

			// (get) Token: 0x060015E9 RID: 5609 RVA: 0x000112E3 File Offset: 0x0000F4E3
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo6
			{
				get
				{
					return this._npadNo6;
				}
			}

			// (get) Token: 0x060015EA RID: 5610 RVA: 0x000112EB File Offset: 0x0000F4EB
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo7
			{
				get
				{
					return this._npadNo7;
				}
			}

			// (get) Token: 0x060015EB RID: 5611 RVA: 0x000112F3 File Offset: 0x0000F4F3
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo8
			{
				get
				{
					return this._npadNo8;
				}
			}

			// (get) Token: 0x060015EC RID: 5612 RVA: 0x000112FB File Offset: 0x0000F4FB
			private NintendoSwitchInputManager.NpadSettings_Internal npadHandheld
			{
				get
				{
					return this._npadHandheld;
				}
			}

			// (get) Token: 0x060015ED RID: 5613 RVA: 0x00011303 File Offset: 0x0000F503
			public NintendoSwitchInputManager.DebugPadSettings_Internal debugPad
			{
				get
				{
					return this._debugPad;
				}
			}

			// (get) Token: 0x060015EE RID: 5614 RVA: 0x00060C10 File Offset: 0x0005EE10
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

			// Token: 0x060015EF RID: 5615 RVA: 0x00060E60 File Offset: 0x0005F060
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

			// Token: 0x060015F0 RID: 5616 RVA: 0x00060EA8 File Offset: 0x0005F0A8
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

			[SerializeField]
			private int _allowedNpadStyles = -1;

			[SerializeField]
			private int _joyConGripStyle = 1;

			[SerializeField]
			private bool _adjustIMUsForGripStyle = true;

			[SerializeField]
			private int _handheldActivationMode;

			[SerializeField]
			private bool _assignJoysticksByNpadId = true;

			[SerializeField]
			private bool _useVibrationThread = true;

			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo1 = new NintendoSwitchInputManager.NpadSettings_Internal(0);

			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo2 = new NintendoSwitchInputManager.NpadSettings_Internal(1);

			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo3 = new NintendoSwitchInputManager.NpadSettings_Internal(2);

			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo4 = new NintendoSwitchInputManager.NpadSettings_Internal(3);

			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo5 = new NintendoSwitchInputManager.NpadSettings_Internal(4);

			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo6 = new NintendoSwitchInputManager.NpadSettings_Internal(5);

			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo7 = new NintendoSwitchInputManager.NpadSettings_Internal(6);

			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo8 = new NintendoSwitchInputManager.NpadSettings_Internal(7);

			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadHandheld = new NintendoSwitchInputManager.NpadSettings_Internal(0);

			[SerializeField]
			private NintendoSwitchInputManager.DebugPadSettings_Internal _debugPad = new NintendoSwitchInputManager.DebugPadSettings_Internal(0);

			private Dictionary<int, object[]> __delegates;
		}

		[Serializable]
		private sealed class NpadSettings_Internal : IKeyedData<int>
		{
			// (get) Token: 0x06001608 RID: 5640 RVA: 0x000113C1 File Offset: 0x0000F5C1
			// (set) Token: 0x06001609 RID: 5641 RVA: 0x000113C9 File Offset: 0x0000F5C9
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

			// (get) Token: 0x0600160A RID: 5642 RVA: 0x000113D2 File Offset: 0x0000F5D2
			// (set) Token: 0x0600160B RID: 5643 RVA: 0x000113DA File Offset: 0x0000F5DA
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

			// (get) Token: 0x0600160C RID: 5644 RVA: 0x000113E3 File Offset: 0x0000F5E3
			// (set) Token: 0x0600160D RID: 5645 RVA: 0x000113EB File Offset: 0x0000F5EB
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

			// Token: 0x0600160E RID: 5646 RVA: 0x000113F4 File Offset: 0x0000F5F4
			internal NpadSettings_Internal(int playerId)
			{
				this._rewiredPlayerId = playerId;
			}

			// (get) Token: 0x0600160F RID: 5647 RVA: 0x00060F90 File Offset: 0x0005F190
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

			// Token: 0x06001610 RID: 5648 RVA: 0x00061040 File Offset: 0x0005F240
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

			// Token: 0x06001611 RID: 5649 RVA: 0x00061088 File Offset: 0x0005F288
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

			[Tooltip("Determines whether this Npad id is allowed to be used by the system.")]
			[SerializeField]
			private bool _isAllowed = true;

			[Tooltip("The Rewired Player Id assigned to this Npad id.")]
			[SerializeField]
			private int _rewiredPlayerId;

			[Tooltip("Determines how Joy-Cons should be handled.\n\nUnmodified: Joy-Con assignment mode will be left at the system default.\nDual: Joy-Cons pairs are handled as a single controller.\nSingle: Joy-Cons are handled as individual controllers.")]
			[SerializeField]
			private int _joyConAssignmentMode = -1;

			private Dictionary<int, object[]> __delegates;
		}

		[Serializable]
		private sealed class DebugPadSettings_Internal : IKeyedData<int>
		{
			// (get) Token: 0x06001618 RID: 5656 RVA: 0x00011444 File Offset: 0x0000F644
			// (set) Token: 0x06001619 RID: 5657 RVA: 0x0001144C File Offset: 0x0000F64C
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

			// (get) Token: 0x0600161A RID: 5658 RVA: 0x00011455 File Offset: 0x0000F655
			// (set) Token: 0x0600161B RID: 5659 RVA: 0x0001145D File Offset: 0x0000F65D
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

			// Token: 0x0600161C RID: 5660 RVA: 0x00011466 File Offset: 0x0000F666
			internal DebugPadSettings_Internal(int playerId)
			{
				this._rewiredPlayerId = playerId;
			}

			// (get) Token: 0x0600161D RID: 5661 RVA: 0x000610C0 File Offset: 0x0005F2C0
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

			// Token: 0x0600161E RID: 5662 RVA: 0x00061144 File Offset: 0x0005F344
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

			// Token: 0x0600161F RID: 5663 RVA: 0x0006118C File Offset: 0x0005F38C
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

			[Tooltip("Determines whether the Debug Pad will be enabled.")]
			[SerializeField]
			private bool _enabled;

			[Tooltip("The Rewired Player Id to which the Debug Pad will be assigned.")]
			[SerializeField]
			private int _rewiredPlayerId;

			private Dictionary<int, object[]> __delegates;
		}
	}
}
