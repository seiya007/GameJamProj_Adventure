using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PositionParamAsset")]
public class SpriteAnimationPosData : ScriptableObject
{
    public Vector3[] pos;

    public Vector3 GetPos(int _idx){ return pos[_idx]; }
}
