using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusinessObject;


namespace DataAccess.Repository
{


    public class MemberDAO 
    {
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private static SaleContext context = new SaleContext();
        private MemberDAO()
        {

        }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance==null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

    

        public List<Member> GetMembers()
        {
            List<Member> list;
            try
            {
                list=context.Members.ToList();
            }
            catch( Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Member GetMemberById(int Id)
        {
            Member member;
            try
            {
                member=context.Members.SingleOrDefault(member => member.MemberId == Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return member;
        }

        public Member GetMemberByEmail(string Email)
        {
            Member member;
            try
            {
                member = context.Members.SingleOrDefault(member => member.Email==Email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return member;
        }

        public void InsertMember(Member member)
        {
            try
            {
                context.Members.Add(member);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

 

        public void UpdateMember(Member member)
        {
            try
            {
                var myEntiry = GetMemberById(member.MemberId);
                myEntiry.Email = member.Email;
                myEntiry.CompanyName = member.CompanyName;
                myEntiry.Country = member.Country;
                myEntiry.City = member.City;
                myEntiry.Password = member.Password;
                context.Entry<Member>(myEntiry).State= Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteMember(int Id)
        {
            Member member;
            try
            {
                member = context.Members.SingleOrDefault(member => member.MemberId == Id);
                context.Members.Remove(member);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
    
}
