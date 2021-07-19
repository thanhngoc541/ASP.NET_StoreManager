using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public void DeleteMember(int Id) => MemberDAO.Instance.DeleteMember(Id);

        public Member GetMemberByEmail(string Email) => MemberDAO.Instance.GetMemberByEmail(Email);

        public Member GetMemberById(int Id) => MemberDAO.Instance.GetMemberById(Id);

        public List<Member> GetMembers() => MemberDAO.Instance.GetMembers();

        public void InsertMember(Member member) => MemberDAO.Instance.InsertMember(member);

        public void UpdateMember(Member member) => MemberDAO.Instance.UpdateMember(member);
    }

}
