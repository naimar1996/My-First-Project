using Core.Entities;
using Core.Helper;
using Data.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Presentation1.Services
{
    public class GroupService
    {
        private readonly GroupRepository _groupRepository;
        private readonly StudentRepository _studentRepository;
        private readonly TeacherRepository _teacherRepository;
        public GroupService()
        {
            _groupRepository = new GroupRepository();
            _studentRepository = new StudentRepository();
            _teacherRepository= new TeacherRepository();
        }

        public void GetAll()
        {

            var groups = _groupRepository.GetAll();

            ConsoleHelper.WriteWithColor("---All Groups---", ConsoleColor.Cyan);

            foreach (var group in groups)
            {
                ConsoleHelper.WriteWithColor($"Id: {group.Id},Name:{group.Name},Max size : {group.MaxSize},Start date: {group.StartDate.ToShortDateString()},End date : {group.EndDate.ToShortDateString()}, Created by: {group.CreatedBy}", ConsoleColor.DarkYellow);
            }
        }
        public void GetAllGroupsbyTeacher()
        {
           var teachers= _teacherRepository.GetAll();
            foreach (var teacher in teachers)
            {
                ConsoleHelper.WriteWithColor($" Id: {teacher.Id},Fullname: {teacher.Name} {teacher.Surname}");

            }
          IDDesc: ConsoleHelper.WriteWithColor(" Enter teacher's ID,please",ConsoleColor.Cyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if( !isSucceeded )
            {
                ConsoleHelper.WriteWithColor(" The entered ID is not a correct format!", ConsoleColor.Red);
                goto IDDesc;
            }
            var dbTeacher = _teacherRepository.Get(id);
            if( dbTeacher == null )
            {
                ConsoleHelper.WriteWithColor(" This id doesn't contain any teacher!", ConsoleColor.Red);
            }
            else
            {
                foreach(var group in dbTeacher.Groups)
                {
                    ConsoleHelper.WriteWithColor($"ID: {group.Id},Name: {group.Name}",ConsoleColor.Green);
                }
            }

        }
        public void GetGroupbyID(Admin admin)
        {
            var groups = _groupRepository.GetAll();
            if (groups.Count == 0)
            {
            AreyousureDesc: ConsoleHelper.WriteWithColor(" Do you want to creat a new group? ", ConsoleColor.DarkYellow);
                char decision;
                bool isSucceed = char.TryParse(Console.ReadLine(), out decision);
                if (!isSucceed)
                {
                    ConsoleHelper.WriteWithColor("Your choice is not a correct format!", ConsoleColor.Red);
                }
                if (!(decision == 'a' || decision == 'b'))
                {
                    ConsoleHelper.WriteWithColor(" Your choice is not correct!", ConsoleColor.Red);
                    goto AreyousureDesc;
                }
                if (decision == 'a')
                {
                    Create(admin);
                }
                else
                {

                EnterIDDesc: ConsoleHelper.WriteWithColor("--- Enter ID,please---", ConsoleColor.Cyan);
                    int id;
                    bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
                    if (isSucceeded)
                    {
                        ConsoleHelper.WriteWithColor("The entered ID is incorrect!", ConsoleColor.Red);
                        goto EnterIDDesc;
                    }
                    var group = _groupRepository.Get(id);
                    if (group == null)
                    {
                        ConsoleHelper.WriteWithColor("There is not any group in this ID!", ConsoleColor.Red);
                    }
                    ConsoleHelper.WriteWithColor($"Id: {group.Id},Name:{group.Name},Max size : {group.MaxSize},Start date: {group.StartDate.ToShortDateString()},End date : {group.EndDate.ToShortDateString()}, Created by: {group.CreatedBy}", ConsoleColor.DarkYellow);
                }
            }

        }
        public void GetGroupbyName()
        {
            GetAll();
            ConsoleHelper.WriteWithColor(" Enter a name of group,please", ConsoleColor.Cyan);
            string name = Console.ReadLine();
            var group = _groupRepository.GetbyName(name);
            if (group == null)
            {
                ConsoleHelper.WriteWithColor("There is not any group in this name", ConsoleColor.Red);
            }
            ConsoleHelper.WriteWithColor($"Id: {group.Id},Name:{group.Name},Max size : {group.MaxSize},Start date: {group.StartDate.ToShortDateString()},End date : {group.EndDate.ToShortDateString()}", ConsoleColor.DarkYellow);
        }
        public void GetUpdate()
        {
            GetAll();
        IDorNameDesc: ConsoleHelper.WriteWithColor(" Which do you want to enter? \n a)ID  \n b)Name ");
            char decision;
            bool isSucceeded = char.TryParse(Console.ReadLine(), out decision);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Your choice is not a correct format!", ConsoleColor.Red);
                goto IDorNameDesc;
            }
            if (!(decision == 'a' || decision == 'b'))
            {
                ConsoleHelper.WriteWithColor(" The entered letter is not correct", ConsoleColor.Red);
            }
            if (decision == 'a')
            {
            IDDesc: ConsoleHelper.WriteWithColor(" Enter an ID of the group", ConsoleColor.Cyan);
                char id;
                isSucceeded = char.TryParse(Console.ReadLine(), out id);

                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("The entered ID is incorrect!", ConsoleColor.Red);
                    goto IDDesc;
                }

                var group = _groupRepository.Get(id);
                if (group == null)
                {
                    ConsoleHelper.WriteWithColor(" There is not any group in this ID!", ConsoleColor.Red);
                }
                InternalUpdate(group);

            }
                else
            {
            NameDesc: ConsoleHelper.WriteWithColor("Enter a name of the group,please", ConsoleColor.Cyan);
                string name = Console.ReadLine();
                var group = _groupRepository.GetbyName(name);
                if (group == null)
                {
                    ConsoleHelper.WriteWithColor(" There is not any group in this Name!", ConsoleColor.Red);
                }
                InternalUpdate(group);

            }

        }
        private void InternalUpdate(Group group)
        {
                ConsoleHelper.WriteWithColor(" Enter new name,please", ConsoleColor.Cyan);
               string name = Console.ReadLine();

            maxSizeDesc: int maxSize;
              bool  isSucceeded = int.TryParse(Console.ReadLine(), out maxSize);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("The entered is not a correct format", ConsoleColor.Red);
                    goto maxSizeDesc;
                }

            StartDateDes: ConsoleHelper.WriteWithColor("Enter  new start date,please", ConsoleColor.Cyan);
                DateTime startDate;
                isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("The start date is not a correct format!", ConsoleColor.Red);
                    goto StartDateDes;
                }
            EndDateDes: ConsoleHelper.WriteWithColor("Enter new end date,please", ConsoleColor.Cyan);
                DateTime endDate;
                isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("The end date is not a correct format!", ConsoleColor.Red);
                    goto EndDateDes;
                }
                if (startDate > endDate)
                {
                    ConsoleHelper.WriteWithColor("The end date must be bigger than the start date", ConsoleColor.Red);
                    goto EndDateDes;
                }
                group.Name = name;
                group.MaxSize = maxSize;
                group.StartDate = startDate;
                group.EndDate = endDate;
                _groupRepository.Update(group);
        }
        public void Create(Admin admin)
        {
            if(_teacherRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor(" Create a teacher firstly,please!",ConsoleColor.Red);
            }
            else
            {

            ConsoleHelper.WriteWithColor("---Enter name, please---", ConsoleColor.Cyan);
            string name = Console.ReadLine();

        MaxSizeDes: ConsoleHelper.WriteWithColor("---Enter group max - size,please---", ConsoleColor.Cyan);
            int maxSize;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out maxSize);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Max size is not a correct format!", ConsoleColor.Red);
                goto MaxSizeDes;
            }
            if (maxSize > 18)
            {
                ConsoleHelper.WriteWithColor("Max size must be less than or equal to 18", ConsoleColor.Red);
                goto MaxSizeDes;
            }

        StartDateDes: ConsoleHelper.WriteWithColor("Enter start date,please", ConsoleColor.Cyan);
            DateTime startDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("The start date is not a correct format!", ConsoleColor.Red);
                goto StartDateDes;
            }
        EndDateDes: ConsoleHelper.WriteWithColor("Enter end date,please", ConsoleColor.Cyan);
            DateTime endDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("The end date is not a correct format!", ConsoleColor.Red);
                goto EndDateDes;
            }
            if (startDate > endDate)
            {
                ConsoleHelper.WriteWithColor("The end date must be bigger than the start date", ConsoleColor.Red);
                goto EndDateDes;
            }

            var teachers = _teacherRepository.GetAll();
            foreach(var teacher in teachers)
            {
                ConsoleHelper.WriteWithColor($" Id: {teacher.Id},Fullname: {teacher.Name} {teacher.Surname}");

            }
              TeacherIdDesc: ConsoleHelper.WriteWithColor(" Enter teacher's ID,please", ConsoleColor.Green);
                int teacherId;
                isSucceeded = int.TryParse(Console.ReadLine(), out teacherId);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor(" The entered teacher's Id is not a correct format",ConsoleColor.Red);
                    goto TeacherIdDesc;
                }
                var dbTeacher = _teacherRepository.Get(teacherId);
                if (dbTeacher == null)
                {
                    ConsoleHelper.WriteWithColor("This ID doesn't contain any teacher", ConsoleColor.Red);
                }

            var group = new Group
            {
                Name = name,
                MaxSize = maxSize,
                StartDate = startDate,
                EndDate = endDate,
                Teacher =dbTeacher,
                CreatedBy = admin.Username
            };
                dbTeacher.Groups.Add(group);

            _groupRepository.Add(group);

            ConsoleHelper.WriteWithColor($"The group succesfully created with Name:{group.Name},Max size : {group.MaxSize},Start date: {group.StartDate.ToShortDateString()},End date : {group.EndDate.ToShortDateString()}", ConsoleColor.DarkYellow);
        }

            }
       
        public void Delete()
        {
            GetAll();

        IdDes: ConsoleHelper.WriteWithColor("Enter group id,please", ConsoleColor.Cyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("The ID is not a correct format!", ConsoleColor.Red);
                goto IdDes;
            }
            var dbGroup = _groupRepository.Get(id);
            if (dbGroup == null)
            {
                ConsoleHelper.WriteWithColor("There is not any in this ID!", ConsoleColor.Red);
            }
            else
            {
                _groupRepository.Delete(dbGroup);
                ConsoleHelper.WriteWithColor("The group successfully deleted!", ConsoleColor.Green);
            }

        }
       
    }
}


       






