namespace AVINSoR_Library.PatternClassification.Inputs.Value
{
    public class PercentValue : GenericValue
    {
        public PercentValue()
        {
            Units = "Percent";
            MaximumAllowableValue = 100;
            MinimumAllowableValue = 0;
        }
    }
}
