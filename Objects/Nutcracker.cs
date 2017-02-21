using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Nutcrackers
{
  public class Nutcracker
  {
    private int _id;
    private string _name;

    public Nutcracker(string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
    }

    public override bool Equals(System.Object otherNutcracker)
    {
      if (!(otherNutcracker is Nutcracker))
      {
        return false;
      }
      else
      {
        Nutcracker newNutcracker = (Nutcracker) otherNutcracker;
        bool nameEquality = (this.GetName() == newNutcracker.GetName());
        return (nameEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }
    public void SetName(string Name)
    {
      _name = Name;
    }
    public string GetName()
    {
      return _name;
    }

    public static List<Nutcracker> GetAll()
    {
      List<Nutcracker> allNutcrackers = new List<Nutcracker>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM nutcrackers;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int nutcrackerId = rdr.GetInt32(0);
        string nutcrackerName = rdr.GetString(1);
        Nutcracker newNutcracker = new Nutcracker(nutcrackerName, nutcrackerId);
        allNutcrackers.Add(newNutcracker);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return allNutcrackers;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO nutcrackers (name) OUTPUT INSERTED.id VALUES (@NutcrackerName);", conn);

      SqlParameter descriptionParameter = new SqlParameter();
      descriptionParameter.ParameterName = "@NutcrackerName";
      descriptionParameter.Value = this.GetName();
      cmd.Parameters.Add(descriptionParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM nutcrackers;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }


  }
}
