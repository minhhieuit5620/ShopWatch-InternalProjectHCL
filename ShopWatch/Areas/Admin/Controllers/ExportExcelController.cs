using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;

namespace ShopWatch.Areas.Admin.Controllers
{
    public class ExportExcelController : Controller
    {
        // GET: Admin/ExportExcel
        public ActionResult Index()
        {
            return View();
        }

      

    //    public ActionResult Default(string SaveOption)
    //    {
    //        if (SaveOption == null)
    //            return View();
    //        //Step 1 : Instantiate the spreadsheet creation engine.
    //        ExcelEngine excelEngine = new ExcelEngine();
    //        //Step 2 : Instantiate the excel application object.
    //        IApplication application = excelEngine.Excel;

    //        //A new workbook is created.[Equivalent to creating a new workbook in Microsoft Excel]
    //        //The new workbook will have 12 worksheets
    //        IWorkbook workbook = application.Workbooks.Open(Resol   ResolveApplicationDataPath(@"BudgetPlanner.xls"));

    //        IWorksheet sheet = workbook.Worksheets[1];
    //        sheet.FirstVisibleRow = 3;

    //        IFont font = workbook.CreateFont();
    //        font.Bold = true;


    //        ITextBoxShape textbox = sheet.TextBoxes.AddTextBox(5, 2, 40, 140);
    //        textbox.Text = "Quick Budget";
    //        textbox.RichText.SetFont(0, 11, font);
    //        textbox.VAlignment = ExcelCommentVAlign.Center;
    //        textbox.HAlignment = ExcelCommentHAlign.Center;
    //        textbox.Fill.FillType = ExcelFillType.Gradient;
    //        textbox.Fill.GradientColorType = ExcelGradientColor.TwoColor;
    //        textbox.Fill.TwoColorGradient(ExcelGradientStyle.Vertical, ExcelGradientVariants.ShadingVariants_2);
    //        textbox.Fill.BackColor = Color.FromArgb(204, 204, 255);

    //        textbox = sheet.TextBoxes.AddTextBox(7, 2, 25, 140);
    //        textbox.Text = "Income";
    //        textbox.RichText.SetFont(0, 5, font);
    //        textbox.VAlignment = ExcelCommentVAlign.Center;
    //        textbox.HAlignment = ExcelCommentHAlign.Center;
    //        textbox.Fill.FillType = ExcelFillType.Gradient;
    //        textbox.Fill.GradientColorType = ExcelGradientColor.TwoColor;
    //        textbox.Fill.TwoColorGradient(ExcelGradientStyle.Vertical, ExcelGradientVariants.ShadingVariants_2);
    //        textbox.Fill.BackColor = Color.FromArgb(0, 0, 128);

    //        textbox = sheet.TextBoxes.AddTextBox(16, 2, 25, 140);
    //        textbox.Text = "Spending";
    //        textbox.RichText.SetFont(0, 7, font);
    //        textbox.VAlignment = ExcelCommentVAlign.Center;
    //        textbox.HAlignment = ExcelCommentHAlign.Center;
    //        textbox.Fill.FillType = ExcelFillType.Gradient;
    //        textbox.Fill.GradientColorType = ExcelGradientColor.TwoColor;
    //        textbox.Fill.TwoColorGradient(ExcelGradientStyle.Vertical, ExcelGradientVariants.ShadingVariants_2);
    //        textbox.Fill.BackColor = Color.FromArgb(0, 0, 128);



    //        try
    //        {
    //            //Saving the workbook to disk.
    //            if (SaveOption == "Xls")
    //            {
    //                //Save as .xls format
    //                workbook.SaveAs("SpreadSheet.xls", HttpContext.ApplicationInstance.Response, ExcelDownloadType.PromptDialog, ExcelHttpContentType.Excel97);
    //            }
    //            //Save as .xlsx format
    //            else
    //            {
    //                workbook.Version = ExcelVersion.Excel2016;
    //                workbook.SaveAs("SpreadSheet.xlsx", HttpContext.ApplicationInstance.Response, ExcelDownloadType.PromptDialog, ExcelHttpContentType.Excel2016);
    //            }
    //        }
    //        catch (Exception)
    //        {
    //        }

    //        //Close the workbook.
    //        workbook.Close();
    //        excelEngine.Dispose();
    //        return View();
    //    }

    //}
}
}