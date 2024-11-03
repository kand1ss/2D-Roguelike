public interface ITickableEffect : IEffect
{
    void CheckPerSecond(float deltaTime);
}