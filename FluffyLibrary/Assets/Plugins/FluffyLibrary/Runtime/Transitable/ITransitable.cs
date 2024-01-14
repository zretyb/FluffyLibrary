using Cysharp.Threading.Tasks;

namespace FluffyLibrary.Transitable
{
    public interface ITransitable
    {
        void TransitInStarted();
        void TransitOutStarted();
        void TransitInEnded();
        void TransitOutEnded();

        UniTask TransitInAsync();
        UniTask TransitOutAsync();
    }
}