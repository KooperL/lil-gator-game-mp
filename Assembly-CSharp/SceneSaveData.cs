using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneSaveData : MonoBehaviour
{
	// (get) Token: 0x06000F27 RID: 3879 RVA: 0x0000D248 File Offset: 0x0000B448
	// (set) Token: 0x06000F28 RID: 3880 RVA: 0x0000D266 File Offset: 0x0000B466
	public static SceneSaveData s
	{
		get
		{
			if (SceneSaveData.instance == null)
			{
				SceneSaveData.instance = global::UnityEngine.Object.FindObjectOfType<SceneSaveData>();
			}
			return SceneSaveData.instance;
		}
		set
		{
			SceneSaveData.instance = value;
		}
	}

	// Token: 0x06000F29 RID: 3881 RVA: 0x0000D26E File Offset: 0x0000B46E
	private void OnEnable()
	{
		SceneSaveData.s = this;
	}

	// Token: 0x06000F2A RID: 3882 RVA: 0x0000D276 File Offset: 0x0000B476
	private void OnDisable()
	{
		if (SceneSaveData.s == this)
		{
			SceneSaveData.s = null;
		}
	}

	// Token: 0x06000F2B RID: 3883 RVA: 0x0000D28B File Offset: 0x0000B48B
	private void Start()
	{
		this.AssignIDs();
		this.Load();
	}

	// Token: 0x06000F2C RID: 3884 RVA: 0x0004FFB4 File Offset: 0x0004E1B4
	private void Load()
	{
		if (GameData.g.gameSaveData == null || this.isLoaded)
		{
			return;
		}
		this.objectStates = GameData.g.gameSaveData.objectStates;
		if (this.objectStates.Length != this.persistentObjects.Length)
		{
			bool[] array = new bool[this.persistentObjects.Length];
			Array.ConstrainedCopy(this.objectStates, 0, array, 0, Mathf.Min(this.objectStates.Length, array.Length));
			this.objectStates = (GameData.g.gameSaveData.objectStates = array);
		}
		this.LoadPersistentObjects();
		this.isLoaded = true;
	}

	// Token: 0x06000F2D RID: 3885 RVA: 0x00050050 File Offset: 0x0004E250
	public void LoadPersistentObjects()
	{
		for (int i = 0; i < this.persistentObjects.Length; i++)
		{
			if (this.persistentObjects[i] != null)
			{
				if (this.objectStates.Length > i)
				{
					this.persistentObjects[i].Load(this.objectStates[i]);
					this.persistentObjects[i].isLoaded = true;
				}
				else
				{
					this.persistentObjects[i].Load(false);
				}
			}
		}
	}

	// Token: 0x06000F2E RID: 3886 RVA: 0x000500C0 File Offset: 0x0004E2C0
	public void SavePersistentObject(int id, bool state)
	{
		if (this.objectStates.Length <= id)
		{
			bool[] array = new bool[id + 1];
			this.objectStates.CopyTo(array, 0);
			this.objectStates = (GameData.g.gameSaveData.objectStates = array);
		}
		this.objectStates[id] = state;
	}

	// Token: 0x06000F2F RID: 3887 RVA: 0x0000D299 File Offset: 0x0000B499
	public bool GetPersistentObject(int id)
	{
		if (!this.isLoaded)
		{
			this.Load();
		}
		return this.objectStates != null && this.objectStates.Length > id && this.objectStates[id];
	}

	// Token: 0x06000F30 RID: 3888 RVA: 0x00050110 File Offset: 0x0004E310
	public bool HasPersistentObject(PersistentObject o)
	{
		if (this.persistentObjects != null)
		{
			for (int i = 0; i < this.persistentObjects.Length; i++)
			{
				if (this.persistentObjects[i] == o)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000F31 RID: 3889 RVA: 0x0005014C File Offset: 0x0004E34C
	public void RegisterPersistentObject(PersistentObject o)
	{
		if (Application.isPlaying)
		{
			Debug.LogError("Do not invoke in play mode!");
			return;
		}
		if (this.HasPersistentObject(o))
		{
			return;
		}
		if (this.persistentObjects == null)
		{
			this.persistentObjects = new PersistentObject[] { o };
			return;
		}
		Array.Resize<PersistentObject>(ref this.persistentObjects, this.persistentObjects.Length + 1);
		this.persistentObjects[this.persistentObjects.Length - 1] = o;
		o.id = this.persistentObjects.Length - 1;
	}

	// Token: 0x06000F32 RID: 3890 RVA: 0x000501C8 File Offset: 0x0004E3C8
	[ContextMenu("Remove Null Objects")]
	public void RemoveNullObjects()
	{
		List<PersistentObject> list = new List<PersistentObject>(this.persistentObjects);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] == null)
			{
				list.RemoveAt(i);
				i--;
			}
		}
		this.persistentObjects = list.ToArray();
	}

	// Token: 0x06000F33 RID: 3891 RVA: 0x0000D2C8 File Offset: 0x0000B4C8
	[ContextMenu("Find Persistent Objects")]
	public void FindPersistentObjects()
	{
		this.AssignIDs();
	}

	// Token: 0x06000F34 RID: 3892 RVA: 0x00050218 File Offset: 0x0004E418
	[ContextMenu("Assign IDs")]
	public void AssignIDs()
	{
		for (int i = 0; i < this.persistentObjects.Length; i++)
		{
			if (this.persistentObjects[i] != null)
			{
				this.persistentObjects[i].id = i;
			}
		}
	}

	private static SceneSaveData instance;

	private bool isLoaded;

	public PersistentObject[] persistentObjects;

	private bool[] objectStates;

	private const bool reuseExistingSlots = false;
}
