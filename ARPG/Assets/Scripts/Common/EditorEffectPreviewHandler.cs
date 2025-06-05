using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EditorEffectPreviewHandler : MonoBehaviour
{
    public Effect Effect { get; private set; }
    SkillSettingData _clone;

    public Effect Init(SkillSettingData settingData)
    {
        _clone = ScriptableObject.Instantiate(settingData);
        _clone.SetOwner(gameObject);
        GameObject go = _clone.CreateSkillToEditor(gameObject);
        Effect = go.GetComponent<Effect>();
        Effect.Init(gameObject);
        return Effect;
    }

    public void SkillAction(int command)
    {
        Effect.PlayAction(command);
    }
}