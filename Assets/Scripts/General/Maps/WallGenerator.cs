using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator 
{
    public static void CreateWalls(HashSet<Vector2Int> floorPos, TileMapVisualizer tileMapVisualizer) { 
    
        var basicWallPositions = FindWallsInDirection(floorPos, Direction2D.cardinalDirectionList);
        foreach (var wall in basicWallPositions) 
        {
            tileMapVisualizer.PaintSingleBasicWall(wall);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirection(HashSet<Vector2Int> floorPos, List<Vector2Int> directionsList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in  floorPos) 
        { 
            foreach (var direction in directionsList) 
            { 
                var neighbourPos = position + direction;
                if (floorPos.Contains(neighbourPos) == false) 
                { 
                wallPositions.Add(neighbourPos);
                }
            }
        }
        return wallPositions;
    }
}
