using UserInterfaceScripts;

public interface ISummoner : IHudManager
{
    string Name { get; set; }
    void AddCreature(CreatureData creature);
    int GetPocketSize();
    bool HasRemainingCreatures();
    bool CanSummonCreature(int slot);
    CreatureData SummonCreature(int slot);
    CreatureData GetActiveCreature();
    int GetNextHealthyCreature();
    
    
}

public interface IHudManager
{
    void SetHUD(PlayerHUD HUD);
    void SetPocketHUD(PocketHUD HUD);
    void SetAttackHuds(AttackWindowHud ctrl, AttackWindowHud shift);
    void UpdateHUD(CreatureData creature);
}
