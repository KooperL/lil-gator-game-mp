using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

// Token: 0x02000058 RID: 88
public class LoadScene : MonoBehaviour
{
	// Token: 0x06000144 RID: 324 RVA: 0x00007BE4 File Offset: 0x00005DE4
	private void Start()
	{
		if (this.automatic)
		{
			this.DoLoadScene();
		}
	}

	// Token: 0x06000145 RID: 325 RVA: 0x00007BF4 File Offset: 0x00005DF4
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

	// Token: 0x040001C3 RID: 451
	public bool triggerSave;

	// Token: 0x040001C4 RID: 452
	public bool automatic;

	// Token: 0x040001C5 RID: 453
	public bool isSceneAsset = true;

	// Token: 0x040001C6 RID: 454
	public AssetReference sceneToLoad;

	// Token: 0x040001C7 RID: 455
	public int preloadedSceneIndex = -1;

	// Token: 0x040001C8 RID: 456
	public UnityEvent beforeLoad;

	// Token: 0x040001C9 RID: 457
	public bool overrideLoadType;

	// Token: 0x040001CA RID: 458
	public LoadSceneSequence.LoadType loadType;
}
