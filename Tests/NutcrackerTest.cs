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

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Nutcracker testNutcracker = new Nutcracker("Phil");

      //Act
      testNutcracker.Save();
      List<Nutcracker> result = Nutcracker.GetAll();
      List<Nutcracker> testList = new List<Nutcracker>{testNutcracker};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      //Arrange
      Nutcracker testNutcracker = new Nutcracker("Phil");

      //Act
      testNutcracker.Save();
      Nutcracker savedNutcracker = Nutcracker.GetAll()[0];

      int result = savedNutcracker.GetId();
      int testId = testNutcracker.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsNutcrackerInDatabase()
    {
      //Arrange
      Nutcracker testNutcracker = new Nutcracker("Phil");
      testNutcracker.Save();

      //Act
      Nutcracker foundNutcracker = Nutcracker.Find(testNutcracker.GetId());

      //Assert
      Assert.Equal(testNutcracker, foundNutcracker);
    }

    public void Dispose()
    {
      Nutcracker.DeleteAll();
    }
  }
}
