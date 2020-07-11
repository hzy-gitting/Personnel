using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using personnel.Model;
using Personnel;

namespace personnel.DAL
{
	/// <summary>
	/// 奖惩DAL
	/// 2020/7/9 18:39
	/// 2020/7/9 22:44 修改了几条错误的sql语句
	/// 2020/7/11 16：28 由于RP表结构的变动，修改和新增了一些代码
	/// 添加了新的方法GetRPsByUserId，GetRPsByRPTime，GetRPsByRPTimeBetween
	/// hzy
	/// </summary>
	public class RPDAL
	{
		SqlDbHelper db = new SqlDbHelper();

		/// <summary>
		/// 添加奖惩记录
		/// no主键字段数据库自动生成，无需手动指定
		/// </summary>
		/// <param name="rp_name"></param>
		/// <param name="sal"></param>
		/// <returns>受影响记录条数</returns>
		public int AddRP(string rp_name,int sal,int user_id,DateTime rp_time)
		{
			string sql = "insert into rp(rp_name,sal,user_id,rp_time) " +
				"values(@rp_name,@sal,@user_id,@rp_time);";
			MySqlParameter[] param =
			{
				new MySqlParameter("@rp_name",MySqlDbType.VarChar),
				new MySqlParameter("@sal",MySqlDbType.Int32),
				new MySqlParameter("@user_id",MySqlDbType.Int32),
				new MySqlParameter("@rp_time",MySqlDbType.DateTime),
			};
			param[0].Value = rp_name;
			param[1].Value = sal;
			param[2].Value = user_id;
			param[3].Value = rp_time;
			return db.ExecuteNonQuery(sql,param);
		}

		/// <summary>
		/// 删除奖惩记录，根据no
		/// </summary>
		/// <param name="rp_id"></param>
		/// <returns>受影响记录条数</returns>
		public int DeleteRPByNo(int no)
		{
			string sql = "delete from rp where no=@no";
			MySqlParameter[] param =
			{
				new MySqlParameter("@no",MySqlDbType.Int32),
			};
			param[0].Value = no;
			return db.ExecuteNonQuery(sql,param);
		}

		/// <summary>
		/// 更改奖惩记录，根据no
		/// </summary>
		/// <param name="rp_id"></param>
		/// <param name="rp_name"></param>
		/// <param name="sal"></param>
		/// <returns>受影响记录条数</returns>
		public int UpdateRPByNo(int no,string rp_name,int sal, int user_id, DateTime rp_time)
		{
			string sql = "update rp set rp_name=@rp_name,sal=@sal" +
				" ,user_id=@user_id,rp_time=@rp_time" +
				" where no=@no";
			MySqlParameter[] param =
			{
				new MySqlParameter("@rp_name",MySqlDbType.VarChar),
				new MySqlParameter("@sal",MySqlDbType.Int32),
				new MySqlParameter("@no",MySqlDbType.Int32),
				new MySqlParameter("@user_id",MySqlDbType.Int32),
				new MySqlParameter("@rp_time",MySqlDbType.DateTime),
			};
			param[0].Value = rp_name;
			param[1].Value = sal;
			param[2].Value = no;
			param[3].Value = user_id;
			param[4].Value = rp_time;
			return db.ExecuteNonQuery(sql,param);
		}
		/// <summary>
		/// 查询所有奖惩记录
		/// </summary>
		/// <returns>奖惩记录列表</returns>
		public List<RP> GetAll()
		{
			string sql = "select * from rp";
			return DataTableToList(db.ExecuteDataTable(sql));
		}
		/// <summary>
		/// 查询奖惩记录，根据no
		/// </summary>
		/// <param name="no"></param>
		/// <returns>奖惩记录</returns>
		public RP GetRPByNo(int no)
		{
			string sql = "select * from rp where no=@no";
			MySqlParameter[] param =
			{
				new MySqlParameter("@no",MySqlDbType.Int32),
			};
			param[0].Value = no;
			DataTable dt = db.ExecuteDataTable(sql, param);
			if (dt.Rows.Count > 0)
			{
				return DataRowToRP(dt.Rows[0]);
			}
			return null;
		}
		/// <summary>
		/// 查询奖惩记录，根据rp_name
		/// </summary>
		/// <param name="rp_name"></param>
		/// <returns>奖惩记录列表</returns>
		public List<RP> GetRPByRPName(string rp_name)
		{
			string sql = "select * from rp where rp_name=@rp_name";
			MySqlParameter[] param =
			{
				new MySqlParameter("@rp_name",MySqlDbType.VarChar),
			};
			param[0].Value = rp_name;
			return DataTableToList(db.ExecuteDataTable(sql, param));
		}

		/// <summary>
		/// 查找奖惩记录，根据user_id
		/// </summary>
		/// <param name="user_id"></param>
		/// <returns>返回符合条件的奖惩记录列表</returns>
		public List<RP> GetRPsByUserId(int user_id)
		{
			string sql = "select * from rp where user_id = @user_id";
			MySqlParameter[] param =
			{
				new MySqlParameter("@user_id",MySqlDbType.Int32),
			};
			param[0].Value = user_id;
			return DataTableToList(db.ExecuteDataTable(sql, param));
		}

		/// <summary>
		/// 查找奖惩记录，根据具体日期
		/// </summary>
		/// <param name="rp_time"></param>
		/// <returns>返回符合条件的奖惩记录列表</returns>
		public List<RP> GetRPsByRPTime(DateTime rp_time)
		{
			string sql= "select * from rp where rp_time = @rp_time";
			MySqlParameter[] param =
			{
				new MySqlParameter("@rp_time",MySqlDbType.DateTime),
			};
			param[0].Value = rp_time;
			return DataTableToList(db.ExecuteDataTable(sql, param));
		}

		/// <summary>
		/// 查找位于某一时间段[later,before]之内的奖惩记录
		/// </summary>
		/// <param name="later"></param>
		/// <param name="before"></param>
		/// <returns>返回符合条件的奖惩记录列表</returns>
		public List<RP> GetRPsByRPTimeBetween(DateTime later ,DateTime before)
		{
			string sql = "select * from rp where rp_time >= @later AND rp_time <= @before";
			MySqlParameter[] param =
			{
				new MySqlParameter("@later",MySqlDbType.DateTime),
				new MySqlParameter("@before",MySqlDbType.DateTime),
			};
			param[0].Value = later;
			param[1].Value = before;
			return DataTableToList(db.ExecuteDataTable(sql, param));
		}


		private RP DataRowToRP(DataRow dr)
		{
			int no = Convert.ToInt32(dr["no"]);
			string rp_name = Convert.ToString(dr["rp_name"]);
			int sal = Convert.ToInt32(dr["sal"]);
			int user_id = Convert.ToInt32(dr["user_id"]);
			DateTime rp_time = Convert.ToDateTime(dr["rp_time"]);
			return new RP(no, rp_name, sal, user_id, rp_time);
		}

		private List<RP> DataTableToList(DataTable dt)
		{
			if (dt.Rows.Count > 0)
			{
				List<RP> rps = new List<RP>();
				foreach (DataRow dr in dt.Rows)
				{
					rps.Add(DataRowToRP(dr));
				}
				return rps;
			}
			return null;
		}
	}
}
