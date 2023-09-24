namespace Infra.Utils;

public static class PageSizeUtil
{
    public static int DividirIntUpValue(this int value, int divisor)
    {
        return (int) (double.Parse(value.ToString()) / divisor).RoundUpValue(0);
    }
    
    public static double RoundUpValue(this double value, int decimalpoint)
    {
        var result = Math.Round(value, decimalpoint);
        if (result < value)
        {
            result += Math.Pow(10, -decimalpoint);
        }

        return result;
    }
}