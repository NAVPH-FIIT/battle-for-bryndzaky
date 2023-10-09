// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Tilemaps;

// public class TileMapVisualizer : MonoBehaviour
// {
//     [SerializeField]
//     private Tilemap floorTilemap, wallTilemap;

//     [SerializeField]
//     private TileBase floorTile1, floorTile2, floorTile3, floorTile4, wallTop;

//     public void PaintFloorTiles(IEnumerable<Vector2Int> floorPoss)
//     {
        
//         PaintTiles(floorPoss, floorTilemap);
//     }

//     private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap)
//     {
//         TileBase tile = null;
  
//         foreach (var position in positions) 
//         {
//             int num = UnityEngine.Random.Range(0, 4);
//             if (num == 0)
//                 tile = floorTile1;
//             if (num == 1)
//                 tile = floorTile2;
//             if (num == 2)
//                 tile = floorTile3;
//             if (num == 3)
//                 tile = floorTile4;
//             PaintSingleTile(tilemap, tile, position);
//         }
//     }

//     private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
//     {
//         var tilePos = tilemap.WorldToCell((Vector3Int)position);
//         tilemap.SetTile(tilePos, tile);
//     }

//     public void Clear()
//     {
//         floorTilemap.ClearAllTiles();
//         wallTilemap.ClearAllTiles();
//     }

//     internal void PaintSingleBasicWall(Vector2Int wall)
//     {
//         PaintSingleTile(wallTilemap, wallTop, wall);
//     }
// }
 