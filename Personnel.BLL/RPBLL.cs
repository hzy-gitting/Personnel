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
		/// 添加奖惩信息（rp_id字段为自动生成，无需手动指定）
		/// </summary>
		/// <param name="rp_name"></param>
		/// <param name="sal"></param>
		/// <returns>添加成功返回true，否则返回false</returns>
		public bool AddRP(string rp_name, int sal)
		{
			return rpDal.AddRP(rp_name, sal) > 0;
		}
		/// <summary>
		/// 更改奖惩记录，根据id
		/// </summary>
		/// <param name="rp_id"></param>
		/// <returns>更改成功返回true，否则返回false</returns>
		public bool UpdateRPById(int rp_id, string rp_name, int sal)
		{
			return rpDal.UpdateRPById(rp_id, rp_name, sal) > 0;
		}

		/// <summary>
		/// 删除奖惩记录，根据id
		/// </summary>
		/// <param name="rp_id"></param>
		/// <returns>删除成功返回true，否则返回false</returns>
		public bool DeleteRPByRPId(int rp_id)
		{
			return rpDal.DeleteRPByRPId(rp_id) > 0;
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
		/// 查询奖惩记录，根据rp_id
		/// </summary>
		/// <param name="rp_id"></param>
		/// <returns>符合条件的奖惩记录</returns>
		public RP GetRPById(int rp_id)
		{
			return rpDal.GetRPById(rp_id);
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
	}
}
