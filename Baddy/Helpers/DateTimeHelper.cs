namespace Baddy.Helpers
{
    public class DateTimeHelper
    {
        public static double MinutesRounder(int minutes)
        {
            if (minutes < 15)
                return 0;

            if (minutes >= 15 && minutes <= 44)
                return 0.5;

            if (minutes > 44)
                return 1;

            return 0;
        }
    }
}
