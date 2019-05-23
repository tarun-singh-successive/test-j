using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TestApplication.DataLayer;
using static TestApplication.Helpers.Enumerations;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using Newtonsoft.Json;
using TestApplication.Helpers;
using System.Collections;

namespace TestApplication.Controllers
{
    public class BaseController : Controller
    {
        private void SetUserPermission()
        {
            var user1 = HttpContext?.User;
            var ggg = user1?.Identity?.Name;
            if (User?.Identity?.IsAuthenticated ?? false)
            {
            }
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            var UserManager = requestContext?.HttpContext?.GetOwinContext()?.GetUserManager<ApplicationUserManager>();
            var user = requestContext?.HttpContext?.User;
            if (user != null && user.Identity.IsAuthenticated)
            {
                var applicationsUser = UserManager.FindByName(user.Identity.Name);
                var roles = UserManager.GetRoles(applicationsUser?.Id);
                ViewBag.IsAdmin = roles.Contains(UserRoles.User);
            }
        }

        public virtual string LoadGridAjax<T>(IEnumerable<T> source = null, Expression<Func<T, bool>> predicate = null, params string[] includes) where T : class
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                if (source == null || source is IQueryable)
                {
                    var modelData = source == null ?  Repository.Instance.All<T>(includes) : source;
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                            modelData = Repository.Instance.Filter(predicate);

                        //if (source != null)
                        //    modelData = source.Intersect(modelData);
                        //var searchColumns = search.Count() == 0 ? ObjectUtility.GetAllColumnsByObject<T>().ToArray() : search;
                        //modelData = modelData.Where(x => searchColumns.Any(y => (x.GetPropertyValue(y)).ToString().ToLower().IndexOf(searchValue.ToLower()) != -1));
                    }
                    //Sorting  
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        modelData = modelData.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    ////total number of rows count   
                    recordsTotal = modelData.Count();
                    ////Paging   
                    pageSize = recordsTotal < pageSize ? recordsTotal : pageSize;
                    var data = modelData.Skip(skip).Take(pageSize).ToList();
                    //Returning Json Data 
                    return JsonConvert.SerializeObject(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, Formatting.Indented,
                                new JsonSerializerSettings
                                {
                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                });
                }
                else
                {
                    var modelData = source;
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        var searchColumns = predicate == null ? ObjectUtility.GetAllColumnsByObject<T>().ToArray() : null;
                        //var u = modelData.Where<T>(predicate);//(searchColumns == null) ? modelData.Where<T>(predicate).As;
                        if (searchColumns == null)
                        {
                            modelData = modelData.AsQueryable().Where(predicate);
                        }
                        else
                        {
                            modelData = modelData.Where(x => searchColumns.Any(y => (x.GetPropertyValue(y)).ToString().ToLower().IndexOf(searchValue.ToLower()) != -1));
                        }
                    }
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        modelData = sortColumnDir.ToLower() == "asc" ? modelData.OrderBy(x => x.GetPropertyValue(sortColumn)) : modelData.OrderByDescending(x => x.GetPropertyValue(sortColumn));
                    }
                    recordsTotal = modelData.Count();
                    pageSize = recordsTotal < pageSize ? recordsTotal : pageSize;
                    var data = modelData.Skip(skip).Take(pageSize).ToList();
                    return JsonConvert.SerializeObject(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, Formatting.Indented,
                                new JsonSerializerSettings
                                {
                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                });
                }
            }
            catch (Exception ex)
            {
                throw;

                Console.WriteLine(ex);
                var modelData = (from tempcustomer in Repository.Instance.All<T>()
                                 select tempcustomer);
                return JsonConvert.SerializeObject(new { draw = "", recordsFiltered = modelData.Count(), recordsTotal = modelData.Count(), data = modelData.Take(100).ToList() }, Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });
            }
        }

        public BaseController()
        {
            //SetUserPermission();
        }

    }
}