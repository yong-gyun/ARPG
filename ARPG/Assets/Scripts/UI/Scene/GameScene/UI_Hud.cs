using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hud : MonoBehaviour
{
    [SerializeField] private Image _hpBar;
    [SerializeField] private Image _mpBar;

    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private TMP_Text _mpText;

    private Creature _owner;

    public void Init(Creature owner)
    {
        _owner = owner;
    }

    private void Update()
    {
        if (_owner == null)
            return;

        _hpBar.fillAmount = Mathf.Lerp(_hpBar.fillAmount, _owner.Hp, 20f * Time.deltaTime);
        _mpBar.fillAmount = Mathf.Lerp(_mpBar.fillAmount, _owner.Mp, 20f * Time.deltaTime);

        _hpText.text = $"{_owner.Hp}/{_owner.MaxHp}";
        _mpText.text = $"{_owner.Mp}/{_owner.MaxMp}";
    }
}
