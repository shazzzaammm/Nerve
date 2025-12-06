using System.Collections.Generic;
using UnityEngine;
public enum TileType
{
    FLOOR,
    WALL,
    EXIT,
    PLAYER_SPAWN,
    CHEST_SPAWN,
    BOSS_SPAWN,
    ENEMY_SPAWN,
    NONE
}
public class LevelGenerationSystem : Singleton<LevelGenerationSystem>
{
    public Dictionary<Vector2Int, TileType> Generate(Texture2D image)
    {
        int width = image.width;
        int height = image.height;

        Dictionary<Vector2Int, TileType> layout = new();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color pixel = image.GetPixel(x, y);
                string hexColor = ColorUtility.ToHtmlStringRGB(pixel).ToUpper();
                Vector2Int tilePosition = new(x, y);
                var tileType = hexColor switch
                {
                    "000000" => TileType.FLOOR,
                    "FFFFFF" => TileType.WALL,
                    "FF0000" => TileType.EXIT,
                    "ED1C24" => TileType.EXIT,
                    "00FF00" => TileType.PLAYER_SPAWN,
                    "22B14C" => TileType.PLAYER_SPAWN,
                    "FF7E00" => TileType.ENEMY_SPAWN,
                    "FFF200" => TileType.CHEST_SPAWN,
                    "4D6DF3" => TileType.BOSS_SPAWN,
                    _ => TileType.NONE,
                };
                if (tileType == TileType.NONE)
                {
                    Debug.Log("Color \'" + hexColor + "\' not recognized");
                }
                if (pixel.a != 0)
                    layout.Add(tilePosition, tileType);
            }
        }
        return layout;
    }
}