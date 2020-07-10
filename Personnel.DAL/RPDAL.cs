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
	/// hzy
	/// </summary>
	public class RPDAL
	{
		SqlDbHelper db = new SqlDbHelper();

		/// <summary>
		/// 添加奖惩信息
		/// </summary>
		/// <param name="rp_name"></param>
		/// <param name="sal"></param>
		/// <returns>受影响记录条数</returns>
		public int AddRP(string rp_name,int sal)
		{
			string sql = "insert into rp(rp_name,sal) values(@rp_name,@sal);";
			MySqlParameter[] param =
			{
				new MySqlParameter("@rp_name",MySqlDbType.VarChar),
				new MySqlParameter("@sal",MySqlDbType.Int32)
			};
			param[0].Value = rp_name;
			param[1].Value = sal;
			return db.ExecuteNonQuery(sql,param);
		}

		/// <summary>
		/// 删除奖惩记录，根据id
		/// </summary>
		/// <param name="rp_id"></param>
		/// <returns>受影响记录条数</returns>
		public int DeleteRPByRPId(int rp_id)
		{
			string sql = "delete from rp where rp_id=@rp_id";
			MySqlParameter[] param =
			{
				new MySqlParameter("@rp_id",MySqlDbType.Int32),
			};
			param[0].Value = rp_id;
			return db.ExecuteNonQuery(sql,param);
		}

		/// <summary>
		/// 更改奖惩记录，根据id
		/// </summary>
		/// <param name="rp_id"></param>
		/// <param name="rp_name"></param>
		/// <param name="sal"></param>
		/// <returns>受影响记录条数</returns>
		public int UpdateRPById(int rp_id,string rp_name,int sal)
		{
			string sql = "update rp set rp_name=@rp_name,sal=@sal where rp_id=@rp_id";
			MySqlParameter[] param =
			{
				new MySqlParameter("@rp_name",MySqlDbType.VarChar),
				new MySqlParameter("@sal",MySqlDbType.Int32),
				new MySqlParameter("@rp_id",MySqlDbType.Int32),
			};
			param[0].Value = rp_name;
			param[1].Value = sal;
			param[2].Value = rp_id;
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
		/// 查询奖惩记录，根据rp_id
		/// </summary>
		/// <param name="rp_id"></param>
		/// <returns>奖惩记录</returns>
		public RP GetRPById(int rp_id)
		{
			string sql = "select * from rp where rp_id=@rp_id";
			MySqlParameter[] param =
			{
				new MySqlParameter("@rp_id",MySqlDbType.Int32),
			};
			param[0].Value = rp_id;
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

		private RP DataRowToRP(DataRow dr)
		{
			int rp_id = Convert.ToInt32(dr["rp_id"]);
			string rp_name = Convert.ToString(dr["rp_name"]);
			int sal = Convert.ToInt32(dr["sal"]);
			return new RP(rp_id, rp_name, sal);
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
