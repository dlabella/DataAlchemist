namespace Data
{
    public abstract class BizObjectView : BizObject
    {
        public abstract string GetView(string condition = null);
    }
}
