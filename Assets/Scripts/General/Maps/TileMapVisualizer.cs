using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualizer : MonoBehaviour
{
  [SerializeField]
  private Tilemap floorTilemap, wallTilemap;

  [SerializeField]
  private TileBase floorTile, wallTop;

  public void PaintFloorTiles(IEnumerable<Vector2Int> floorPoss)
  {
    PaintTiles(floorPoss, floorTilemap, floorTile);
  }

  private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
  {
    foreach (var position in positions)
    {
      int num = UnityEngine.Random.Range(0, 10);
      if (num < 2)
      {
        num = UnityEngine.Random.Range(0, 3);
        if (num == 0)
          tile = floorTile2;
        if (num == 1)
          tile = floorTile3;
        if (num == 2)
          tile = floorTile4;
      }
      else
      {
        tile = floorTile1;
      }
      PaintSingleTile(tilemap, tile, position);
    }
  }

  private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
  {
    var tilePos = tilemap.WorldToCell((Vector3Int)position);
    tilemap.SetTile(tilePos, tile);
  }

  public void Clear()
  {
    floorTilemap.ClearAllTiles();
    wallTilemap.ClearAllTiles();
  }

  internal void PaintSingleBasicWall(Vector2Int wall)
  {
    PaintSingleTile(floorTilemap, floorTile2, wall);
    //PaintSingleTile(wallTilemap, wallTop, wall);
  }
}
