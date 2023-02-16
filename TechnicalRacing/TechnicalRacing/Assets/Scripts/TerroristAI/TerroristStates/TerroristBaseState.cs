using UnityEngine;

public abstract class TerroristBaseState
{
    public abstract void EnterState(TerroristAImenager terroMenager);
    public abstract void UpdateState(TerroristAImenager terroMenager);
}
