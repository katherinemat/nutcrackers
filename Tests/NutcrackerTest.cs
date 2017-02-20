using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Nutcrackers
{
  public class NutcrackerTest : IDisposable
  {
    public NutcrackerTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=collection_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Nutcracker.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfSameName()
    {
      //Arrange, Act
      Nutcracker nutcracker1 = new Nutcracker("Phil");
      Nutcracker nutcracker2 = new Nutcracker("Phil");

      //Assert
      Assert.Equal(nutcracker1, nutcracker2);
    }

    public void Dispose()
    {
      Nutcracker.DeleteAll();
    }
  }
}
