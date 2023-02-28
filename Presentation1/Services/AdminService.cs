using Core.Entities;
using Core.Helper;
using Data.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation1.Services
{
    public class AdminService
    {
        private readonly AdminRepository _adminRepository;
        public AdminService() 
        {
            _adminRepository = new AdminRepository();
        }

        public Admin Authorize()
        {
        LoginDesc: ConsoleHelper.WriteWithColor("--- Log in ---,please", ConsoleColor.White);
            Console.WriteLine();
            ConsoleHelper.WriteWithColor("--- Enter username  ---,please", ConsoleColor.Gray);
            string username = Console.ReadLine();
            ConsoleHelper.WriteWithColor("--- Enter password ---,please", ConsoleColor.Gray);
            string password = Console.ReadLine();
            var admin = _adminRepository.GetbyUsernameAndPassword(username, password);
            if (admin == null)
            {
                ConsoleHelper.WriteWithColor(" The username or password is incorrect!", ConsoleColor.Red);
                goto LoginDesc;
            }
            return admin;

        }


    }
}
