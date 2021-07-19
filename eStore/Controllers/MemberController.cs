using BusinessObject;
using DataAccess;
using DataAccess.Repository;
using eStore.Controllers.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.Controllers
{
    public class MemberController : Controller
    {
        IMemberRepository memberRepository= new MemberRepository();


        public ActionResult Login()
        {
            return View();
        }

        // POST: MemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Member member)
        {
            
            var t = memberRepository.GetMemberByEmail(member.Email);
            if (t == null) ViewData["Message"] = "Account does not exist!";
            else
            if (!t.Password.Equals(member.Password)) ViewData["Message"] = "Wrong password";
            else
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "id", t.MemberId);
                return RedirectToAction(nameof(Index)); }

            member.Password=" ";
                return View(member);
        }

        // GET: MemberController
        public ActionResult Index()
        {
            var model = memberRepository.GetMembers();
            return View(model);
        }

        // GET: MemberController/Details/5
        public ActionResult Details(int id)
        {
           
            var member = memberRepository.GetMemberById(id);
            if (member == null) return NotFound();
            return View(member);
        }

        // GET: MemberController/Create
        public ActionResult Create()
        {
            if (SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "id")==1)
            return View();
            return RedirectToAction(nameof(Index));
        }

        // POST: MemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member member)
        {
           
            try
            {
                if (ModelState.IsValid)
                {
                    memberRepository.InsertMember(member);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(member);
            }
        }

        // GET: MemberController/Edit/5
        public ActionResult Edit(int id)
        {
            int aID = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "id");
            if (aID == 1 || aID == id)
            {
                var member = memberRepository.GetMemberById(id);
                if (member == null) return NotFound();
                return View(member);
            }
            else return RedirectToAction(nameof(Index));
        }

        // POST: MemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Member member)
        {
            try
            {
                if (id != member.MemberId) return NotFound();
                if (ModelState.IsValid)
                {
                    memberRepository.UpdateMember(member);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(member);
            }
        }

        // GET: MemberController/Delete/5
        public ActionResult Delete(int id)
        {
            
            int aID = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "id");
            if (aID == 1 || aID == id)
            {
                var member = memberRepository.GetMemberById(id);
                if (member == null) return NotFound();
                return View(member);
            } else
            return RedirectToAction(nameof(Index));
        }

        // POST: MemberController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                memberRepository.DeleteMember(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
