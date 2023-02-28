using Core.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Concrete
{
    public class StudentRepository : IStudentRepository
    {
        static int id;
        public List<Student> GetAll()
        {
            return DbContext.Students;
        }
        public Student Get(int id)
        {
            return DbContext.Students.FirstOrDefault(x => x.Id == id);
        }
        public Student GetbyName(string name)
        {
            return DbContext.Students.FirstOrDefault(d => d.Name == name);
        }

        public void Add(Student student)
        {
            id++;
            student.Id = id;
            DbContext.Students.Add(student);
        }
        public void Update(Student student)
        {
            var dbStudent = DbContext.Students.FirstOrDefault(s => s.Id == student.Id);
            if (dbStudent != null)
            {
                dbStudent.Name = student.Name;
                dbStudent.Surname = student.Surname;
                dbStudent.BirthDate = student.BirthDate;
                dbStudent.Id = student.Id;

            }
        }
        public void Delete(Student student)
        {
            DbContext.Students.Remove(student);
        }
        public bool IsDublicatedEmail(string email)
        {
            return DbContext.Students.Any(d => d.Email == email);
        }

    }
}


