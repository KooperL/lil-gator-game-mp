using System;

public interface IItemBehaviour
{
	void Input(bool isDown, bool isHeld);

	void Cancel();

	void SetEquipped(bool isEquipped);

	void OnRemove();

	void SetIndex(int index);
}
