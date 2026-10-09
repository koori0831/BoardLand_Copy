using Core;
using UnityEngine;

public class TileRenderer : MonoBehaviour
{
    [SerializeField] private SerializableDictionary<ETileType, Sprite> tileIconDictionary;
    [SerializeField] private SerializableDictionary<EElementType, Material> materialDictionary;
    [SerializeField] private SpriteRenderer typeIcon;
    [SerializeField] private Renderer tileRenderer;

    public void Bind(Tile tile)
    {
        TileSetting(tile.TileType, tile.ElementType);
    }

    private void TileSetting(ETileType tileType, EElementType elementType)
    {
        if (tileIconDictionary.TryGetValue(tileType, out Sprite icon))
            typeIcon.sprite = icon;
        else
            LogManager.Log($"{tileType}에 해당하는 타일 아이콘이 없음.", ELogType.Test, ELogCategory.GameSystem, ELogLevel.Error);

        if (materialDictionary.TryGetValue(elementType, out Material material) && material != null)
            tileRenderer.material = material;
    }
}
