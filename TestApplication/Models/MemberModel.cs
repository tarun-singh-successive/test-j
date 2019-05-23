using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApplication.DataLayer;
using TestApplication.Entities;

namespace TestApplication.Models
{
    public class MemberModel
    {
        public static SelectList GetMembers()
        {
            return new SelectList(Repository.Instance.Filter<Member>(x => !x.IsPromoter).ToList(), "Id", "Name");
        }

        public static SelectList GetPromoters()
        {
            return new SelectList(Repository.Instance.Filter<Member>(x => x.IsPromoter).ToList(), "Id", "Name");
        }

        public static string GetMemberNameById(int id) => Repository.Instance.Filter<Member>(x => x.Id == id).FirstOrDefault()?.Name ?? string.Empty;

        public static List<SelectListItem> GetBranches() => Repository.Instance.All<Branches>().ToList().Select(x => GetItem(x.Name, x.Id.ToString())).ToList();

        public static List<SelectListItem> GetGroups() => Repository.Instance.All<Group>().ToList().Select(x => GetItem(x.Name, x.Id.ToString())).ToList();

        private static SelectListItem GetItem(string text, string value)
        {
            return new SelectListItem() { Text = text, Value = value };
        }

        public static List<SelectListItem> GetAgents()
        {
            var agents = new List<SelectListItem>();
            agents.Add(GetItem("Akash", "1"));
            agents.Add(GetItem("Gautam", "2"));
            agents.Add(GetItem("Agent1", "3"));
            return agents;
        }

        public static List<SelectListItem> GetChequeBanks()
        {
            var banks = new List<SelectListItem>();
            banks.Add(GetItem("Hdfc", "1"));
            banks.Add(GetItem("Sbi", "2"));
            banks.Add(GetItem("Pnb", "3"));
            return banks;
        }
    }
}