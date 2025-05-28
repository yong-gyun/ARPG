using Data.Contents;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillDetail : MonoBehaviour
{
    [SerializeField] private Image _skillIcon;
    [SerializeField] private TMP_Text _descText;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _cooldownText;
    private int _skillID;

    public async void Init(int skillID)
    {
        _skillID = skillID;
        SkillInfoScript script = Managers.Data.GetSkillInfoScripts.Find(x => x.SkillID == _skillID);
        
        if (_skillIcon != null)
            _skillIcon.sprite = await Managers.Resource.LoadSpriteAsync("Icon/Skill", $"Skill_{_skillID}");
        
        if (_nameText != null)
            _nameText.text = script.SkillName;
        
        if (_descText != null)
            _descText.text = script.SkillDesc;
    }
}
