using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankingSystem.API.Models
{
    public class Accounts
    {
        [Key]
        public Guid AccountId { get; set; } = Guid.NewGuid();
        [Required]
        public Guid UserId { get; set; }

        // Navigation property
        [ForeignKey("UserId")]
        public Users User { get; set; }

        [Required]
        public long AccountNumber { get; set; }

        public decimal Balance { get; set; }
        public long AtmCardNum { get; set; }

        [Required]

        public int AtmCardPin { get; set; }

        public DateTime AccountCreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid AccountCreatedBy { get; set; }

        // Navigation property
        [ForeignKey("AccountCreatedBy")]
        public Users CreatedByUser { get; set; }

        public DateTime AccountModifiedAt { get; set; } = DateTime.UtcNow;

        public Guid AccountModifiedBy { get; set; }

        // Navigation property
        [ForeignKey("AccountModifiedBy")]
        public Users ModifiedByUser { get; set; }


        public Accounts(Guid accountId, Guid userId, decimal balance, int atmCardPin, DateTime accountCreatedAt, Guid accountCreatedBy, DateTime accountModifiedAt, Guid accountModifiedBy)
        {
            AccountId = accountId;
            UserId = userId;
            Balance = balance;
            AtmCardPin = atmCardPin;
            AccountCreatedAt = accountCreatedAt;
            AccountCreatedBy = accountCreatedBy;
            AccountModifiedAt = accountModifiedAt;
            AccountModifiedBy = accountModifiedBy;

        }

        public Accounts()
        {
            AccountNumber = GenerateRandomAccountNumber(1);
            AtmCardNum = GenerateRandomAccountNumber(2);

        }

        private static Random ran = new Random();

        private long GenerateRandomAccountNumber(int num)
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

//private long GenerateRandomAccountNumber()
//{
//    var builder = new StringBuilder("1000001");

//    while (builder.Length < 16)
//    {
//        builder.Append(ran.Next(10).ToString());
//    }

//    long newAccountNumber = long.Parse(builder.ToString());

//    // Check if the new account number already exists in the database
//    while (AccountNumberExists(newAccountNumber))
//    {
//        builder.Clear();
//        builder.Append("1000001");

//        while (builder.Length < 16)
//        {
//            builder.Append(ran.Next(10).ToString());
//        }

//        newAccountNumber = long.Parse(builder.ToString());
//    }

//    return newAccountNumber;
//}

//private bool AccountNumberExists(long accountNumber)
//{
//    // Check if the account number exists in the database
//    // You will need to implement this method based on your database logic
//    // For example, using Entity Framework:
//    using (var context = new YourDbContext())
//    {
//        return context.Accounts.Any(a => a.AccountNumber == accountNumber);
//    }
//}
