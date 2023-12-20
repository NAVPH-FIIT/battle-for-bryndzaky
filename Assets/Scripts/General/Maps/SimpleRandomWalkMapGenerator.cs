using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SimpleRandomWalkMapGenerator : AbstractDungeonGen
{

    [SerializeField] private SimpleRandomWalkData randomWalkParam;
    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk();

        tileMapVisualizer.Clear();
        tileMapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tileMapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPos = startPos;
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();

        for (int i = 0; i < randomWalkParam.interations; i++)
        {
            var path = Generation_Algorythm.SimpleRandomWalk(currentPos, randomWalkParam.walkLen);
            
            floorPos.UnionWith(path);
            if (randomWalkParam.startRandomlyEachIter)
                currentPos = floorPos.ElementAt(UnityEngine.Random.Range(0, floorPos.Count));
        }
        return floorPos;
    }

    //---------------
    protected override void ClearMapTiles() {
        tileMapVisualizer.Clear();
    }
}
