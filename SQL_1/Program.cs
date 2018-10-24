using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SQL_1
{
	class Program
	{
		static void Main()
		{
			SQLSender();
		}

		static void SQLSender()
		{
			string sql;//Переменная для отправки и принятия команд
			MySqlCommand command;//Переменная отправка запроса
			MySqlDataReader reader;//Переменная принятие ответа
			sql = "server=localhost;user=root;database=database;password=0000";//Вход
			MySqlConnection conn = new MySqlConnection(sql);//Вход
			conn.Open();//Вход

			sql = "SELECT * FROM orders order by customer_id";
			command = new MySqlCommand(sql, conn);//Отправка запроса
			reader = command.ExecuteReader();//Принятие ответа
			Console.WriteLine("ORDERS TABLE");
			Console.WriteLine("ID		COSTUMER_ID	AMOUNT");
			while (reader.Read())
			{
				for (int i = 0; i < reader.FieldCount; i++)
				{
					Console.Write(reader[i]);
					Console.Write("		");
				}
				Console.WriteLine();
			}
			reader.Close();
			Console.WriteLine();
			Console.WriteLine();

			sql = "SELECT orders.customer_id, sum(orders.amount), customer.name FROM orders JOIN customer ON orders.customer_id = customer.id WHERE customer_id > 0 GROUP BY customer_id order by orders.customer_id";
			command = new MySqlCommand(sql, conn);//Отправка запроса
			reader = command.ExecuteReader();//Принятие ответа
			Console.WriteLine("SUM OF ORDERS BY CUSTOMER");
			Console.WriteLine("COSTUMER_ID	SUM		NAME");
			while (reader.Read())
			{
				for (int i = 0; i < reader.FieldCount; i++)
				{
					Console.Write(reader[i]);
					Console.Write("		");
				}
				Console.WriteLine();
			}
			reader.Close();
			Console.WriteLine();
			Console.WriteLine();

			sql = "SELECT customer.id, customer.name FROM customer WHERE customer.id NOT IN (SELECT orders.customer_id FROM orders)";
			command = new MySqlCommand(sql, conn);//Отправка запроса
			reader = command.ExecuteReader();//Принятие ответа
			Console.WriteLine("NO ORDERS");
			Console.WriteLine("COSTUMER_ID	NAME");
			while (reader.Read())
			{
				for (int i = 0; i < reader.FieldCount; i++)
				{
					Console.Write(reader[i]);
					Console.Write("		");
				}
				Console.WriteLine();
			}
			reader.Close();

			conn.Close();
			Console.ReadLine();

		}
	}
}
