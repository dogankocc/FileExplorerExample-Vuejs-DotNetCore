using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FileTreeExample.Models;
using System.IO;
using FileTreeExample.DTOs;
using Newtonsoft.Json.Linq;

namespace FileTreeExample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string toggleID = "")
        {
            DirectoryInfo inf_ = new DirectoryInfo(@"C:\inetpub\intranet\files\Ölçek");
            FileDTO dt = new FileDTO();
            dt.ID = Guid.NewGuid();
            dt.FileName = inf_.FullName;
            dt.Name = inf_.Name;
            dt.Extension = inf_.Extension;
            dt.Expand = true;
            GetSubDirectoryAndFiles(dt, "Ölçek");
            ViewBag.Directories = JToken.FromObject(dt);
            ViewBag.ToggleID = toggleID;

            return View();
        }

        public void GetSubDirectoryAndFiles(FileDTO dt, string id)
        {
            DirectoryInfo di = new DirectoryInfo(dt.FileName);

            var SubFilesOrderby = di.GetDirectories().OrderBy(x => x.FullName).ToList();

            foreach (var subDi in SubFilesOrderby)
            {
                FileDTO subDt = new FileDTO();
                subDt.ID = Guid.NewGuid();
                subDt.FileName = subDi.FullName;
                subDt.Name = subDi.Name;
                subDt.Extension = subDi.Extension;
                if (("C:\\inetpub\\intranet\\files\\" + id).Contains(subDt.FileName))
                {
                    subDt.Expand = true;
                }
                else
                {
                    subDt.Expand = false;
                }
                dt.Directories.Add(subDt);
                GetSubDirectoryAndFiles(subDt, id);
            }
            foreach (FileInfo fi in di.GetFiles())
            {
                FileDTO subDt = new FileDTO();
                subDt.ID = Guid.NewGuid();
                subDt.FileName = fi.FullName;
                subDt.Name = fi.Name;
                subDt.Extension = fi.Extension;
                dt.Files.Add(subDt);
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
