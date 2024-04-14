using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebBanhTrang.Models;
using WebBanhTrang.Repositories;
using WebBanhTrang.Controllers;

namespace WebBanhTrang.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        /* public HomeController(ILogger<HomeController> logger)
         {
             _logger = logger;
         }*/
        public HomeController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        /*        public async Task<IActionResult> Index()
                {
                    var products = await _productRepository.GetAllAsync();
                    return View(products);


                }*/
        public async Task<IActionResult> FilterByCategory(int categoryId)
        {
            var products = await _productRepository.GetByCategoryIdAsync(categoryId);
            return View("Index", products);
        }
        [HttpGet]
        public async Task<IActionResult> Index(string name, decimal? to, decimal? from)
        {
            var products = await _productRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(name))
            {
                if (to != null && from != null)
                {
                    products = products.Where(x => x.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0 && x.Price >= to && x.Price <= from);
                }
                else
                {
                    products = products.Where(x => x.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }
            else
            {
                if (to != null && from != null)
                {
                    products = products.Where(x => x.Price >= to && x.Price <= from);
                }
            }
            /*var product = await _productRepository.GetAllAsync();*/
            ViewBag.Categories = await _categoryRepository.GetAllAsync();

            return View(products);
        }

        public IActionResult Search(string keyword)
        {
            // ?i?u h??ng ng??i dùng ??n trang hi?n th? k?t qu? tìm ki?m v?i t? khóa ?ã nh?p
            return RedirectToAction("SearchResult", new { keyword = keyword });
        }

        public async Task<IActionResult> SearchResult(string keyword)
        {
            var products = await _productRepository.SearchAsync(keyword);
            return View(products);

            /*var products = await _productRepository.SearchAsync(keyword);
            return View("Index", products); // Sử dụng view "Index" để hiển thị danh sách sản phẩm*/
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
