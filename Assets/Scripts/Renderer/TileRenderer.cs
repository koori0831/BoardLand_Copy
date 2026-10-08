using UnityEngine;

public class TileRenderer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer typeIcon;
    [SerializeField] private Renderer renderer;
    [SerializeField] private Sprite attackIcon, deffendIcon, healIcon;

    public void TileSetting(ETileType tileType, EElementType elementType)
    {
        // elementType에 따라서 머테리얼이 바뀌고
        // tileType에 따라서 icon이 변해야 함.
        switch (tileType)
        {
            case ETileType.Attack:
                typeIcon.sprite = attackIcon;
                break;
            case ETileType.Deffend:
                typeIcon.sprite = deffendIcon;
                break;
            case ETileType.Heal:
                typeIcon.sprite = healIcon;
                break;
        }
    }
}