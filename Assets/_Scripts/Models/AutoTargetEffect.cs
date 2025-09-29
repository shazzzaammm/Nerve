using SerializeReferenceEditor;
using UnityEngine;

[System.Serializable]
public class AutoTargetEffect
{
    [field: SerializeReference, SR] public TargetMode targetMode { get; private set; }
    [field: SerializeReference, SR] public Effect effect { get; private set; }

}
