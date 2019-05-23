using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TestApplication.DataLayer;
using TestApplication.Entities;
using TestApplication.Helpers;
using System.Linq.Dynamic;
using System.IO;

namespace TestApplication.Controllers
{
    public class MemberController : BaseController
    {
        public ActionResult AddMember(int? Id = null, bool? IsView = null)
        {
            LoadDefaultValues();
            ViewBag.IsView = IsView;
            ViewData["disablecontrols"] = false;
            if (IsView == true)
            {
                ViewData["disablecontrols"] = true;
            }
            if (!Id.HasValue)
            {
                return View(new MemberAccountNominee());
            }
            else
            {
                var tp = Repository.Instance.Find<MemberAccountNominee>(x => x.MemberId == Id);
                return View(tp);
            }
            
            var memberProfile = Repository.Instance.All<MemberAccountNominee>().FirstOrDefault(x => Id.HasValue && x.MemberId == Id);
            return Id.HasValue ? View(memberProfile ?? new MemberAccountNominee()) : View();
        }

        public void LoadDefaultValues()
        {
            ViewBag.AllCountries = Repository.Instance.All<Country>().Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).AsEnumerable();
            ViewBag.IndiaStates = Repository.Instance.All<State>().Where(x => x.CountryId == 101)
                                                                  .Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() })
                                                                  .AsEnumerable();
            var agents = new List<SelectListItem>();
            agents.Add(GetItem("Akash", "1"));
            agents.Add(GetItem("Gautam", "2"));
            agents.Add(GetItem("Agent1", "3"));
            ViewBag.Agents = agents;

            var braches = new List<SelectListItem>();
            braches.Add(GetItem("Gurgaon branch", "1"));
            braches.Add(GetItem("Delhi branch", "2"));
            braches.Add(GetItem("Other brnach", "3"));
            ViewBag.Branches = braches;

            var groups = new List<SelectListItem>();
            groups.Add(GetItem("Gurgaon Ladies", "1"));
            groups.Add(GetItem("Delhi Ladies", "2"));
            groups.Add(GetItem("Groups", "3"));
            ViewBag.Groups = groups;
        }

        private SelectListItem GetItem(string text, string value)
        {
            return new SelectListItem() { Text = text, Value = value };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMember(MemberAccountNominee memberAccountNominee)
        {
            LoadDefaultValues();
            if (ModelState.IsValid && memberAccountNominee != null)
            {
                memberAccountNominee.AssignedDate = DateTime.Now;
                Repository.Instance.Insert(memberAccountNominee, true);
                return RedirectToAction("MemberProfile");
            }
            return View();
        }

        public ActionResult MemberProfile(int? id = null)
        {
            var memberProfile = id.HasValue ?
                                Repository.Instance.All<MemberAccountNominee>().FirstOrDefault(x => id.HasValue && x.MemberId == id)
                                : Repository.Instance.All<MemberAccountNominee>().OrderByDescending(x => x.Id).FirstOrDefault();
            return View(memberProfile);
        }

        public ActionResult Members(PagedView<Member> pagedView)
        {
            //var filterPattern = string.Format("%{0}%", pagedView.FilterKey ?? "");

            //pagedView.Sort = pagedView.Sort ?? "EnrollmentDate";
            //pagedView.SortDir = pagedView.SortDir ?? "DESC";
            //DataLayer.Repository.Instance.Filter(x => (SqlFunctions.PatIndex(filterPattern, x.FirstName) > 0
            //    || SqlFunctions.PatIndex(filterPattern, x.Occupation) > 0
            //    || SqlFunctions.PatIndex(filterPattern, x.Mobile) > 0
            //    || SqlFunctions.PatIndex(filterPattern, x.Email) > 0
            //    || SqlFunctions.PatIndex(filterPattern, x.FatherName) > 0)
            //    , pagedView);
            //return View(pagedView);
            return View();
        }

        public ActionResult ShareHoldings(PagedView<ShareHolding> pagedView)
        {
            return View();
        }

        public ActionResult ViewShareHolding(int? id = null)
        {
            return View(new ShareHolding());
        }

        public ActionResult AddShareHolding(int? Id, bool? IsView = null)
        {
            ViewBag.IsView = IsView;
            ViewData["disablecontrols"] = false;
            if (IsView == true)
            {
                ViewData["disablecontrols"] = true;
            }
            if (!Id.HasValue)
            {
                return View();
            }
            else
            {
                var shareHolding = Repository.Instance.Find<ShareHolding>(x => x.Id == Id);
                return View(shareHolding);
            }

        }

        [HttpPut]
        public ActionResult DeleteShareHolding(int? id = null)
        {
            var shareHolding = Repository.Instance.Filter<ShareHolding>(x => x.Id == id).FirstOrDefault();
            if (shareHolding != null)
            {
                shareHolding.IsDeleted = true;
                Repository.Instance.Update(shareHolding);
            }
            return RedirectToAction("ShareHoldings");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddShareHolding(ShareHolding shareHolding)
        {
            if (ModelState.IsValid)
            {
                if (shareHolding.Id == 0)
                {
                    var newRecord = Repository.Instance.Insert(shareHolding, true);
                    return RedirectToAction("ShareHoldings");
                }
                else
                {
                    var grp = Repository.Instance.Update(shareHolding);
                    Repository.Instance.Commit();
                    return RedirectToAction("ShareHoldings");
                }
            }
            return View();
        }

        private int GetLimit(int limit)
        {
            return limit <= 0 || limit > 100 ? PageOptions.DefaultFundsLimit : limit;
        }

        public ActionResult Groups()
        {
            //var t =Repository.Instance.All<Groups>().Include(x=>x.MembersInGroup).ToList();
            //var yy = t.Select(x => x.MembersInGroup);
            //var jhj = t.Select(x => x.Members);

            //var q = Repository.Instance.All<Member>().ToList();
            //var yu = q.Select(x => x.MembersInGroup);
            return View();
        }

        public ActionResult InmateTbScreens()
        {
            return View();
        }

        public ActionResult AddGroup(int? Id, bool? IsView)
        {
            ViewBag.IsView = IsView;
            ViewData["disablecontrols"] = false;
            if (IsView == true)
            {
                ViewData["disablecontrols"] = true;
            }
            if (!Id.HasValue)
            {
                var group = new Group();
                group.CreationDate = DateTime.Now;
                return View(group);
            }
            else
            {
                var grp = Repository.Instance.Find<Group>(x => x.Id == Id);
                grp.MemberIds = grp.MembersInGroup.Select(x => x.MemberId.ToString()).ToArray();
                return View(grp);
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddGroup(Group group)
        {
            if (ModelState.IsValid)
            {
                if (group.Id == 0)
                {
                    group.MembersInGroup.AddRange(group.MemberIds.Select(x => new MembersInGroup() { MemberId = Convert.ToInt32(x) }));
                    var newRecord = Repository.Instance.Insert(group, true);
                    //var yy = newRecord.Members;
                    return RedirectToAction("Groups");
                }
                else
                {
                    var grp = Repository.Instance.Update(group);
                    group.MembersInGroup.AddRange(group.MemberIds.Select(x => new MembersInGroup() { MemberId = Convert.ToInt32(x) }));
                    Repository.Instance.Commit();
                    return RedirectToAction("Groups");
                }
            }
            return View();
        }

        public ActionResult Minors()
        {
            return View();
        }

        public ActionResult AddMinor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMinor(Member member)
        {
            if (ModelState.IsValid)
            {
                if (member.Id == 0)
                {
                    var newRecord = Repository.Instance.Insert(member, true);
                }
                else
                {
                    Repository.Instance.Update(member);
                    Repository.Instance.Commit();
                }
            }
            return View();
        }

        public ActionResult UploadDocuments()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadDocuments(UploadDocument uploadDocument)
        {
            try
            {
                if (uploadDocument.FileAttach.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(uploadDocument.FileAttach.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    uploadDocument.FileAttach.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }


        #region Ajax Methods

        public string LoadMembers()
        {
            var source = Repository.Instance.All<Member>().ToList();
            return base.LoadGridAjax(source);
        }

        public string LoadGroups()
        {
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
            return base.LoadGridAjax<Group>(Repository.Instance.All<Group>(), x=>x.Name.IndexOf(searchValue) != -1);
        }

        public string LoadShareHoldings()
        {
            return base.LoadGridAjax<ShareHolding>(Repository.Instance.All<ShareHolding>().ToList());
        }

        public string LoadData()
        {
            return base.LoadGridAjax<InmateTbScreens>();
        }

        public string LoadMinors()
        {
            var minors = Repository.Instance.Filter<Member>(x => x.IsMinor).ToList();
            return base.LoadGridAjax(minors);
        }

        #endregion
    }
}