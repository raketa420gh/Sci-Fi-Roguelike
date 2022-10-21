using UnityEngine;

public interface IGameFactory
{
    Player CreatePlayerCharacter(Vector3 position);

    void CreateHUD();
}