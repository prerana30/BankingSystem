using System.Text;

namespace BankingSystem.API.Utilities
{
    public static class RandomNumberGeneratorHelper
    {
        private static Random ran = new Random();

        public static long GenerateRandomNumber(int num)
        {
            switch (num)
            {
                case 1:
                    var accountNumber = new StringBuilder("100001");
                    while (accountNumber.Length < 16)
                    {
                        accountNumber.Append(ran.Next(10).ToString());
                    }
                    return long.Parse(accountNumber.ToString());

                case 2:

                    var atmCardNum = new StringBuilder("900009");
                    while (atmCardNum.Length < 16)
                    {
                        atmCardNum.Append(ran.Next(10).ToString());
                    }
                    return long.Parse(atmCardNum.ToString());

                case 3:

                    var atmCardPin = new StringBuilder();
                    while (atmCardPin.Length < 4)
                    {
                        atmCardPin.Append(ran.Next(4).ToString());
                    }

                    return long.Parse(atmCardPin.ToString());

                default:
                    return 0;
            }
        }
    }
}
