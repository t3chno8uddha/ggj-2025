public static class NumberFormatter
{
    public static string Format(int number, string format = "#.##")
    {
        decimal prefix;
        char suffix;

        if (number >= 1_000_000_000)
        {
            prefix = (decimal)number / 1_000_000_000;
            suffix = 'B';
        }
        else if (number >= 1_000_000)
        {
            prefix = (decimal)number / 1_000_000;
            suffix = 'M';
        }
        else if (number >= 1_000)
        {
            prefix = (decimal)number / 1_000;
            suffix = 'K';
        }
        else
        {
            return number.ToString();
        }

        return $"{prefix.ToString(format)}{suffix}";
    }

}
