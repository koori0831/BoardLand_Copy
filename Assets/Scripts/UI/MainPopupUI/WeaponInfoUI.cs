using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;

    private Board board;
    private Player player;

    public bool HasWeapon => player != null && player.Weapon != null;

    public void Bind(Board board)
    {
        if (this.board != null)
            this.board.CurrentGameChanged -= OnGameChanged;

        this.board = board;
        board.CurrentGameChanged += OnGameChanged;
    }

    private void OnGameChanged(int currentGameIndex)
    {
        if (player != null)
            player.WeaponChanged -= OnWeaponChanged;

        player = board.Games[currentGameIndex].Player;
        OnWeaponChanged(player.Weapon);

        player.WeaponChanged += OnWeaponChanged;
    }

    private void OnWeaponChanged(Weapon weapon)
    {
        if (weapon == null)
        {
            icon.enabled = false;
            nameText.text = "Empty";
            descriptionText.text = string.Empty;
            return;
        }

        icon.enabled = true;
        icon.sprite = weapon.Data.WeaponIcon;
        nameText.text = weapon.Data.WeaponName;
        descriptionText.text = weapon.Data.WeaponDescription;
    }

    private void OnDestroy()
    {
        if (board != null)
            board.CurrentGameChanged -= OnGameChanged;
        if (player != null)
            player.WeaponChanged -= OnWeaponChanged;
    }
}
