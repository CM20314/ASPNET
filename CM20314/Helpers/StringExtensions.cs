namespace CM20314.Helpers
{
    public static class StringExtensions
    {
        public static string FormatCoordinateLine(this string input)
        {
            while(input.Contains("= "))
            {
                input = StripAfterEqualSign(input);
            }
            return input;
        }

        private static string StripAfterEqualSign(string input)
        {
            return input.Replace("= ", "=");
        }
    }
}
