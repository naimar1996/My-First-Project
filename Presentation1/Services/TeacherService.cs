using Core.Helper;
using Data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Data.Repositories.Concrete;



namespace Presentation1.Services
{
    public class TeacherService
    {
        private readonly TeacherRepository _teacherRepository;
        public TeacherService()
        {
            _teacherRepository = new TeacherRepository();
        }

        public void GetAll()
        {
            var teachers = _teacherRepository.GetAll();
            if (teachers.Count == 0)
            {
                ConsoleHelper.WriteWithColor(" There is not any teacher!", ConsoleColor.Red);
            }

            foreach (var teacher in teachers)
            {
                ConsoleHelper.WriteWithColor($"Id: {teacher.Id}, Fullname: {teacher.Name} {teacher.Surname},BirthDate: {teacher.BirthDate},Speciality: {teacher.Speciality}", ConsoleColor.DarkYellow);

                if (teacher.Groups.Count == 0)
                {
                    ConsoleHelper.WriteWithColor(" This teacher has not any group!", ConsoleColor.Red);
                }
                foreach(var group in teacher.Groups)
                {
                    ConsoleHelper.WriteWithColor($"Id: {group.Id}, Name: {group.Name}", ConsoleColor.DarkYellow);
                }
                Console.WriteLine();


            }






        }
        public void Create()
        {
            ConsoleHelper.WriteWithColor(" Enter teacher's name,please", ConsoleColor.Cyan);
            string name = Console.ReadLine();

            ConsoleHelper.WriteWithColor(" Enter teacher's surname,please", ConsoleColor.Cyan);
            string surname = Console.ReadLine();

        BirthDateDesc: ConsoleHelper.WriteWithColor(" Enter teacher's birth date,please", ConsoleColor.Cyan);
            DateTime birthDate;
            bool isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("The birth date is not a correct format!", ConsoleColor.Red);
                goto BirthDateDesc;
            }

            ConsoleHelper.WriteWithColor(" Enter teacher's speciality,please", ConsoleColor.Cyan);
            string speciality = Console.ReadLine();

            var teacher = new Teacher()
            {
                Name = name,
                Surname = surname,
                BirthDate = birthDate,
                Speciality = speciality,
                CreatedAt = DateTime.Now,
            };
            _teacherRepository.Add(teacher);
            string teacherBirthDate = teacher.BirthDate.ToString("ddddd, dd MM yyyy");
            ConsoleHelper.WriteWithColor($"FullName: {teacher.Name} {teacher.Surname}, Birth date: {teacherBirthDate},Speciality: {teacher.Speciality}, ", ConsoleColor.Green);
        }
        public void Update()
        {
            GetAll();
        IdDesc: ConsoleHelper.WriteWithColor(" Enter teacher's id,please", ConsoleColor.Cyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor(" The entered ID is not a correct format!", ConsoleColor.Red);
                goto IdDesc;
            }
            var teacher = _teacherRepository.Get(id);
            if (teacher is null)
            {
                ConsoleHelper.WriteWithColor(" The entered ID doesn't contain any teacher!", ConsoleColor.Red);
            }
            _teacherRepository.Update(teacher);

            ConsoleHelper.WriteWithColor(" Enter a new teacher's name,please", ConsoleColor.Cyan);
            string name = Console.ReadLine();

            ConsoleHelper.WriteWithColor(" Enter a new teacher's surname,please", ConsoleColor.Cyan);
            string surname = Console.ReadLine();

        BirthDateDesc: ConsoleHelper.WriteWithColor(" Enter a new teacher's birth date,please", ConsoleColor.Cyan);
            DateTime birthDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("The birth date is not a correct format!", ConsoleColor.Red);
                goto BirthDateDesc;
            }

            ConsoleHelper.WriteWithColor(" Enter a new teacher's speciality,please", ConsoleColor.Cyan);
            string speciality = Console.ReadLine();

            teacher.Name = name;
            teacher.Surname = surname;
            teacher.BirthDate = birthDate;
            teacher.Speciality = speciality;

            _teacherRepository.Update(teacher);


        }
        public void Delete()
        {
            GetAll();
            if(_teacherRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor(" There is not any teacher!", ConsoleColor.Red);
            }
            else
            {
        EnterIDDesc: ConsoleHelper.WriteWithColor(" Enter student's id,please", ConsoleColor.Cyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor(" The entered ID is an incorrect format!", ConsoleColor.Red);
                goto EnterIDDesc;
            }
            var teacher = _teacherRepository.Get(id);
            if (teacher == null)
            {
                ConsoleHelper.WriteWithColor(" The entered id doesn't contain any teacher!", ConsoleColor.Red);
            }
            _teacherRepository.Delete(teacher);
            ConsoleHelper.WriteWithColor("The teacher is successfully deleted", ConsoleColor.Green);
            }


        }



    }
}
