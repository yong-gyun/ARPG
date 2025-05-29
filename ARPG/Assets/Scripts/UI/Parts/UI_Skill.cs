using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Skill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UI_SkillDetail _detailUI;
    [SerializeField] private Image _skillIcon;
    [SerializeField] private Image _cooldownMask;

    public async void Init(int skillID)
    {
        if (_skillIcon != null)
            _skillIcon.sprite = await Managers.Resource.LoadSpriteAsync("Icon/Skill", $"Skill_{skillID}");

        if (_detailUI != null)
            _detailUI.Init(skillID);
    }

    public void SetCooldown(float time)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_detailUI != null)
            _detailUI.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_detailUI != null)
            _detailUI.gameObject.SetActive(false);
    }
}
