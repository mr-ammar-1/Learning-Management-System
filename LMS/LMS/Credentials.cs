using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS
{
    class Credentials
    {
        class Credential
        {
            private string Username;
            private string Password;
            private string Role;

            public void setUsername(String username)
            {
                Username = username;
            }
            public void setPassword(String password)
            {
                Password = password;
            }
            public void setRole(String role)
            {
                Role = role;
            }
            public string getUsername()
            {
                return Username;
            }
            public string getPassword()
            {
                return Password;
            }
            public string getRole()
            {
                return Role;
            }
            // Constructor
            public Credential(string username, string password, string role)
            {
                Username = username;
                Password = password;
                Role = role;
            }
            public Credential(string username,string password)
            {
                username = this.Username;
                password = this.Password;
            }
            public void displayrol()
            {
                
            }
        }
    }
}
