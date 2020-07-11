using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using personnel.DAL;
using personnel.Model;

namespace personnel.BLL
{
	/// <summary>
	/// 奖惩BLL
	/// 2020 7/10 15：12 通过了单元测试
	/// hzy
	/// </summary>
	public class RPBLL
	{
		RPDAL rpDal = new RPDAL();

		/// <summary>
		/// 添加奖惩记录（no字段为自动生成，无需手动指定）
		/// </summary>
		/// <param name="rp_name"></param>
		/// <param name="sal"></param>
		/// <returns>添加成功返回true，否则返回false</returns>
		public bool AddRP(string rp_name, int sal, int user_id, DateTime rp_time)
		{
			return rpDal.AddRP(rp_name, sal, user_id,rp_time) > 0;
		}

		/// <summary>
		/// 更改奖惩记录，根据id
		/// </summary>
		/// <param name="rp_id"></param>
		/// <returns>更改成功返回true，否则返回false</returns>
		public bool UpdateRPById(int no, string rp_name, int sal, int user_id, DateTime rp_time)
		{
			return rpDal.UpdateRPByNo(no, rp_name, sal,user_id,rp_time) > 0;
		}

		/// <summary>
		/// 删除奖惩记录，根据id
		/// </summary>
		/// <param name="rp_id"></param>
		/// <returns>删除成功返回true，否则返回false</returns>
		public bool DeleteRPByNo(int no)
		{
			return rpDal.DeleteRPByNo(no) > 0;
		}

		/// <summary>
		/// 获得所有奖惩记录
		/// </summary>
		/// <returns>奖惩记录列表</returns>
		public List<RP> GetAllRP()
		{
			return rpDal.GetAll();
		}

		/// <summary>
		/// 查询奖惩记录，根据no
		/// </summary>
		/// <param name="no"></param>
		/// <returns>符合条件的奖惩记录</returns>
		public RP GetRPByNo(int no)
		{
			return rpDal.GetRPByNo(no);
		}

		/// <summary>
		/// 查询奖惩记录，根据rp_name
		/// </summary>
		/// <param name="rp_name"></param>
		/// <returns>符合条件的奖惩记录列表</returns>
		public List<RP> GetRPByRPName(string rp_name)
		{
			return rpDal.GetRPByRPName(rp_name);
		}

		/// <summary>
		/// 查找奖惩记录，根据user_id
		/// </summary>
		/// <param name="user_id"></param>
		/// <returns>返回符合条件的奖惩记录列表</returns>
		public List<RP> GetRPsByUserId(int user_id)
		{
			return rpDal.GetRPsByUserId(user_id);
		}

		/// <summary>
		/// 查找奖惩记录，根据具体日期
		/// </summary>
		/// <param name="rp_time"></param>
		/// <returns>返回符合条件的奖惩记录列表</returns>
		public List<RP> GetRPsByRPTime(DateTime rp_time)
		{
			return rpDal.GetRPsByRPTime(rp_time);
		}


		/// <summary>
		/// 查找位于某一时间段[later,before]之内的奖惩记录
		/// </summary>
		/// <param name="later"></param>
		/// <param name="before"></param>
		/// <returns>返回符合条件的奖惩记录列表</returns>
		public List<RP> GetRPsByRPTimeBetween(DateTime later, DateTime before)
		{
			return rpDal.GetRPsByRPTimeBetween(later, before);
		}

		
	}
}
