using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Collection
{
  public class Collection : IDisposable
  {
    public Collection()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=collection_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Collection.GetAll().Count;
      Assert.Equal(0, result);
    }

    public void Dispose()
    {
      Collection.DeleteAll();
    }
  }
}
