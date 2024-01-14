namespace FluffyLibrary.PageManager.Model
{
    public class LayerModel
    {
        public int HolderIndex;

        public LayerModel SetHolderIndex(int holderIndex)
        {
            HolderIndex = holderIndex;
            return this;
        }
    }
}