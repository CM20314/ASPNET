namespace CM20314.Helpers
{
    /// <summary>
    /// Responsible for removing whitespace on coordinate lines in input files
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Removes all whitespace following an equals (=) sign
        /// </summary>
        /// <param name="input">Coordinate line to process</param>
        /// <returns>Formatted coordinate line</returns>
        public static string FormatCoordinateLine(this string input)
        {
            while(input.Contains("= "))
            {
                input = StripAfterEqualSign(input);
            }
            return input;
        }

        /// <summary>
        /// Removes the first space after an equals (=) sign
        /// </summary>
        /// <param name="input">String to process</param>
        /// <returns>Processed string</returns>
        private static string StripAfterEqualSign(string input)
        {
            return input.Replace("= ", "=");
        }
    }
}
