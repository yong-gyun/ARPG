using Common.Input;
using Data.Contents.LoaderForm;
using System;
using System.Collections.Generic;
using System.IO;
using UniRx;
using UnityEngine;

using static Define;

namespace Common.Input
{
    [SerializeField]
    public class UserInputSettingAllData
    {
        public List<UserInputSettingData> userInputSettingDatas;
    }


    [Serializable]
    public class UserInputSettingData
    {
        public KeyID keyID;
        public int keyCode;
    }
}

public class InputManager
{
    public string Path { get { return Application.persistentDataPath + "/UserInputSettingData.json"; } }
    public IObservable<(KeyID keyID, InputState inputState)> InputKeyEventHandler { get { return _inputKeyCallback.AsObservable(); } }
    public IObservable<(KeyID keyID, InputState inputState)> InputMouseEventHandler { get { return _inputMouseCallback.AsObservable(); } }
    
    private Subject<(KeyID keyID, InputState inputState)> _inputKeyCallback = new Subject<(KeyID, InputState)>();
    
    private Subject<(KeyID keyID, InputState inputState)> _inputMouseCallback = new Subject<(KeyID, InputState)>();
    
    private Dictionary<KeyID, int> _userKeySettingDatas = new Dictionary<KeyID, int>();

    public void Init()
    {
        if (File.Exists(Path) == false)
        {
            LoadDefaultData();
        }
        else
        {
            LoadData();
        }

        if (_userKeySettingDatas.Count == 0)
            LoadDefaultData();
    }

    private void LoadData()
    {
        string file = File.ReadAllText(Path);
        UserInputSettingAllData userInputAllDatas = JsonUtility.FromJson<UserInputSettingAllData>(file);
        if (userInputAllDatas.userInputSettingDatas == null)
            Debug.Log("Check");

        if (userInputAllDatas != null)
        {
            foreach (var item in userInputAllDatas.userInputSettingDatas)
                _userKeySettingDatas.Add(item.keyID, item.keyCode);
        }
    }

    private void LoadDefaultData()
    {
        Array array = Enum.GetValues(typeof(KeyID));
        foreach (KeyID id in array)
        {
            if (GetInputEnv(id) == InputEnv.Mouse)
            {
                var mouseIndex = GetDefaultMouseInputValue(id);
                _userKeySettingDatas.Add(id, mouseIndex);
            }
            else
            {
                var keyCode = GetDefaultKeyValue(id);
                _userKeySettingDatas.Add(id, (int)keyCode);
            }
        }
    }

    public void OnUpdate()
    {
        foreach ((KeyID keyID, int keyCode) in _userKeySettingDatas)
        {
            if (GetInputEnv(keyID) == InputEnv.Mouse)
            {
                if (CheckDownKey(keyID) == true)
                {
                    if (Input.GetMouseButtonDown(keyCode) == true)
                        _inputMouseCallback.OnNext((keyID, InputState.Down));
                }

                if (CheckPressedKey(keyID) == true)
                {
                    if (Input.GetMouseButton(keyCode) == true)
                        _inputMouseCallback.OnNext((keyID, InputState.Pressed));
                }

                if (CheckUpKey(keyID) == true)
                {
                    if (Input.GetMouseButtonUp(keyCode) == true)
                        _inputMouseCallback.OnNext((keyID, InputState.Up));
                }
            }
            
            if (GetInputEnv(keyID) == InputEnv.Keyboard)
            {
                if (CheckDownKey(keyID) == true)
                {
                    if (Input.GetKeyDown((KeyCode) keyCode) == true)
                        _inputKeyCallback.OnNext((keyID, InputState.Down));
                }

                if (CheckPressedKey(keyID) == true)
                {
                    if (Input.GetKey((KeyCode) keyCode) == true)
                        _inputKeyCallback.OnNext((keyID, InputState.Pressed));
                }

                if (CheckUpKey(keyID) == true)
                {
                    if (Input.GetKeyUp((KeyCode) keyCode) == true)
                        _inputKeyCallback.OnNext((keyID, InputState.Up));
                }
            }
        }
    }

    private bool CheckDownKey(KeyID id)
    {
        switch (id)
        {
            case KeyID.Dash:
            case KeyID.NormalAttack:
            case KeyID.NormalSkill_1:
            case KeyID.NormalSkill_2:
                return true;
        }

        return false;
    }

    private bool CheckPressedKey(KeyID id)
    {
        switch (id)
        {
            case KeyID.MoveForward:
            case KeyID.MoveBack:
            case KeyID.MoveRight:
            case KeyID.MoveLeft:
                return true;
        }

        return false;
    }

    private bool CheckUpKey(KeyID id)
    {
        switch (id)
        {
            case KeyID.MoveForward:
            case KeyID.MoveBack:
            case KeyID.MoveRight:
            case KeyID.MoveLeft:
                return true;
        }

        return false;
    }

    private InputEnv GetInputEnv(KeyID keyID)
    {
        switch (keyID)
        {
            case KeyID.NormalAttack:
            case KeyID.BreakSkill:
                return InputEnv.Mouse;
        }

        return InputEnv.Keyboard;
    }

    private KeyCode GetDefaultKeyValue(KeyID keyID)
    {
        switch (keyID)
        {
            case KeyID.MoveForward:
                return KeyCode.W;
            case KeyID.MoveBack:
                return KeyCode.S;
            case KeyID.MoveRight:
                return KeyCode.D;
            case KeyID.MoveLeft:
                return KeyCode.A;
            case KeyID.Dash:
                return KeyCode.Space;
            case KeyID.NormalSkill_1:
                return KeyCode.Q;
            case KeyID.NormalSkill_2:
                return KeyCode.E;
            case KeyID.UltSkill:
                return KeyCode.R;
        }

        return KeyCode.None;
    }

    private int GetDefaultMouseInputValue(KeyID keyID)
    {
        switch (keyID)
        {
            case KeyID.NormalAttack:
                return 0;
            case KeyID.BreakSkill:
                return 1;
        }

        return 0;
    }

    public void SaveData()
    {
        UserInputSettingAllData userInputSettingAllData = new UserInputSettingAllData();
        userInputSettingAllData.userInputSettingDatas = new List<UserInputSettingData>();
        if (_userKeySettingDatas == null || _userKeySettingDatas.Count == 0)
            Debug.Log("Check");

        foreach (var item in _userKeySettingDatas)
        {
            var saveData = new UserInputSettingData();
            saveData.keyID = item.Key;
            saveData.keyCode = item.Value;

            userInputSettingAllData.userInputSettingDatas.Add(saveData);
        }

        string json = JsonUtility.ToJson(userInputSettingAllData);
        File.WriteAllText(Path, json);
    }
}