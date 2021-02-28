using UnlimitedBombs.StatSystem;

/// <summary>
/// The IStatModApplicator interface exists as an easy way to give any class the ability to apply and remove StatModifiers to
/// a Stat. It isn't necessary to add / remove modifiers, but can keep the code more clean.
/// </summary>
public interface IStatModApplicator
{
    void ApplyModifier();

    void RemoveModifier(StatModifier mod);
    
    void RemoveAllModifiers();
}