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
	/// 离职BLL
	/// 2020/7/10 16：38 通过了单元测试
	/// 2020/7/11 19:36 修改了AddQuit方法的签名：新增id参数，来指明是哪位员工离职
	/// hzy
	/// </summary>
	public class QuitBLL
	{
		QuitDAL quitDal = new QuitDAL();

		/// <summary>
		/// 添加离职信息
		/// </summary>
		/// <param name="quit_date"></param>
		/// <param name="quit_reason"></param>
		/// <returns>添加成功返回true， 否则返回false</returns>
		public bool AddQuit(int id,DateTime quit_date, string quit_reason)
		{
			return quitDal.AddQuit(id, quit_date, quit_reason) > 0;
		}

		/// <summary>
		/// 删除离职信息，根据id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>删除成功返回true，否则返回false</returns>
		public bool DeleteQuitById(int id)
		{
			return quitDal.DeleteQuitById(id) > 0;
		}
		/// <summary>
		/// 更改离职信息，根据id
		/// </summary>
		/// <param name="id"></param>
		/// <param name="quit_date"></param>
		/// <param name="quit_reason"></param>
		/// <returns>更改成功返回true，否则返回false</returns>
		public bool UpdateQuitById(int id, DateTime quit_date, string quit_reason)
		{
			return quitDal.UpdateQuitById(id, quit_date, quit_reason) > 0;
		}

		/// <summary>
		/// 获取所有离职信息
		/// </summary>
		/// <returns>离职信息列表，若为空返回null</returns>
		public List<Quit> GetAllQuit()
		{
			return quitDal.GetAll();
		}

		/// <summary>
		/// 查询离职信息，根据id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>符合条件的离职信息,不存在则返回null</returns>
		public Quit GetQuitById(int id)
		{
			return quitDal.GetQuitById(id);
		}

		/// <summary>
		/// 查询离职信息，根据离职时间
		/// </summary>
		/// <param name="quit_date"></param>
		/// <returns>符合条件的离职信息列表,若不存在符合条件的则返回null</returns>
		public List<Quit> GetQuitByQuitDate(DateTime quit_date)
		{
			return quitDal.GetQuitByQuitDate(quit_date);
		}

		/// <summary>
		/// 查询离职信息，根据离职原因
		/// </summary>
		/// <param name="quit_date"></param>
		/// <returns>符合条件的离职信息列表,若不存在符合条件的则返回null</returns>
		public List<Quit> GetQuitByQuitReason(string quit_reason)
		{
			return quitDal.GetQuitByQuitReason(quit_reason);
		}
	}
}
