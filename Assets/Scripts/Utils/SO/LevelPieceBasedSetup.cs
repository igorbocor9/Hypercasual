using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu]

public class LevelPieceBasedSetup : ScriptableObject
{
    public ArtManager.ArtType artType;

    [Header("Pieces")]
    public List<LevelPieceBase> levelPiecesStart;
    public List<LevelPieceBase> levelPieces;
    public List<LevelPieceBase> levelPiecesEnd;

    public int piecesNumberStart = 3;
    public int piecesNumber = 5;
    public int piecesNumberEnd = 1;
}
