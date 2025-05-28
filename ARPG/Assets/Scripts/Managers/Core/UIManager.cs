using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class PopupArg
{
    public static PopupArg empty = new PopupArg();
}

public class PopupWaitData
{
    public Define.PopupType popupType;
    public PopupArg popupArg = PopupArg.empty;
}

public class UIManager
{
    public Transform PopupCanvas;

    public UI_Scene GetSceneUI { get { return _sceneUI; } }

    private UI_Scene _sceneUI;

    private List<PopupWaitData> _waitActivePopups = new List<PopupWaitData>();
    private List<UI_Popup> _waitRemovePopups = new List<UI_Popup>();
    private List<UI_Popup> _activePopups = new List<UI_Popup>();

    public void OnUpdate()
    {
        foreach (var waitData in _waitActivePopups)
            ShowPopupBox(waitData);

        _waitActivePopups.Clear();


        foreach (var waitData in _waitRemovePopups)
            ClosePopupBox(waitData);

        _waitActivePopups.Clear();
    }

    public void ShowPopupBox(Define.PopupType popupType, PopupArg arg)
    {
        PopupWaitData waitData = new PopupWaitData();
        waitData.popupType = popupType;
        waitData.popupArg = arg;
        
        _waitActivePopups.Add(waitData);
    }

    public async void ShowPopupBox(PopupWaitData waitData)
    {
        GameObject go = await Managers.Resource.InstantiateAsync("Popup", waitData.popupType.ToString(), PopupCanvas);
        if (go == null)
            return;

        go.name = waitData.popupType.ToString();

        UI_Popup popup = go.GetComponent<UI_Popup>();
        if (popup == null)
            return;

        popup.Init(waitData.popupArg);
        _activePopups.Add(popup);
    }

    public void ClosePopupBox(GameObject go)
    {
        if (go != null)
        {
            var popup = go.GetComponent<UI_Popup>();
            if (popup == null)
                return;

            _waitRemovePopups.Add(popup);
        }
    }

    public void ClosePopupBox(UI_Popup popup)
    {
        if (popup != null)
        {
            popup.OnClosePopupBox();
            Object.Destroy(popup);
        }
    }

    public void RegsiterSceneUI(UI_Scene sceneUI)
    {
        _sceneUI = sceneUI;
    }

    public void UnrgsiterSceneUI(UI_Scene sceneUI)
    {
        if (_sceneUI == sceneUI)
            _sceneUI = null;
    }

    public async UniTask<GameObject> GetPopupPrefab(Define.PopupType popupType)
    {
        GameObject prefab = await Managers.Resource.LoadGameObjectAsync("Popup", popupType.ToString());
        return prefab;
    }
}