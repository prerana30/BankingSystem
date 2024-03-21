namespace BankingSystem.API.Utils
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

                default:

                    return 0;

            }
        }
    }
}
