using System.Numerics;

namespace SampleCode.Database
{
    public class AccountSelect :SqlCommandBase
    {
        public AccountSelect() : base()
        {
            _command.CommandText = "SelectAccount";

            _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AUID", System.Data.SqlDbType.BigInt) { Direction = System.Data.ParameterDirection.Input });
        }

        public void SetParam(long auid)
        {
            _command.Parameters[0].Value = auid;
        }
    }
}
