using Core.Constants;
using Core.Helper;
using Data.Repositories.Concrete;
using Presentation1.Services;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using Core.Entities;
using Data;
using Data.Contexts;

namespace Presentation1
{
    public static class Program
    {
        private readonly static GroupService _groupService;
        private readonly static StudentService _studentService;
        private readonly static TeacherService _teacherService;
        private readonly static AdminService _adminService;

        static Program()
        {
            _groupService = new GroupService();
            _studentService = new StudentService();
            _teacherService = new TeacherService();
            _adminService = new AdminService();
            DbInitializer.SeedAdmins();

        }


        static void Main()
        {

        AuthorizeDesc: var admin = _adminService.Authorize();
            if (admin != null)
            {
                while (true)
                {
                    ConsoleHelper.WriteWithColor("--- Welcome!,{admin.Username} ---", ConsoleColor.Cyan);

                MainMenuDesc: ConsoleHelper.WriteWithColor(" 1 - Groups ", ConsoleColor.Yellow);
                    ConsoleHelper.WriteWithColor(" 2 - Students", ConsoleColor.Yellow);
                    ConsoleHelper.WriteWithColor(" 3 - Teachers", ConsoleColor.Yellow);
                    ConsoleHelper.WriteWithColor(" 0 - Logout ", ConsoleColor.Yellow);

                    int number;
                    bool isSucceeded = int.TryParse(Console.ReadLine(), out number);
                    if (!isSucceeded)
                    {
                        ConsoleHelper.WriteWithColor("The entered number is an incorrect format!", ConsoleColor.Red);
                        goto MainMenuDesc;
                    }
                    else
                    {
                        switch (number)
                        {
                            case (int)MainMenuOptions.Groups:
                                while (true)

                                {

                                ListDescription: ConsoleHelper.WriteWithColor(" 1 - Create Group", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor(" 2 - Update Group", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor(" 3 - Delete Group", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor(" 4 - Get All Group", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor(" 5 - Get Group by ID", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor(" 6 - Get Group by Name", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor(" 7 - Get All Groups by Teacher", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor(" 0 - Back to Main Menu", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor(" Select one of the options above", ConsoleColor.Cyan);

                                    isSucceeded = int.TryParse(Console.ReadLine(), out number);
                                    if (!isSucceeded)
                                    {
                                        ConsoleHelper.WriteWithColor("The entered number is not a correct format!", ConsoleColor.Red);
                                    }

                                    switch (number)
                                    {
                                        case (int)GroupOptions.CreateGroup:
                                            _groupService.Create(admin);
                                            break;

                                        case (int)GroupOptions.UpdateGroup:
                                            _groupService.GetUpdate();
                                            break;

                                        case (int)GroupOptions.DeleteGroup:
                                            _groupService.Delete();
                                            break;

                                        case (int)GroupOptions.GetAllGroup:
                                            _groupService.GetAll();
                                            break
                                                ;
                                        case (int)GroupOptions.GetGroupbyID:
                                            _groupService.GetGroupbyID(admin);
                                            break;

                                        case (int)GroupOptions.GetGroupbyName:
                                            _groupService.GetGroupbyName();
                                            break;
                                        case (int)GroupOptions.GetAllGroupsbyTeacher:
                                            _groupService.GetAllGroupsbyTeacher();
                                            break;
                                        case (int)GroupOptions.BacktoMainMenu:
                                            goto MainMenuDesc;

                                        default:
                                            ConsoleHelper.WriteWithColor("There is not such a number in the list!", ConsoleColor.Red);
                                            goto ListDescription;

                                    }
                                }

                            case (int)MainMenuOptions.Students:
                                while (true)
                                {
                                ListDescription: ConsoleHelper.WriteWithColor("1 - Create Student", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("2 - Update Student", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("3 - Delete Student", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("4 - Get All Students", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("5 - Get All Students by Group", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("0 - Back to Main Menu ", ConsoleColor.Yellow);

                                    isSucceeded = int.TryParse(Console.ReadLine(), out number);
                                    if (!isSucceeded)
                                    {
                                        ConsoleHelper.WriteWithColor("The entered number is not a correct format!", ConsoleColor.Red);
                                    }

                                    switch (number)
                                    {
                                        case (int)StudentOptions.CreateStudent:
                                            _studentService.Create();
                                            break;

                                        case (int)StudentOptions.UpdateStudent:
                                            _studentService.Update();
                                            break;
                                        case (int)StudentOptions.DeleteStudent:
                                            _studentService.Delete();
                                            break;
                                        case (int)StudentOptions.GetAllStudents:
                                            _studentService.GetALl();
                                            break;
                                        case (int)StudentOptions.GetAllStudentsbyGroup:
                                            break;
                                        case (int)StudentOptions.BacktoMainMenu:
                                            goto MainMenuDesc;



                                    }
                                }

                            case (int)MainMenuOptions.Teachers:
                                while (true)
                                {
                                TeacherDescription: ConsoleHelper.WriteWithColor("1 - Create Teacher", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("2 - Update Teacher", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("3 - Delete Teacher", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("4 - Get All Teachers", ConsoleColor.Yellow);
                                    ConsoleHelper.WriteWithColor("0 - Back to Main Menu", ConsoleColor.Yellow);

                                    isSucceeded = int.TryParse(Console.ReadLine(), out number);
                                    if (!isSucceeded)
                                    {
                                        Console.WriteLine(" The entered number is not a correct format! ", ConsoleColor.Red);
                                        goto TeacherDescription;
                                    }
                                    else
                                    {
                                        switch (number)
                                        {
                                            case (int)TeacherOptions.CreateTeacher:
                                                _teacherService.Create();
                                                break;
                                            case (int)TeacherOptions.UpdateTeacher:
                                                _teacherService.Update();
                                                break;
                                            case (int)TeacherOptions.DeleteTeacher:
                                                _teacherService.Delete();
                                                break;
                                            case (int)TeacherOptions.GetAllTeachers:
                                                _teacherService.GetAll();
                                                break;
                                            case (int)TeacherOptions.BacktoMainMenu:
                                                goto MainMenuDesc;
                                        }
                                    }
                                }
                            case (int)MainMenuOptions.Logout:
                                goto AuthorizeDesc;
                            default:
                                ConsoleHelper.WriteWithColor("The entered number doesn't exist in the main menu!", ConsoleColor.Red);
                                break;
                        }
                    }
                                               
                }

            }

        }
    }

}

























