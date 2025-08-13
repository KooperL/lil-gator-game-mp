using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Rewired.Utils.Libraries.TinyJson;
using UnityEngine;

namespace Rewired.Data
{
	// Token: 0x0200040D RID: 1037
	public class UserDataStore_PlayerPrefs : UserDataStore
	{
		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x000111D2 File Offset: 0x0000F3D2
		// (set) Token: 0x0600161B RID: 5659 RVA: 0x000111DA File Offset: 0x0000F3DA
		public bool IsEnabled
		{
			get
			{
				return this.isEnabled;
			}
			set
			{
				this.isEnabled = value;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x000111E3 File Offset: 0x0000F3E3
		// (set) Token: 0x0600161D RID: 5661 RVA: 0x000111EB File Offset: 0x0000F3EB
		public bool LoadDataOnStart
		{
			get
			{
				return this.loadDataOnStart;
			}
			set
			{
				this.loadDataOnStart = value;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x000111F4 File Offset: 0x0000F3F4
		// (set) Token: 0x0600161F RID: 5663 RVA: 0x000111FC File Offset: 0x0000F3FC
		public bool LoadJoystickAssignments
		{
			get
			{
				return this.loadJoystickAssignments;
			}
			set
			{
				this.loadJoystickAssignments = value;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x00011205 File Offset: 0x0000F405
		// (set) Token: 0x06001621 RID: 5665 RVA: 0x0001120D File Offset: 0x0000F40D
		public bool LoadKeyboardAssignments
		{
			get
			{
				return this.loadKeyboardAssignments;
			}
			set
			{
				this.loadKeyboardAssignments = value;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x00011216 File Offset: 0x0000F416
		// (set) Token: 0x06001623 RID: 5667 RVA: 0x0001121E File Offset: 0x0000F41E
		public bool LoadMouseAssignments
		{
			get
			{
				return this.loadMouseAssignments;
			}
			set
			{
				this.loadMouseAssignments = value;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x00011227 File Offset: 0x0000F427
		// (set) Token: 0x06001625 RID: 5669 RVA: 0x0001122F File Offset: 0x0000F42F
		public string PlayerPrefsKeyPrefix
		{
			get
			{
				return this.playerPrefsKeyPrefix;
			}
			set
			{
				this.playerPrefsKeyPrefix = value;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001626 RID: 5670 RVA: 0x00011238 File Offset: 0x0000F438
		private string playerPrefsKey_controllerAssignments
		{
			get
			{
				return string.Format("{0}_{1}", this.playerPrefsKeyPrefix, "ControllerAssignments");
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x0001124F File Offset: 0x0000F44F
		private bool loadControllerAssignments
		{
			get
			{
				return this.loadKeyboardAssignments || this.loadMouseAssignments || this.loadJoystickAssignments;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x0005F3A0 File Offset: 0x0005D5A0
		private List<int> allActionIds
		{
			get
			{
				if (this.__allActionIds != null)
				{
					return this.__allActionIds;
				}
				List<int> list = new List<int>();
				IList<InputAction> actions = ReInput.mapping.Actions;
				for (int i = 0; i < actions.Count; i++)
				{
					list.Add(actions[i].id);
				}
				this.__allActionIds = list;
				return list;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001629 RID: 5673 RVA: 0x0005F3F8 File Offset: 0x0005D5F8
		private string allActionIdsString
		{
			get
			{
				if (!string.IsNullOrEmpty(this.__allActionIdsString))
				{
					return this.__allActionIdsString;
				}
				StringBuilder stringBuilder = new StringBuilder();
				List<int> allActionIds = this.allActionIds;
				for (int i = 0; i < allActionIds.Count; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(allActionIds[i]);
				}
				this.__allActionIdsString = stringBuilder.ToString();
				return this.__allActionIdsString;
			}
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x00011269 File Offset: 0x0000F469
		public override void Save()
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not save any data.", this);
				return;
			}
			this.SaveAll();
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00011285 File Offset: 0x0000F485
		public override void SaveControllerData(int playerId, ControllerType controllerType, int controllerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not save any data.", this);
				return;
			}
			this.SaveControllerDataNow(playerId, controllerType, controllerId);
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x000112A4 File Offset: 0x0000F4A4
		public override void SaveControllerData(ControllerType controllerType, int controllerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not save any data.", this);
				return;
			}
			this.SaveControllerDataNow(controllerType, controllerId);
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x000112C2 File Offset: 0x0000F4C2
		public override void SavePlayerData(int playerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not save any data.", this);
				return;
			}
			this.SavePlayerDataNow(playerId);
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x000112DF File Offset: 0x0000F4DF
		public override void SaveInputBehavior(int playerId, int behaviorId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not save any data.", this);
				return;
			}
			this.SaveInputBehaviorNow(playerId, behaviorId);
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x000112FD File Offset: 0x0000F4FD
		public override void Load()
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not load any data.", this);
				return;
			}
			this.LoadAll();
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x0001131A File Offset: 0x0000F51A
		public override void LoadControllerData(int playerId, ControllerType controllerType, int controllerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not load any data.", this);
				return;
			}
			this.LoadControllerDataNow(playerId, controllerType, controllerId);
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x0001133A File Offset: 0x0000F53A
		public override void LoadControllerData(ControllerType controllerType, int controllerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not load any data.", this);
				return;
			}
			this.LoadControllerDataNow(controllerType, controllerId);
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x00011359 File Offset: 0x0000F559
		public override void LoadPlayerData(int playerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not load any data.", this);
				return;
			}
			this.LoadPlayerDataNow(playerId);
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x00011377 File Offset: 0x0000F577
		public override void LoadInputBehavior(int playerId, int behaviorId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not load any data.", this);
				return;
			}
			this.LoadInputBehaviorNow(playerId, behaviorId);
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x00011396 File Offset: 0x0000F596
		protected override void OnInitialize()
		{
			if (this.loadDataOnStart)
			{
				this.Load();
				if (this.loadControllerAssignments && ReInput.controllers.joystickCount > 0)
				{
					this.wasJoystickEverDetected = true;
					this.SaveControllerAssignments();
				}
			}
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x0005F468 File Offset: 0x0005D668
		protected override void OnControllerConnected(ControllerStatusChangedEventArgs args)
		{
			if (!this.isEnabled)
			{
				return;
			}
			if (args.controllerType == 2)
			{
				this.LoadJoystickData(args.controllerId);
				if (this.loadDataOnStart && this.loadJoystickAssignments && !this.wasJoystickEverDetected)
				{
					base.StartCoroutine(this.LoadJoystickAssignmentsDeferred());
				}
				if (this.loadJoystickAssignments && !this.deferredJoystickAssignmentLoadPending)
				{
					this.SaveControllerAssignments();
				}
				this.wasJoystickEverDetected = true;
			}
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x000113C9 File Offset: 0x0000F5C9
		protected override void OnControllerPreDisconnect(ControllerStatusChangedEventArgs args)
		{
			if (!this.isEnabled)
			{
				return;
			}
			if (args.controllerType == 2)
			{
				this.SaveJoystickData(args.controllerId);
			}
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x000113E9 File Offset: 0x0000F5E9
		protected override void OnControllerDisconnected(ControllerStatusChangedEventArgs args)
		{
			if (!this.isEnabled)
			{
				return;
			}
			if (this.loadControllerAssignments)
			{
				this.SaveControllerAssignments();
			}
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x0005F4D8 File Offset: 0x0005D6D8
		public override void SaveControllerMap(int playerId, ControllerMap controllerMap)
		{
			if (controllerMap == null)
			{
				return;
			}
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return;
			}
			this.SaveControllerMap(player, controllerMap);
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x0005F504 File Offset: 0x0005D704
		public override ControllerMap LoadControllerMap(int playerId, ControllerIdentifier controllerIdentifier, int categoryId, int layoutId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return null;
			}
			return this.LoadControllerMap(player, controllerIdentifier, categoryId, layoutId);
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x0005F530 File Offset: 0x0005D730
		private int LoadAll()
		{
			int num = 0;
			if (this.loadControllerAssignments && this.LoadControllerAssignmentsNow())
			{
				num++;
			}
			IList<Player> allPlayers = ReInput.players.AllPlayers;
			for (int i = 0; i < allPlayers.Count; i++)
			{
				num += this.LoadPlayerDataNow(allPlayers[i]);
			}
			return num + this.LoadAllJoystickCalibrationData();
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x00011403 File Offset: 0x0000F603
		private int LoadPlayerDataNow(int playerId)
		{
			return this.LoadPlayerDataNow(ReInput.players.GetPlayer(playerId));
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x0005F58C File Offset: 0x0005D78C
		private int LoadPlayerDataNow(Player player)
		{
			if (player == null)
			{
				return 0;
			}
			int num = 0;
			num += this.LoadInputBehaviors(player.id);
			num += this.LoadControllerMaps(player.id, 0, 0);
			num += this.LoadControllerMaps(player.id, 1, 0);
			foreach (Joystick joystick in player.controllers.Joysticks)
			{
				num += this.LoadControllerMaps(player.id, 2, joystick.id);
			}
			this.RefreshLayoutManager(player.id);
			return num;
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x0005F634 File Offset: 0x0005D834
		private int LoadAllJoystickCalibrationData()
		{
			int num = 0;
			IList<Joystick> joysticks = ReInput.controllers.Joysticks;
			for (int i = 0; i < joysticks.Count; i++)
			{
				num += this.LoadJoystickCalibrationData(joysticks[i]);
			}
			return num;
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x00011416 File Offset: 0x0000F616
		private int LoadJoystickCalibrationData(Joystick joystick)
		{
			if (joystick == null)
			{
				return 0;
			}
			if (!joystick.ImportCalibrationMapFromXmlString(this.GetJoystickCalibrationMapXml(joystick)))
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x0001142F File Offset: 0x0000F62F
		private int LoadJoystickCalibrationData(int joystickId)
		{
			return this.LoadJoystickCalibrationData(ReInput.controllers.GetJoystick(joystickId));
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x0005F670 File Offset: 0x0005D870
		private int LoadJoystickData(int joystickId)
		{
			int num = 0;
			IList<Player> allPlayers = ReInput.players.AllPlayers;
			for (int i = 0; i < allPlayers.Count; i++)
			{
				Player player = allPlayers[i];
				if (player.controllers.ContainsController(2, joystickId))
				{
					num += this.LoadControllerMaps(player.id, 2, joystickId);
					this.RefreshLayoutManager(player.id);
				}
			}
			return num + this.LoadJoystickCalibrationData(joystickId);
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x00011442 File Offset: 0x0000F642
		private int LoadControllerDataNow(int playerId, ControllerType controllerType, int controllerId)
		{
			int num = 0 + this.LoadControllerMaps(playerId, controllerType, controllerId);
			this.RefreshLayoutManager(playerId);
			return num + this.LoadControllerDataNow(controllerType, controllerId);
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x0005F6DC File Offset: 0x0005D8DC
		private int LoadControllerDataNow(ControllerType controllerType, int controllerId)
		{
			int num = 0;
			if (controllerType == 2)
			{
				num += this.LoadJoystickCalibrationData(controllerId);
			}
			return num;
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x0005F6FC File Offset: 0x0005D8FC
		private int LoadControllerMaps(int playerId, ControllerType controllerType, int controllerId)
		{
			int num = 0;
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return num;
			}
			Controller controller = ReInput.controllers.GetController(controllerType, controllerId);
			if (controller == null)
			{
				return num;
			}
			IList<InputMapCategory> mapCategories = ReInput.mapping.MapCategories;
			for (int i = 0; i < mapCategories.Count; i++)
			{
				InputMapCategory inputMapCategory = mapCategories[i];
				if (inputMapCategory.userAssignable)
				{
					IList<InputLayout> list = ReInput.mapping.MapLayouts(controller.type);
					for (int j = 0; j < list.Count; j++)
					{
						InputLayout inputLayout = list[j];
						ControllerMap controllerMap = this.LoadControllerMap(player, controller.identifier, inputMapCategory.id, inputLayout.id);
						if (controllerMap != null)
						{
							player.controllers.maps.AddMap(controller, controllerMap);
							num++;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x0005F7D4 File Offset: 0x0005D9D4
		private ControllerMap LoadControllerMap(Player player, ControllerIdentifier controllerIdentifier, int categoryId, int layoutId)
		{
			if (player == null)
			{
				return null;
			}
			string controllerMapXml = this.GetControllerMapXml(player, controllerIdentifier, categoryId, layoutId);
			if (string.IsNullOrEmpty(controllerMapXml))
			{
				return null;
			}
			ControllerMap controllerMap = ControllerMap.CreateFromXml(controllerIdentifier.controllerType, controllerMapXml);
			if (controllerMap == null)
			{
				return null;
			}
			List<int> controllerMapKnownActionIds = this.GetControllerMapKnownActionIds(player, controllerIdentifier, categoryId, layoutId);
			this.AddDefaultMappingsForNewActions(controllerIdentifier, controllerMap, controllerMapKnownActionIds);
			return controllerMap;
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x0005F828 File Offset: 0x0005DA28
		private int LoadInputBehaviors(int playerId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return 0;
			}
			int num = 0;
			IList<InputBehavior> inputBehaviors = ReInput.mapping.GetInputBehaviors(player.id);
			for (int i = 0; i < inputBehaviors.Count; i++)
			{
				num += this.LoadInputBehaviorNow(player, inputBehaviors[i]);
			}
			return num;
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x0005F87C File Offset: 0x0005DA7C
		private int LoadInputBehaviorNow(int playerId, int behaviorId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return 0;
			}
			InputBehavior inputBehavior = ReInput.mapping.GetInputBehavior(playerId, behaviorId);
			if (inputBehavior == null)
			{
				return 0;
			}
			return this.LoadInputBehaviorNow(player, inputBehavior);
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x0005F8B4 File Offset: 0x0005DAB4
		private int LoadInputBehaviorNow(Player player, InputBehavior inputBehavior)
		{
			if (player == null || inputBehavior == null)
			{
				return 0;
			}
			string inputBehaviorXml = this.GetInputBehaviorXml(player, inputBehavior.id);
			if (inputBehaviorXml == null || inputBehaviorXml == string.Empty)
			{
				return 0;
			}
			if (!inputBehavior.ImportXmlString(inputBehaviorXml))
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x0005F8F8 File Offset: 0x0005DAF8
		private bool LoadControllerAssignmentsNow()
		{
			try
			{
				UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo controllerAssignmentSaveInfo = this.LoadControllerAssignmentData();
				if (controllerAssignmentSaveInfo == null)
				{
					return false;
				}
				if (this.loadKeyboardAssignments || this.loadMouseAssignments)
				{
					this.LoadKeyboardAndMouseAssignmentsNow(controllerAssignmentSaveInfo);
				}
				if (this.loadJoystickAssignments)
				{
					this.LoadJoystickAssignmentsNow(controllerAssignmentSaveInfo);
				}
			}
			catch
			{
			}
			return true;
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x0005F954 File Offset: 0x0005DB54
		private bool LoadKeyboardAndMouseAssignmentsNow(UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo data)
		{
			try
			{
				if (data == null && (data = this.LoadControllerAssignmentData()) == null)
				{
					return false;
				}
				foreach (Player player in ReInput.players.AllPlayers)
				{
					if (data.ContainsPlayer(player.id))
					{
						UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo playerInfo = data.players[data.IndexOfPlayer(player.id)];
						if (this.loadKeyboardAssignments)
						{
							player.controllers.hasKeyboard = playerInfo.hasKeyboard;
						}
						if (this.loadMouseAssignments)
						{
							player.controllers.hasMouse = playerInfo.hasMouse;
						}
					}
				}
			}
			catch
			{
			}
			return true;
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x0005FA1C File Offset: 0x0005DC1C
		private bool LoadJoystickAssignmentsNow(UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo data)
		{
			try
			{
				if (ReInput.controllers.joystickCount == 0)
				{
					return false;
				}
				if (data == null && (data = this.LoadControllerAssignmentData()) == null)
				{
					return false;
				}
				foreach (Player player in ReInput.players.AllPlayers)
				{
					player.controllers.ClearControllersOfType(2);
				}
				List<UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo> list = (this.loadJoystickAssignments ? new List<UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo>() : null);
				foreach (Player player2 in ReInput.players.AllPlayers)
				{
					if (data.ContainsPlayer(player2.id))
					{
						UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo playerInfo = data.players[data.IndexOfPlayer(player2.id)];
						for (int i = 0; i < playerInfo.joystickCount; i++)
						{
							UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo joystickInfo2 = playerInfo.joysticks[i];
							if (joystickInfo2 != null)
							{
								Joystick joystick = this.FindJoystickPrecise(joystickInfo2);
								if (joystick != null)
								{
									if (list.Find((UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo x) => x.joystick == joystick) == null)
									{
										list.Add(new UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo(joystick, joystickInfo2.id));
									}
									player2.controllers.AddController(joystick, false);
								}
							}
						}
					}
				}
				if (this.allowImpreciseJoystickAssignmentMatching)
				{
					foreach (Player player3 in ReInput.players.AllPlayers)
					{
						if (data.ContainsPlayer(player3.id))
						{
							UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo playerInfo2 = data.players[data.IndexOfPlayer(player3.id)];
							for (int j = 0; j < playerInfo2.joystickCount; j++)
							{
								UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo joystickInfo = playerInfo2.joysticks[j];
								if (joystickInfo != null)
								{
									Joystick joystick2 = null;
									int num = list.FindIndex((UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo x) => x.oldJoystickId == joystickInfo.id);
									if (num >= 0)
									{
										joystick2 = list[num].joystick;
									}
									else
									{
										List<Joystick> list2;
										if (!this.TryFindJoysticksImprecise(joystickInfo, out list2))
										{
											goto IL_0298;
										}
										using (List<Joystick>.Enumerator enumerator2 = list2.GetEnumerator())
										{
											while (enumerator2.MoveNext())
											{
												Joystick match = enumerator2.Current;
												if (list.Find((UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo x) => x.joystick == match) == null)
												{
													joystick2 = match;
													break;
												}
											}
										}
										if (joystick2 == null)
										{
											goto IL_0298;
										}
										list.Add(new UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo(joystick2, joystickInfo.id));
									}
									player3.controllers.AddController(joystick2, false);
								}
								IL_0298:;
							}
						}
					}
				}
			}
			catch
			{
			}
			if (ReInput.configuration.autoAssignJoysticks)
			{
				ReInput.controllers.AutoAssignJoysticks();
			}
			return true;
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x0005FD88 File Offset: 0x0005DF88
		private UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo LoadControllerAssignmentData()
		{
			UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo controllerAssignmentSaveInfo;
			try
			{
				if (!PlayerPrefs.HasKey(this.playerPrefsKey_controllerAssignments))
				{
					controllerAssignmentSaveInfo = null;
				}
				else
				{
					string @string = PlayerPrefs.GetString(this.playerPrefsKey_controllerAssignments);
					if (string.IsNullOrEmpty(@string))
					{
						controllerAssignmentSaveInfo = null;
					}
					else
					{
						UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo controllerAssignmentSaveInfo2 = JsonParser.FromJson<UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo>(@string);
						if (controllerAssignmentSaveInfo2 == null || controllerAssignmentSaveInfo2.playerCount == 0)
						{
							controllerAssignmentSaveInfo = null;
						}
						else
						{
							controllerAssignmentSaveInfo = controllerAssignmentSaveInfo2;
						}
					}
				}
			}
			catch
			{
				controllerAssignmentSaveInfo = null;
			}
			return controllerAssignmentSaveInfo;
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x0001145F File Offset: 0x0000F65F
		private IEnumerator LoadJoystickAssignmentsDeferred()
		{
			this.deferredJoystickAssignmentLoadPending = true;
			yield return new WaitForEndOfFrame();
			if (!ReInput.isReady)
			{
				yield break;
			}
			this.LoadJoystickAssignmentsNow(null);
			this.SaveControllerAssignments();
			this.deferredJoystickAssignmentLoadPending = false;
			yield break;
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x0005FDF0 File Offset: 0x0005DFF0
		private void SaveAll()
		{
			IList<Player> allPlayers = ReInput.players.AllPlayers;
			for (int i = 0; i < allPlayers.Count; i++)
			{
				this.SavePlayerDataNow(allPlayers[i]);
			}
			this.SaveAllJoystickCalibrationData();
			if (this.loadControllerAssignments)
			{
				this.SaveControllerAssignments();
			}
			PlayerPrefs.Save();
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x0001146E File Offset: 0x0000F66E
		private void SavePlayerDataNow(int playerId)
		{
			this.SavePlayerDataNow(ReInput.players.GetPlayer(playerId));
			PlayerPrefs.Save();
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x0005FE40 File Offset: 0x0005E040
		private void SavePlayerDataNow(Player player)
		{
			if (player == null)
			{
				return;
			}
			PlayerSaveData saveData = player.GetSaveData(true);
			this.SaveInputBehaviors(player, saveData);
			this.SaveControllerMaps(player, saveData);
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x0005FE6C File Offset: 0x0005E06C
		private void SaveAllJoystickCalibrationData()
		{
			IList<Joystick> joysticks = ReInput.controllers.Joysticks;
			for (int i = 0; i < joysticks.Count; i++)
			{
				this.SaveJoystickCalibrationData(joysticks[i]);
			}
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x00011486 File Offset: 0x0000F686
		private void SaveJoystickCalibrationData(int joystickId)
		{
			this.SaveJoystickCalibrationData(ReInput.controllers.GetJoystick(joystickId));
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x0005FEA4 File Offset: 0x0005E0A4
		private void SaveJoystickCalibrationData(Joystick joystick)
		{
			if (joystick == null)
			{
				return;
			}
			JoystickCalibrationMapSaveData calibrationMapSaveData = joystick.GetCalibrationMapSaveData();
			PlayerPrefs.SetString(this.GetJoystickCalibrationMapPlayerPrefsKey(joystick), calibrationMapSaveData.map.ToXmlString());
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x0005FED4 File Offset: 0x0005E0D4
		private void SaveJoystickData(int joystickId)
		{
			IList<Player> allPlayers = ReInput.players.AllPlayers;
			for (int i = 0; i < allPlayers.Count; i++)
			{
				Player player = allPlayers[i];
				if (player.controllers.ContainsController(2, joystickId))
				{
					this.SaveControllerMaps(player.id, 2, joystickId);
				}
			}
			this.SaveJoystickCalibrationData(joystickId);
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x00011499 File Offset: 0x0000F699
		private void SaveControllerDataNow(int playerId, ControllerType controllerType, int controllerId)
		{
			this.SaveControllerMaps(playerId, controllerType, controllerId);
			this.SaveControllerDataNow(controllerType, controllerId);
			PlayerPrefs.Save();
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x000114B1 File Offset: 0x0000F6B1
		private void SaveControllerDataNow(ControllerType controllerType, int controllerId)
		{
			if (controllerType == 2)
			{
				this.SaveJoystickCalibrationData(controllerId);
			}
			PlayerPrefs.Save();
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x0005FF2C File Offset: 0x0005E12C
		private void SaveControllerMaps(Player player, PlayerSaveData playerSaveData)
		{
			foreach (ControllerMapSaveData controllerMapSaveData in playerSaveData.AllControllerMapSaveData)
			{
				this.SaveControllerMap(player, controllerMapSaveData.map);
			}
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x0005FF80 File Offset: 0x0005E180
		private void SaveControllerMaps(int playerId, ControllerType controllerType, int controllerId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return;
			}
			if (!player.controllers.ContainsController(controllerType, controllerId))
			{
				return;
			}
			ControllerMapSaveData[] mapSaveData = player.controllers.maps.GetMapSaveData(controllerType, controllerId, true);
			if (mapSaveData == null)
			{
				return;
			}
			for (int i = 0; i < mapSaveData.Length; i++)
			{
				this.SaveControllerMap(player, mapSaveData[i].map);
			}
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x0005FFE4 File Offset: 0x0005E1E4
		private void SaveControllerMap(Player player, ControllerMap controllerMap)
		{
			PlayerPrefs.SetString(this.GetControllerMapPlayerPrefsKey(player, controllerMap.controller.identifier, controllerMap.categoryId, controllerMap.layoutId, 2), controllerMap.ToXmlString());
			PlayerPrefs.SetString(this.GetControllerMapKnownActionIdsPlayerPrefsKey(player, controllerMap.controller.identifier, controllerMap.categoryId, controllerMap.layoutId, 2), this.allActionIdsString);
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x00060048 File Offset: 0x0005E248
		private void SaveInputBehaviors(Player player, PlayerSaveData playerSaveData)
		{
			if (player == null)
			{
				return;
			}
			InputBehavior[] inputBehaviors = playerSaveData.inputBehaviors;
			for (int i = 0; i < inputBehaviors.Length; i++)
			{
				this.SaveInputBehaviorNow(player, inputBehaviors[i]);
			}
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x0006007C File Offset: 0x0005E27C
		private void SaveInputBehaviorNow(int playerId, int behaviorId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return;
			}
			InputBehavior inputBehavior = ReInput.mapping.GetInputBehavior(playerId, behaviorId);
			if (inputBehavior == null)
			{
				return;
			}
			this.SaveInputBehaviorNow(player, inputBehavior);
			PlayerPrefs.Save();
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x000114C3 File Offset: 0x0000F6C3
		private void SaveInputBehaviorNow(Player player, InputBehavior inputBehavior)
		{
			if (player == null || inputBehavior == null)
			{
				return;
			}
			PlayerPrefs.SetString(this.GetInputBehaviorPlayerPrefsKey(player, inputBehavior.id), inputBehavior.ToXmlString());
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x000600B8 File Offset: 0x0005E2B8
		private bool SaveControllerAssignments()
		{
			try
			{
				UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo controllerAssignmentSaveInfo = new UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo(ReInput.players.allPlayerCount);
				for (int i = 0; i < ReInput.players.allPlayerCount; i++)
				{
					Player player = ReInput.players.AllPlayers[i];
					UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo playerInfo = new UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo();
					controllerAssignmentSaveInfo.players[i] = playerInfo;
					playerInfo.id = player.id;
					playerInfo.hasKeyboard = player.controllers.hasKeyboard;
					playerInfo.hasMouse = player.controllers.hasMouse;
					UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo[] array = new UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo[player.controllers.joystickCount];
					playerInfo.joysticks = array;
					for (int j = 0; j < player.controllers.joystickCount; j++)
					{
						Joystick joystick = player.controllers.Joysticks[j];
						array[j] = new UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo
						{
							instanceGuid = joystick.deviceInstanceGuid,
							id = joystick.id,
							hardwareIdentifier = joystick.hardwareIdentifier
						};
					}
				}
				PlayerPrefs.SetString(this.playerPrefsKey_controllerAssignments, JsonWriter.ToJson(controllerAssignmentSaveInfo));
				PlayerPrefs.Save();
			}
			catch
			{
			}
			return true;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x000114E4 File Offset: 0x0000F6E4
		private bool ControllerAssignmentSaveDataExists()
		{
			return PlayerPrefs.HasKey(this.playerPrefsKey_controllerAssignments) && !string.IsNullOrEmpty(PlayerPrefs.GetString(this.playerPrefsKey_controllerAssignments));
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x0001150A File Offset: 0x0000F70A
		private string GetBasePlayerPrefsKey(Player player)
		{
			return this.playerPrefsKeyPrefix + "|playerName=" + player.name;
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x00011522 File Offset: 0x0000F722
		private string GetControllerMapPlayerPrefsKey(Player player, ControllerIdentifier controllerIdentifier, int categoryId, int layoutId, int ppKeyVersion)
		{
			return this.GetBasePlayerPrefsKey(player) + "|dataType=ControllerMap" + UserDataStore_PlayerPrefs.GetControllerMapPlayerPrefsKeyCommonSuffix(player, controllerIdentifier, categoryId, layoutId, ppKeyVersion);
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x00011546 File Offset: 0x0000F746
		private string GetControllerMapKnownActionIdsPlayerPrefsKey(Player player, ControllerIdentifier controllerIdentifier, int categoryId, int layoutId, int ppKeyVersion)
		{
			return this.GetBasePlayerPrefsKey(player) + "|dataType=ControllerMap_KnownActionIds" + UserDataStore_PlayerPrefs.GetControllerMapPlayerPrefsKeyCommonSuffix(player, controllerIdentifier, categoryId, layoutId, ppKeyVersion);
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x000601F8 File Offset: 0x0005E3F8
		private static string GetControllerMapPlayerPrefsKeyCommonSuffix(Player player, ControllerIdentifier controllerIdentifier, int categoryId, int layoutId, int ppKeyVersion)
		{
			string text = "";
			if (ppKeyVersion >= 2)
			{
				text = text + "|kv=" + ppKeyVersion.ToString();
			}
			text = text + "|controllerMapType=" + UserDataStore_PlayerPrefs.GetControllerMapType(controllerIdentifier.controllerType).Name;
			text = string.Concat(new string[]
			{
				text,
				"|categoryId=",
				categoryId.ToString(),
				"|layoutId=",
				layoutId.ToString()
			});
			if (ppKeyVersion >= 2)
			{
				text = text + "|hardwareGuid=" + controllerIdentifier.hardwareTypeGuid.ToString();
				if (controllerIdentifier.hardwareTypeGuid == Guid.Empty)
				{
					text = text + "|hardwareIdentifier=" + controllerIdentifier.hardwareIdentifier;
				}
				if (controllerIdentifier.controllerType == 2)
				{
					text = text + "|duplicate=" + UserDataStore_PlayerPrefs.GetDuplicateIndex(player, controllerIdentifier).ToString();
				}
			}
			else
			{
				text = text + "|hardwareIdentifier=" + controllerIdentifier.hardwareIdentifier;
				if (controllerIdentifier.controllerType == 2)
				{
					text = text + "|hardwareGuid=" + controllerIdentifier.hardwareTypeGuid.ToString();
					if (ppKeyVersion >= 1)
					{
						text = text + "|duplicate=" + UserDataStore_PlayerPrefs.GetDuplicateIndex(player, controllerIdentifier).ToString();
					}
				}
			}
			return text;
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x00060348 File Offset: 0x0005E548
		private string GetJoystickCalibrationMapPlayerPrefsKey(Joystick joystick)
		{
			return this.playerPrefsKeyPrefix + "|dataType=CalibrationMap" + "|controllerType=" + joystick.type.ToString() + "|hardwareIdentifier=" + joystick.hardwareIdentifier + "|hardwareGuid=" + joystick.hardwareTypeGuid.ToString();
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0001156A File Offset: 0x0000F76A
		private string GetInputBehaviorPlayerPrefsKey(Player player, int inputBehaviorId)
		{
			return this.GetBasePlayerPrefsKey(player) + "|dataType=InputBehavior" + "|id=" + inputBehaviorId.ToString();
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x000603B4 File Offset: 0x0005E5B4
		private string GetControllerMapXml(Player player, ControllerIdentifier controllerIdentifier, int categoryId, int layoutId)
		{
			for (int i = 2; i >= 0; i--)
			{
				string controllerMapPlayerPrefsKey = this.GetControllerMapPlayerPrefsKey(player, controllerIdentifier, categoryId, layoutId, i);
				if (PlayerPrefs.HasKey(controllerMapPlayerPrefsKey))
				{
					return PlayerPrefs.GetString(controllerMapPlayerPrefsKey);
				}
			}
			return null;
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x000603EC File Offset: 0x0005E5EC
		private List<int> GetControllerMapKnownActionIds(Player player, ControllerIdentifier controllerIdentifier, int categoryId, int layoutId)
		{
			List<int> list = new List<int>();
			string text = null;
			bool flag = false;
			for (int i = 2; i >= 0; i--)
			{
				text = this.GetControllerMapKnownActionIdsPlayerPrefsKey(player, controllerIdentifier, categoryId, layoutId, i);
				if (PlayerPrefs.HasKey(text))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return list;
			}
			string @string = PlayerPrefs.GetString(text);
			if (string.IsNullOrEmpty(@string))
			{
				return list;
			}
			string[] array = @string.Split(new char[] { ',' });
			for (int j = 0; j < array.Length; j++)
			{
				int num;
				if (!string.IsNullOrEmpty(array[j]) && int.TryParse(array[j], out num))
				{
					list.Add(num);
				}
			}
			return list;
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0006048C File Offset: 0x0005E68C
		private string GetJoystickCalibrationMapXml(Joystick joystick)
		{
			string joystickCalibrationMapPlayerPrefsKey = this.GetJoystickCalibrationMapPlayerPrefsKey(joystick);
			if (!PlayerPrefs.HasKey(joystickCalibrationMapPlayerPrefsKey))
			{
				return string.Empty;
			}
			return PlayerPrefs.GetString(joystickCalibrationMapPlayerPrefsKey);
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x000604B8 File Offset: 0x0005E6B8
		private string GetInputBehaviorXml(Player player, int id)
		{
			string inputBehaviorPlayerPrefsKey = this.GetInputBehaviorPlayerPrefsKey(player, id);
			if (!PlayerPrefs.HasKey(inputBehaviorPlayerPrefsKey))
			{
				return string.Empty;
			}
			return PlayerPrefs.GetString(inputBehaviorPlayerPrefsKey);
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x000604E4 File Offset: 0x0005E6E4
		private void AddDefaultMappingsForNewActions(ControllerIdentifier controllerIdentifier, ControllerMap controllerMap, List<int> knownActionIds)
		{
			if (controllerMap == null || knownActionIds == null)
			{
				return;
			}
			if (knownActionIds == null || knownActionIds.Count == 0)
			{
				return;
			}
			ControllerMap controllerMapInstance = ReInput.mapping.GetControllerMapInstance(controllerIdentifier, controllerMap.categoryId, controllerMap.layoutId);
			if (controllerMapInstance == null)
			{
				return;
			}
			List<int> list = new List<int>();
			foreach (int num in this.allActionIds)
			{
				if (!knownActionIds.Contains(num))
				{
					list.Add(num);
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			foreach (ActionElementMap actionElementMap in controllerMapInstance.AllMaps)
			{
				if (list.Contains(actionElementMap.actionId) && !controllerMap.DoesElementAssignmentConflict(actionElementMap))
				{
					ElementAssignment elementAssignment;
					elementAssignment..ctor(controllerMap.controllerType, actionElementMap.elementType, actionElementMap.elementIdentifierId, actionElementMap.axisRange, actionElementMap.keyCode, actionElementMap.modifierKeyFlags, actionElementMap.actionId, actionElementMap.axisContribution, actionElementMap.invert);
					controllerMap.CreateElementMap(elementAssignment);
				}
			}
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x00060620 File Offset: 0x0005E820
		private Joystick FindJoystickPrecise(UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo joystickInfo)
		{
			if (joystickInfo == null)
			{
				return null;
			}
			if (joystickInfo.instanceGuid == Guid.Empty)
			{
				return null;
			}
			IList<Joystick> joysticks = ReInput.controllers.Joysticks;
			for (int i = 0; i < joysticks.Count; i++)
			{
				if (joysticks[i].deviceInstanceGuid == joystickInfo.instanceGuid)
				{
					return joysticks[i];
				}
			}
			return null;
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x00060684 File Offset: 0x0005E884
		private bool TryFindJoysticksImprecise(UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo joystickInfo, out List<Joystick> matches)
		{
			matches = null;
			if (joystickInfo == null)
			{
				return false;
			}
			if (string.IsNullOrEmpty(joystickInfo.hardwareIdentifier))
			{
				return false;
			}
			IList<Joystick> joysticks = ReInput.controllers.Joysticks;
			for (int i = 0; i < joysticks.Count; i++)
			{
				if (string.Equals(joysticks[i].hardwareIdentifier, joystickInfo.hardwareIdentifier, StringComparison.OrdinalIgnoreCase))
				{
					if (matches == null)
					{
						matches = new List<Joystick>();
					}
					matches.Add(joysticks[i]);
				}
			}
			return matches != null;
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x000606FC File Offset: 0x0005E8FC
		private static int GetDuplicateIndex(Player player, ControllerIdentifier controllerIdentifier)
		{
			Controller controller = ReInput.controllers.GetController(controllerIdentifier);
			if (controller == null)
			{
				return 0;
			}
			int num = 0;
			foreach (Controller controller2 in player.controllers.Controllers)
			{
				if (controller2.type == controller.type)
				{
					bool flag = false;
					if (controller.type == 2)
					{
						if ((controller2 as Joystick).hardwareTypeGuid != controller.hardwareTypeGuid)
						{
							continue;
						}
						if (controller.hardwareTypeGuid != Guid.Empty)
						{
							flag = true;
						}
					}
					if (flag || !(controller2.hardwareIdentifier != controller.hardwareIdentifier))
					{
						if (controller2 == controller)
						{
							return num;
						}
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x000607CC File Offset: 0x0005E9CC
		private void RefreshLayoutManager(int playerId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return;
			}
			player.controllers.maps.layoutManager.Apply();
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x00060800 File Offset: 0x0005EA00
		private static Type GetControllerMapType(ControllerType controllerType)
		{
			switch (controllerType)
			{
			case 0:
				return typeof(KeyboardMap);
			case 1:
				return typeof(MouseMap);
			case 2:
				return typeof(JoystickMap);
			default:
				if (controllerType == 20)
				{
					return typeof(CustomControllerMap);
				}
				Debug.LogWarning("Rewired: Unknown ControllerType " + controllerType.ToString());
				return null;
			}
		}

		// Token: 0x04001AFE RID: 6910
		private const string thisScriptName = "UserDataStore_PlayerPrefs";

		// Token: 0x04001AFF RID: 6911
		private const string logPrefix = "Rewired: ";

		// Token: 0x04001B00 RID: 6912
		private const string editorLoadedMessage = "\n***IMPORTANT:*** Changes made to the Rewired Input Manager configuration after the last time XML data was saved WILL NOT be used because the loaded old saved data has overwritten these values. If you change something in the Rewired Input Manager such as a Joystick Map or Input Behavior settings, you will not see these changes reflected in the current configuration. Clear PlayerPrefs using the inspector option on the UserDataStore_PlayerPrefs component.";

		// Token: 0x04001B01 RID: 6913
		private const string playerPrefsKeySuffix_controllerAssignments = "ControllerAssignments";

		// Token: 0x04001B02 RID: 6914
		private const int controllerMapPPKeyVersion_original = 0;

		// Token: 0x04001B03 RID: 6915
		private const int controllerMapPPKeyVersion_includeDuplicateJoystickIndex = 1;

		// Token: 0x04001B04 RID: 6916
		private const int controllerMapPPKeyVersion_supportDisconnectedControllers = 2;

		// Token: 0x04001B05 RID: 6917
		private const int controllerMapPPKeyVersion_includeFormatVersion = 2;

		// Token: 0x04001B06 RID: 6918
		private const int controllerMapPPKeyVersion = 2;

		// Token: 0x04001B07 RID: 6919
		[Tooltip("Should this script be used? If disabled, nothing will be saved or loaded.")]
		[SerializeField]
		private bool isEnabled = true;

		// Token: 0x04001B08 RID: 6920
		[Tooltip("Should saved data be loaded on start?")]
		[SerializeField]
		private bool loadDataOnStart = true;

		// Token: 0x04001B09 RID: 6921
		[Tooltip("Should Player Joystick assignments be saved and loaded? This is not totally reliable for all Joysticks on all platforms. Some platforms/input sources do not provide enough information to reliably save assignments from session to session and reboot to reboot.")]
		[SerializeField]
		private bool loadJoystickAssignments = true;

		// Token: 0x04001B0A RID: 6922
		[Tooltip("Should Player Keyboard assignments be saved and loaded?")]
		[SerializeField]
		private bool loadKeyboardAssignments = true;

		// Token: 0x04001B0B RID: 6923
		[Tooltip("Should Player Mouse assignments be saved and loaded?")]
		[SerializeField]
		private bool loadMouseAssignments = true;

		// Token: 0x04001B0C RID: 6924
		[Tooltip("The PlayerPrefs key prefix. Change this to change how keys are stored in PlayerPrefs. Changing this will make saved data already stored with the old key no longer accessible.")]
		[SerializeField]
		private string playerPrefsKeyPrefix = "RewiredSaveData";

		// Token: 0x04001B0D RID: 6925
		[NonSerialized]
		private bool allowImpreciseJoystickAssignmentMatching = true;

		// Token: 0x04001B0E RID: 6926
		[NonSerialized]
		private bool deferredJoystickAssignmentLoadPending;

		// Token: 0x04001B0F RID: 6927
		[NonSerialized]
		private bool wasJoystickEverDetected;

		// Token: 0x04001B10 RID: 6928
		[NonSerialized]
		private List<int> __allActionIds;

		// Token: 0x04001B11 RID: 6929
		[NonSerialized]
		private string __allActionIdsString;

		// Token: 0x0200040E RID: 1038
		private class ControllerAssignmentSaveInfo
		{
			// Token: 0x17000417 RID: 1047
			// (get) Token: 0x0600166F RID: 5743 RVA: 0x000115CB File Offset: 0x0000F7CB
			public int playerCount
			{
				get
				{
					if (this.players == null)
					{
						return 0;
					}
					return this.players.Length;
				}
			}

			// Token: 0x06001670 RID: 5744 RVA: 0x0000227F File Offset: 0x0000047F
			public ControllerAssignmentSaveInfo()
			{
			}

			// Token: 0x06001671 RID: 5745 RVA: 0x00060870 File Offset: 0x0005EA70
			public ControllerAssignmentSaveInfo(int playerCount)
			{
				this.players = new UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo[playerCount];
				for (int i = 0; i < playerCount; i++)
				{
					this.players[i] = new UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo();
				}
			}

			// Token: 0x06001672 RID: 5746 RVA: 0x000608A8 File Offset: 0x0005EAA8
			public int IndexOfPlayer(int playerId)
			{
				for (int i = 0; i < this.playerCount; i++)
				{
					if (this.players[i] != null && this.players[i].id == playerId)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06001673 RID: 5747 RVA: 0x000115DF File Offset: 0x0000F7DF
			public bool ContainsPlayer(int playerId)
			{
				return this.IndexOfPlayer(playerId) >= 0;
			}

			// Token: 0x04001B12 RID: 6930
			public UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo[] players;

			// Token: 0x0200040F RID: 1039
			public class PlayerInfo
			{
				// Token: 0x17000418 RID: 1048
				// (get) Token: 0x06001674 RID: 5748 RVA: 0x000115EE File Offset: 0x0000F7EE
				public int joystickCount
				{
					get
					{
						if (this.joysticks == null)
						{
							return 0;
						}
						return this.joysticks.Length;
					}
				}

				// Token: 0x06001675 RID: 5749 RVA: 0x000608E4 File Offset: 0x0005EAE4
				public int IndexOfJoystick(int joystickId)
				{
					for (int i = 0; i < this.joystickCount; i++)
					{
						if (this.joysticks[i] != null && this.joysticks[i].id == joystickId)
						{
							return i;
						}
					}
					return -1;
				}

				// Token: 0x06001676 RID: 5750 RVA: 0x00011602 File Offset: 0x0000F802
				public bool ContainsJoystick(int joystickId)
				{
					return this.IndexOfJoystick(joystickId) >= 0;
				}

				// Token: 0x04001B13 RID: 6931
				public int id;

				// Token: 0x04001B14 RID: 6932
				public bool hasKeyboard;

				// Token: 0x04001B15 RID: 6933
				public bool hasMouse;

				// Token: 0x04001B16 RID: 6934
				public UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo[] joysticks;
			}

			// Token: 0x02000410 RID: 1040
			public class JoystickInfo
			{
				// Token: 0x04001B17 RID: 6935
				public Guid instanceGuid;

				// Token: 0x04001B18 RID: 6936
				public string hardwareIdentifier;

				// Token: 0x04001B19 RID: 6937
				public int id;
			}
		}

		// Token: 0x02000411 RID: 1041
		private class JoystickAssignmentHistoryInfo
		{
			// Token: 0x06001679 RID: 5753 RVA: 0x00011611 File Offset: 0x0000F811
			public JoystickAssignmentHistoryInfo(Joystick joystick, int oldJoystickId)
			{
				if (joystick == null)
				{
					throw new ArgumentNullException("joystick");
				}
				this.joystick = joystick;
				this.oldJoystickId = oldJoystickId;
			}

			// Token: 0x04001B1A RID: 6938
			public readonly Joystick joystick;

			// Token: 0x04001B1B RID: 6939
			public readonly int oldJoystickId;
		}
	}
}
