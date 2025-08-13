using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

// Token: 0x02000072 RID: 114
public class LoadScene : MonoBehaviour
{
	// Token: 0x0600016F RID: 367 RVA: 0x00003382 File Offset: 0x00001582
	private void Start()
	{
		if (this.automatic)
		{
			this.DoLoadScene();
		}
	}

	// Token: 0x06000170 RID: 368 RVA: 0x0001C114 File Offset: 0x0001A314
	public void DoLoadScene()
	{
		if (this.triggerSave && Game.AllowedToSave)
		{
			GameData.g.WriteToDisk();
		}
		this.beforeLoad.Invoke();
		if (this.overrideLoadType)
		{
			if (this.isSceneAsset)
			{
				LoadSceneSequence.LoadScene(this.sceneToLoad, this.loadType);
				return;
			}
			LoadSceneSequence.LoadScene(this.preloadedSceneIndex, this.loadType);
			return;
		}
		else
		{
			if (this.isSceneAsset)
			{
				LoadSceneSequence.LoadScene(this.sceneToLoad, LoadSceneSequence.LoadType.LoadingScreen);
				return;
			}
			LoadSceneSequence.LoadScene(this.preloadedSceneIndex, LoadSceneSequence.LoadType.LoadingScreen);
			return;
		}
	}

	// Token: 0x0400022A RID: 554
	public bool triggerSave;

	// Token: 0x0400022B RID: 555
	public bool automatic;

	// Token: 0x0400022C RID: 556
	public bool isSceneAsset = true;

	// Token: 0x0400022D RID: 557
	public AssetReference sceneToLoad;

	// Token: 0x0400022E RID: 558
	public int preloadedSceneIndex = -1;

	// Token: 0x0400022F RID: 559
	public UnityEvent beforeLoad;

	// Token: 0x04000230 RID: 560
	public bool overrideLoadType;

	// Token: 0x04000231 RID: 561
	public LoadSceneSequence.LoadType loadType;
}
