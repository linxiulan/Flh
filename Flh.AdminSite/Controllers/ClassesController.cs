﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Flh.Web;
using Flh.Business;
using Newtonsoft.Json;

namespace Flh.AdminSite.Controllers
{
    [FlhAuthorize]
    public class ClassesController : BaseController
    {
        private readonly IClassesManager _ClassesManager;

        public ClassesController(IClassesManager classesManager)
        {
            _ClassesManager = classesManager;
        }

        public ActionResult List(string pno, int? page)
        {
            if (String.IsNullOrWhiteSpace(pno) || !pno.StartsWith(FlhConfig.CLASSNO_CLASS_PREFIX))
            {
                pno = FlhConfig.CLASSNO_CLASS_PREFIX;
            }
            if (!page.HasValue || page.Value < 1)
                page = 1;
            var size = 30;
            var parent = _ClassesManager.GetEnabled(pno);

            var classes = _ClassesManager.GetChildren(pno);

            return View(new Models.Classes.ListModel(){
                ParentNo = pno.Trim(),
                ParentFullName = Util.DisplayClassFullName(parent.full_name),
                Items = new PageModel<Models.Classes.ListModel.Item>(classes
                            .OrderByDescending(n => n.order_by)
                            .ThenByDescending(n => n.created)
                            .Skip((page.Value - 1) * size)
                            .Take(size)
                            .Select(n => new Models.Classes.ListModel.Item{
                                Name = n.name,
                                No = n.no,
                                Order = n.order_by
                            }), page.Value, (int)Math.Ceiling((double)classes.Count()/(double)size))
            });
        }

        [HttpGet]
        public ActionResult BatchAdd(string pno)
        {
            var parent = _ClassesManager.GetEnabled(pno);

            return View(new Models.Classes.BatchAddModel(6)
            {
                ParentNo = parent.no,
                ParentFullName = Util.DisplayClassFullName(parent.full_name)
            });
        }

        [HttpPost]
        public ActionResult BatchAdd(string pno, string model)
        {
            var items = JsonConvert.DeserializeObject<Models.Classes.BatchAddModel.EditModel[]>(model);
            _ClassesManager.AddRange(this.CurrentUser.Uid, pno, items);
            return SuccessJsonResult();
        }
    }
}