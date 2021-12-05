using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using FirstZX.Core.Security;
using FirstZX.Core.Services.Interface;

namespace FirstZX.Areas.Admin.Controllers
{
    [Area("admin")]
    [PermissionChecker(2)]
    public class ForcastForAdmin : Controller
    {

        private IUserService _userService;
        private IPermission _permission;
        private IExcelExport _excelExport;

        public ForcastForAdmin(IUserService userService, IPermission permission, IExcelExport excelExport)
        {
            _userService = userService;
            _permission = permission;
            _excelExport = excelExport;
        }
        public IActionResult ListOfUser(int pageId = 1, string filterEmail = "", string filerUserId = "")
        {

            return View(_userService.GetUserForecastForAdnin(pageId, filterEmail, filerUserId));
        }
        [Route("ViewUserForecast")]
        public IActionResult ViewUserForecast(int UAId)
        {
            if (TempData["status"] != null)
            {
                ViewBag.IsRestart = true;
            };

            return View(_userService.getUserAnswersForAdmin(UAId));
        }
        [Route("RestartUserForcast")]
        public IActionResult RestartUserForcast(int UAId)
        {

            _userService.RestartUserForcast(UAId);
            TempData["status"] = true;
            var url = "/ViewUserForecast?UAId=" + UAId;
            return Redirect(url);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExportForecastAdmin()
        {
            var list = _excelExport.GetInformationForExcelFromForecaste();
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[10]
            {
                new DataColumn("User Id"),
                new DataColumn("Email"),
                new DataColumn("Time"),
                new DataColumn("User Answer Id"),
                new DataColumn("Answer Id"),
                new DataColumn("Coin"),
                new DataColumn("Ratio Coin"),
                new DataColumn("Buy or Sell (1 is buy 2 is sell)"),
                new DataColumn("Percentage"),
                new DataColumn("IsDelete"),
             });
            foreach (var item in list)
            {
                dt.Rows.Add(item.UserId,
                    item.Email,
                    item.DateTime,
                    item.UserAnswerId,
                    item.AnswerId,
                    item.Cryptocurrency,
                    item.RatioCryptocurrency,
                    item.BuyOrSell,
                    item.Percentage,
                    item.IsDelete);
            }

            using (XLWorkbook wb=new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream=new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Grid.xlsx");
                }
            }
            

            
        }

    }
}
