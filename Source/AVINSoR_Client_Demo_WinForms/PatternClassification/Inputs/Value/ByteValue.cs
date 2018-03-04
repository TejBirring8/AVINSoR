namespace AVINSoR_Library.PatternClassification.Inputs.Value
{
    public class ByteValue : GenericValue
    {
        public ByteValue()
        {
            Units = "Byte range (0-255) Intensity";
            MaximumAllowableValue = 255;
            MinimumAllowableValue = 0;
        }
    }
}
