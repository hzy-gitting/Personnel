using Personnel.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Personnel;
using personnel.Model;

namespace personnel.DAL
{
	/// <summary>
	/// 离职表DAL
	/// 2020/7/9 17:57 
	/// 2020/7/9 23:19 修改了错误的sql语句，和错误的参数类型
	/// hzy
	/// </summary>
	public class QuitDAL
	{
		SqlDbHelper db = new SqlDbHelper();

		/// <summary>
		/// 添加离职信息
		/// </summary>
		/// <param name="quit_date"></param>
		/// <param name="quit_reason"></param>
		/// <returns>受影响记录条数</returns>
		public int AddQuit(DateTime quit_date, string quit_reason)
		{
			string sql = "insert into quit (quit_date,quit_reason) " +
				"values (@quit_date,@quit_reason)";
			MySqlParameter[] param = {
				new MySqlParameter("@quit_date",MySqlDbType.DateTime),
				new MySqlParameter("@quit_reason",MySqlDbType.VarChar)
			};
			param[0].Value = quit_date;
			param[1].Value = quit_reason;
			return db.ExecuteNonQuery(sql, param);
		}
		/// <summary>
		/// 删除离职信息，根据id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>受影响记录条数</returns>
		public int DeleteQuitById(int id)
		{
			string sql = "delete from quit where id = @id";
			MySqlParameter[] param = {
				new MySqlParameter("@id",MySqlDbType.Int32),
			};
			param[0].Value = id;
			return db.ExecuteNonQuery(sql, param);
		}
		/// <summary>
		/// 更改离职信息，根据id
		/// </summary>
		/// <param name="id"></param>
		/// <param name="quit_date"></param>
		/// <param name="quit_reason"></param>
		/// <returns>受影响记录条数</returns>
		public int UpdateQuitById(int id, DateTime quit_date, string quit_reason)
		{
			string sql = "update quit set quit_date=@quit_date,quit_reason=@quit_reason" +
				"  where id=@id";
			MySqlParameter[] param = {
				new MySqlParameter("@id",MySqlDbType.Int32),
				new MySqlParameter("@quit_date",MySqlDbType.DateTime),
				new MySqlParameter("@quit_reason",MySqlDbType.VarChar)
			};
			param[0].Value = id;
			param[1].Value = quit_date;
			param[2].Value = quit_reason;
			return db.ExecuteNonQuery(sql, param);
		}

		/// <summary>
		/// 查询所有离职信息
		/// </summary>
		/// <returns>离职信息列表</returns>
		public List<Quit> GetAll()
		{
			string sql = "select * from quit";
			DataTable dt = db.ExecuteDataTable(sql);
			return DataTableToList(dt);
		}
		/// <summary>
		/// 查询离职信息，根据id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>离职信息</returns>
		public Quit GetQuitById(int id)
		{
			string sql = "select * from quit where id = @id";
			MySqlParameter[] param =
			{
				new MySqlParameter("@id",MySqlDbType.Int32)
			};
			param[0].Value = id;
			DataTable dt = db.ExecuteDataTable(sql, param);
			if (dt.Rows.Count > 0)
			{
				return DataRowToQuit(dt.Rows[0]);
			}
			return null;
		}
		/// <summary>
		/// 查询离职信息，根据离职时间
		/// </summary>
		/// <param name="quit_date"></param>
		/// <returns>离职信息列表</returns>
		public List<Quit> GetQuitByQuitDate(DateTime quit_date)
		{
			string sql = "select * from quit where quit_date = @quit_date";
			MySqlParameter[] param =
			{
				new MySqlParameter("@quit_date",MySqlDbType.DateTime)
			};
			param[0].Value = quit_date;
			DataTable dt = db.ExecuteDataTable(sql, param);
			return DataTableToList(dt);
		}
		/// <summary>
		/// 查询离职信息，根据离职原因
		/// </summary>
		/// <param name="quit_date"></param>
		/// <returns>离职信息列表</returns>
		public List<Quit> GetQuitByQuitReason(string quit_reason)
		{
			string sql = "select * from quit where quit_reason = @quit_reason";
			MySqlParameter[] param =
			{
				new MySqlParameter("@quit_reason",MySqlDbType.VarChar)
			};
			param[0].Value = quit_reason;
			DataTable dt = db.ExecuteDataTable(sql, param);
			return DataTableToList(dt);
		}

		private Quit DataRowToQuit(DataRow dr)
		{
			Quit quit = new Quit();

			quit.id = Convert.ToInt32(dr["id"]);
			quit.quit_date = Convert.ToDateTime(dr["quit_date"]);
			quit.quit_reason = Convert.ToString(dr["quit_reason"]);
			return quit;
		}
		private List<Quit> DataTableToList(DataTable dt)
		{
			if (dt.Rows.Count > 0)
			{
				List<Quit> quits = new List<Quit>();
				foreach (DataRow dr in dt.Rows)
				{
					quits.Add(DataRowToQuit(dr));
				}
				return quits;
			}
			return null;
		}
	}
}
