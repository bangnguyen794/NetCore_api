using ApiCore_facebook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore_facebook.Library
{
    /// <summary>
    /// Trả về user kèm token 
    /// </summary>
    public interface IUserService
    {
        User Authenticate(string id_user);
        //IEnumerable<User> GetAll();
        //User GetById(int id);
    }
   
    public class UserService : IUserService
    {
        db_facebookContext XLDL = new db_facebookContext();
        private readonly AppSettings _appSettings;
        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public  User Authenticate(string id_user)
        {
            List<User> _users = new List<User>();
            var query = XLDL.FbUserToken.AsNoTracking().Where(x=>x.IdUser == id_user).Select(x=> new  {x.Id, x.IdUser,x.NameUser }).Take(1).FirstOrDefault();
            var query_role = XLDL.FbSetting.AsNoTracking().Any(x => x.FullQuyen.Contains(id_user));
            var query_page = XLDL.FbPageDetail.AsNoTracking().Where(x => x.IdUser==id_user).Select(s=>new { s.IdPage}).ToList();
            string quyen = "user",str_page = "";
            if (query_role) quyen = "full_admin";
            if (query_page!=null)
            {
                foreach(var row in query_page)
                {
                    str_page += row.IdPage + ",";
                }
            }
            //// return null if user not found
            if (query!=null)
            {
                _users.Add(new User { Id = query.Id,id_user=query.IdUser, Fullname = query.NameUser, Role = quyen });
            }
            else
            {
                return null;
            }
            var user = _users.SingleOrDefault();
            if (user == null)
                return null;
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Version,"v1"),
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Fullname.ToString()),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.GroupSid, str_page.TrimEnd(','))
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
           
            // remove password before returning
            user.Password = null;

            return user;
        }

        //public IEnumerable<User> GetAll()
        //{
        //    // return users without passwords
        //    return _users.Select(x => {
        //        x.Password = null;
        //        return x;
        //    });
        //}

        //public User GetById(int id)
        //{
        //    var user = _users.FirstOrDefault(x => x.Id == id);

        //    // return user without password
        //    if (user != null)
        //        user.Password = null;

        //    return user;
        //}
    }
}
