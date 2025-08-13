using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Rewired.Utils;
using Rewired.Utils.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Rewired
{
	// Token: 0x02000407 RID: 1031
	[AddComponentMenu("Rewired/Input Manager")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class InputManager : InputManager_Base
	{
		// Token: 0x0600156B RID: 5483 RVA: 0x00010DF1 File Offset: 0x0000EFF1
		protected override void OnInitialized()
		{
			this.SubscribeEvents();
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x00010DF9 File Offset: 0x0000EFF9
		protected override void OnDeinitialized()
		{
			this.UnsubscribeEvents();
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0005ED14 File Offset: 0x0005CF14
		protected override void DetectPlatform()
		{
			this.scriptingBackend = 0;
			this.scriptingAPILevel = 0;
			this.editorPlatform = 0;
			this.platform = 0;
			this.webplayerPlatform = 0;
			this.isEditor = false;
			if (SystemInfo.deviceName == null)
			{
				string empty = string.Empty;
			}
			if (SystemInfo.deviceModel == null)
			{
				string empty2 = string.Empty;
			}
			this.platform = 1;
			this.scriptingBackend = 0;
			this.scriptingAPILevel = 3;
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x00002229 File Offset: 0x00000429
		protected override void CheckRecompile()
		{
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x00010E01 File Offset: 0x0000F001
		protected override IExternalTools GetExternalTools()
		{
			return new ExternalTools();
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x00010E08 File Offset: 0x0000F008
		private bool CheckDeviceName(string searchPattern, string deviceName, string deviceModel)
		{
			return Regex.IsMatch(deviceName, searchPattern, RegexOptions.IgnoreCase) || Regex.IsMatch(deviceModel, searchPattern, RegexOptions.IgnoreCase);
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x00010E1E File Offset: 0x0000F01E
		private void SubscribeEvents()
		{
			this.UnsubscribeEvents();
			SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(this.OnSceneLoaded);
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x00010E37 File Offset: 0x0000F037
		private void UnsubscribeEvents()
		{
			SceneManager.sceneLoaded -= new UnityAction<Scene, LoadSceneMode>(this.OnSceneLoaded);
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x00010E4A File Offset: 0x0000F04A
		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			base.OnSceneLoaded();
		}

		// Token: 0x04001AE0 RID: 6880
		private bool ignoreRecompile;
	}
}
