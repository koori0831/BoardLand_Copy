using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventoryUI : MonoBehaviour
{
    [SerializeField] private List<Button> slotButtons;
    [SerializeField] private List<Image> slotIcons;

    private Board board;
    private Player player;

    private void Awake()
    {
        for (int i = 0; i < slotButtons.Count; i++)
        {
            int index = i;
            slotButtons[i].onClick.AddListener(() => ClickItem(index));
        }
    }

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
            player.ItemInventoryChanged -= OnItemInventoryChanged;

        player = board.Games[currentGameIndex].Player;
        OnItemInventoryChanged(player.ItemInventory);

        player.ItemInventoryChanged += OnItemInventoryChanged;
    }

    private void OnItemInventoryChanged(List<Item> items)
    {
        for (int i = 0; i < slotIcons.Count; i++)
        {
            bool hasItem = i < items.Count;
            slotIcons[i].enabled = hasItem;
            slotButtons[i].interactable = hasItem;
            if (hasItem)
                slotIcons[i].sprite = items[i].Data.ItemIcon;
        }
    }

    private void ClickItem(int itemIndex)
    {
        board.Games[board.CurrentGameIndex].UseItem(itemIndex);
    }

    private void OnDestroy()
    {
        if (board != null)
            board.CurrentGameChanged -= OnGameChanged;
        if (player != null)
            player.ItemInventoryChanged -= OnItemInventoryChanged;
    }
}
