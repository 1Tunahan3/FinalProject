using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;

namespace Buisness.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        void Add(User user);
        User GetByMail(string email);
    }
}
