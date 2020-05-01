using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class LevelBoxSelection : MonoBehaviour, IPointerClickHandler
{
    public string levelNumber;
    public string levelName;
    public string levelUnlocked;
    public string levelPoints;

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.clickCount == 2) {
            GameHandler.game.StartGame(Int32.Parse(levelNumber));
        }
    }
}
