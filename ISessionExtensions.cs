namespace NHibernate.Dapper
{

	public static class ISessionExtensions
  {
		public static dynamic Query(this ISession session, string sql, object param = null)
		{
			var transaction = GetTransaction(session);
			return session.Connection.Query(sql, param, transaction);
		}

		public static dynamic Execute(this ISession session, string sql, object param = null)
		{
			var transaction = GetTransaction(session);
			return session.Connection.Execute(sql, param, transaction);
		}

		public static IEnumerable<T> Query<T>(this ISession session, string sql, object param = null)
		{
			var transaction = GetTransaction(session);
			return session.Connection.Query<T>(sql, param, transaction);
		}
        
    
    //http://ayende.com/blog/1583/i-hate-this-code
    private static IDbTransaction GetTransaction(ISession session)
    {
        using (var command = session.Connection.CreateCommand()) {
            session.Transaction.Enlist(command);
            return command.Transaction;
          }
		}
	}
}
