using WebBanhTrang.Models;

namespace WebBanhTrang.Repositories
{
	public interface IOrderRepository
	{
		IEnumerable<Order> GetAll();
		Order GetById(int id);
		void CancelOrder(int id);
		void DeleteOrder(int id);
	}
}
