using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        List<Member> GetMembers();
        Member GetMemberById(int Id);
        Member GetMemberByEmail(String Email);
        void InsertMember(Member member);
        void DeleteMember(int Id);
        void UpdateMember(Member member);

    }
}
