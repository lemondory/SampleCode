using System.Data.SqlClient;

namespace SampleCode.Database
{
    public class SqlCommandBase
    {
        public SqlCommand _command = null;

        public SqlCommandBase()
        {
            _command = new SqlCommand();
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandTimeout = 10;
        }

        public bool Connection(SqlConnection connection)
        {
            if (connection == null) { return false; }

            _command.Connection = connection;

            if (System.Data.ConnectionState.Closed == _command.Connection.State)
            {
                _command.Connection.Open();
            }
            else if (System.Data.ConnectionState.Broken == _command.Connection.State)
            {
                _command.Connection.Close();
                _command.Connection.Open();
            }

            return true;
        }

        public void CloseConnection()
        {
            _command.Connection.Close();
        }

        public int ExecuteNonQuery()
        {
            try
            {
                int result = _command.ExecuteNonQuery();

                ResetParameter();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return -1;
        }

        public async Task<int> ExecuteNonQueryAsync()
        {
            try
            {
                int result = await _command.ExecuteNonQueryAsync().ConfigureAwait(false);

                ResetParameter();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return -1;
        }

        public SqlDataReader ExecuteReader()
        {
            try
            {
                var reader = _command.ExecuteReader();

                ResetParameter();

                return reader;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return null;
        }

        public async Task<SqlDataReader> ExecuteReaderAsync()
        {
            try
            {
                var reader = await _command.ExecuteReaderAsync().ConfigureAwait(false);

                ResetParameter();

                return reader;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return null;
        }

        private void ResetParameter()
        {
            for (int i = 0; i < _command.Parameters.Count; i++)
            {
                _command.Parameters[i].Value = null;
            }
        }
    }
}
