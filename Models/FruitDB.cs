using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Dapper;
using System.ComponentModel.DataAnnotations;

namespace FruitMarketData.Models
{
    public class FruitDB
    {
        private string connectionString = ("server=KHALIFABUILD202; database=personal; user id=raju; password=raju123");
        public List<Fruit> allFruits()
        {
            List<Fruit> fruits = new List<Fruit>();
            string sql = "select * from Fruit";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                fruits = conn.Query<Fruit>(sql).ToList();
            }
            return fruits;
        }

        public Fruit getFruitById(int id)
        {
            Fruit fruit = new Fruit();
            string sql = "select * from Fruit where Id = " + id;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                fruit = conn.QueryFirst<Fruit>(sql, new { id });
            }
            return fruit;
        }

        public void saveEditFruit(Fruit fruit)
        {
            string sql = "update Fruit set FruitName = '" + fruit.FruitName + "', " +
                "FruitImage = '" + fruit.FruitImage + "' where Id = " + fruit.Id;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Execute(sql, fruit);
            }
        }

        public void removeFruit(int id)
        {
            Fruit fruit = new Fruit();
            string sql = "delete from Fruit where Id = " + id;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Execute(sql, new { id });
            }
        }

        public void newFruit(Fruit fruit)
        {
            string sql = "insert into Fruit (FruitName, FruitImage) " +
                "values ('" + fruit.FruitName + "', '" + fruit.FruitImage + "')";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Execute(sql, fruit);
            }
        }
    }
}