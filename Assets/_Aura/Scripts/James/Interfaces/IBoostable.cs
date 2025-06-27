namespace Aura.Core.Interfaces
{
    public interface IBoostable
    {
        bool IsBoosting { get; }
        void ToggleBoost(bool enable);
    }
}