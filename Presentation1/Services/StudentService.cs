using Core.Constants;
using Core.Entities;
using Core.Extensions;
using Core.Helper;
using Data.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation1.Services
{
    public class StudentService
    {
        private readonly StudentRepository _studentRepository;
        private readonly GroupRepository _groupRepository;
        public StudentService()
        {
            _studentRepository = new StudentRepository();
            _groupRepository = new GroupRepository();
        }
        public void GetALl()
        {
            var students = _studentRepository.GetAll();
            ConsoleHelper.WriteWithColor(" --- All Students --- ", ConsoleColor.Cyan);
            foreach (var student in students)
            {
                ConsoleHelper.WriteWithColor($"Id:{student.Id}, Name: {student.Name}, Surname: {student.Surname}, Birth Date: {student.BirthDate.ToShortDateString()},Email: {student.Email}", ConsoleColor.DarkYellow);
            }
        }
        public void GetStudentbyName()
        {
            GetALl();
            ConsoleHelper.WriteWithColor(" Enter an ID,please", ConsoleColor.Cyan);
            string name = Console.ReadLine();
            var student = _studentRepository.GetbyName(name);
            if (student == null)
            {
                ConsoleHelper.WriteWithColor(" There is not any student in this name", ConsoleColor.Red);
            }
            ConsoleHelper.WriteWithColor($"ID: {student.Id},Name: {student.Name},Surname: {student.Surname}, BirthDate: {student.BirthDate}, Email: {student.Email}", ConsoleColor.DarkYellow);

        }
        public void Create()
        {
            ConsoleHelper.WriteWithColor(" Enter student's name,please", ConsoleColor.Cyan);
            string name = Console.ReadLine();

            ConsoleHelper.WriteWithColor(" Enter student's surname,please", ConsoleColor.Cyan);
            string surname = Console.ReadLine();

        BirthDateDesc: ConsoleHelper.WriteWithColor(" Enter student birth date,please", ConsoleColor.Cyan);
            DateTime birthDate;
            bool isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("The birth date is not a correct format!", ConsoleColor.Red);
                goto BirthDateDesc;
            }

        EmailDesc: ConsoleHelper.WriteWithColor(" Enter an email,please ", ConsoleColor.Cyan);
            string email = Console.ReadLine();
            if (!email.IsEmail())
            {
                ConsoleHelper.WriteWithColor(" The entered email is not a correct format", ConsoleColor.Red);
                goto EmailDesc;
            }
            if(_studentRepository.IsDublicatedEmail(email))
            {
                ConsoleHelper.WriteWithColor(" The entered email is alread used!", ConsoleColor.Red);
                goto EmailDesc;
            }

        GroupDescription: _groupRepository.GetAll();
            ConsoleHelper.WriteWithColor(" Enter Group ID,please", ConsoleColor.Cyan);
            int groupId;
            isSucceeded = int.TryParse(Console.ReadLine(), out groupId);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor(" The entered Group Id is not a correct format!", ConsoleColor.Red);
                goto GroupDescription;
            }
            var group = _groupRepository.Get(groupId);
            if (group == null)
            {
                ConsoleHelper.WriteWithColor(" Group doesn't exist in this id!", ConsoleColor.Red);
                goto GroupDescription;
            }

            if(group.MaxSize == group.Students.Count)
            {
                ConsoleHelper.WriteWithColor(" This group is already full!",ConsoleColor.Red);
                goto GroupDescription;
            }

            var student = new Student()
            {
                Name = name,
                Surname = surname,
                BirthDate = birthDate,
                Email = email,
                Group = group,
                GroupId = group.Id,
            };
            group.Students.Add(student);
            _studentRepository.Add(student);

            _studentRepository.Add(student);
            ConsoleHelper.WriteWithColor($"{student.Name}, {student.Surname},{student.BirthDate},{student.Email}, {student.Email}, {student.Group}, {student.GroupId} is seccessfully added", ConsoleColor.Green);
        }
        public void Update()
        {
            GetALl();
            IDDesc: ConsoleHelper.WriteWithColor(" Enter an ID of the group", ConsoleColor.Cyan);
                int id;
                bool isSuccessful = int.TryParse(Console.ReadLine(), out id);

                if (!isSuccessful)
                {
                    ConsoleHelper.WriteWithColor("The entered ID is incorrect!", ConsoleColor.Red);
                    goto IDDesc;
                }

                var group = _groupRepository.Get(id);
                if (group == null)
                {
                    ConsoleHelper.WriteWithColor(" There is not any group in this ID!", ConsoleColor.Red);
                }

                ConsoleHelper.WriteWithColor(" Enter new student's name,please", ConsoleColor.Cyan);
                string name = Console.ReadLine();
                ConsoleHelper.WriteWithColor(" Enter new student's surname,please", ConsoleColor.Cyan);
                string surname = Console.ReadLine();
            BirthDateDesc: ConsoleHelper.WriteWithColor(" Enter new student's birthdate,please", ConsoleColor.Cyan);
                DateTime birthDate;
                bool isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
                if (isSucceeded)
                {
                    ConsoleHelper.WriteWithColor(" The entered birthday is not a correct format!", ConsoleColor.Red);
                    goto BirthDateDesc;
                }

            }
        public void Delete()
        {
            GetALl();
        IDDesc: ConsoleHelper.WriteWithColor(" Enter student's id,please ", ConsoleColor.Cyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor(" The entered ID is an incorrect format!", ConsoleColor.Red);
                goto IDDesc;
            }
            var dbStudent = _studentRepository.Get(id);
            if (dbStudent == null)
            {
                ConsoleHelper.WriteWithColor(" The entered ID doesn't contain any student! ", ConsoleColor.Red);
            }
            _studentRepository.Delete(dbStudent);
            ConsoleHelper.WriteWithColor(" The student is successfully deleted", ConsoleColor.Green);
        }



    }
}
