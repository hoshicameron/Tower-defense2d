using DefenseNetwork.Core.EventChannels.DataObjects.Enums;

public interface ITowerData
{
    string Name { get; }
    int DeployCost { get; }
    int UpgradeCost { get; }
    TowerType Type { get; }
    string Description { get; }
    int SellIncome { get; }
}