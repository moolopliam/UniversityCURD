using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using University.Models.Data;
using University.Models.Requests;

namespace University.Models.Querys
{
    public class StudentQ
    {
        private readonly UniversityContext _dbContext = new();

        // แสดงรายชื่อนักศึกษาทั้งหทด
        public object GetStudenst(int pageSize = 10, int currentPage = 1, string search = "")
        {
            var query = _dbContext.Students.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(a => a.Name.Contains(search) || a.LastName.Contains(search));
            }

            int count = query.Count();
            var item = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            var result =  (from student in item
                           join title in _dbContext.Titels
                    on student.TitleId equals title.TitleId into data
                from v in data.DefaultIfEmpty()
                select new
                {
                    student.StdId,
                    student.TitleId,
                    TitleName = v == null ? null : v.TitleName,
                    student.Name,
                    student.LastName,
                    student.Birthay,
                    student.Phone,
                    student.Email,
                    student.Address,
                    student.DistrictId,
                    student.Amphur,
                    student.Province,
                    student.Username,
                    student.Postcode,
                });

            return new
            {
                StatusCode = count == 0 ? "001" : "002",
                Message = count == 0 ? "ไม่พบข้อมูล" : "พบข้อมูล",
                Pagin = new
                {
                    totlaPage = (int)Math.Ceiling((double)count / pageSize),
                    totalRow = count,
                    currentPage,
                    pageSize
                },
                Data = result
            };
        }

        public object SaveStudent(StudentReq values)
        {
            var checkStdId = _dbContext.Students.FirstOrDefault(a => a.StdId.Equals(values.StdId));
            //ตรวจสอบว่ามีนักศึกษารหัสนี้หรือยัง
            if (checkStdId != null)
            {
                return new
                {
                    StatusCode = "000",
                    Message = "มีรหัสนักศึกษานี้แล้ว"
                };
            }
            //ทำรหัสรหัสผ่าน 
            string salf = Guid.NewGuid().ToString();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(values.Password + salf);

            _dbContext.Add(new Student()
            {
                StdId = values.StdId,
                TitleId = values.TitleId,
                Name = values.Name,
                LastName = values.LastName,
                Birthay = values.Birthay.Date,
                Phone = values.Phone,
                Email = values.Email,
                Address = values.Address,
                DistrictId = values.DistrictId,
                Amphur = values.Amphur,
                Province = values.Province,
                Username = values.Username,
                Password = hashedPassword,
                Salt = salf,
                Postcode = values.Postcode

            });
            //บันทึกรายชื่อนักศึกษาลงฐานข้อมูล
            _dbContext.SaveChanges();

            return new
            {
                StatusCode = "003",
                Message = "บันทึกข้อมูลสำเร็จ"
            };

        }

        public object EditStudent(string stdID, StudentReq values)
        {
            var student = _dbContext.Students.AsNoTracking().FirstOrDefault(a => a.StdId == stdID);
            if (student == null)
            {
                return new
                {
                    StatusCode = "000",
                    Message = "ไม่พบข้อมูล"
                };
            }

            _dbContext.Entry(new Student
            {
                StdId = values.StdId,
                TitleId = values.TitleId,
                Name = values.Name,
                LastName = values.LastName,
                Birthay = values.Birthay.Date,
                Phone = values.Phone,
                Email = values.Email,
                Address = values.Address,
                DistrictId = values.DistrictId,
                Amphur = values.Amphur,
                Province = values.Province,
                Username = values.Username,
                Postcode = values.Postcode
            }).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return new
            {
                StatusCode = "003",
                Message = "บันทึกข้อมูลสำเร็จ"
            };
        }

        public object DetailStudent(string stdID)
        {
            var student = _dbContext.Students.AsNoTracking().FirstOrDefault(a => a.StdId == stdID);
            if (student == null)
            {
                return new
                {
                    StatusCode = "000",
                    Message = "ไม่พบข้อมูล"
                };
            }
            return new
            {
                StatusCode = "001",
                Message = "พบข้อมูล",
                Data = student
            };
        }

        public object RemoveStudent(string stdID)
        {
            var student = _dbContext.Students.AsNoTracking().FirstOrDefault(a => a.StdId == stdID);
            if (student == null)
            {
                return new
                {
                    StatusCode = "000",
                    Message = "ไม่พบข้อมูล"
                };
            }

            _dbContext.Students.Remove(student);
            _dbContext.SaveChanges();

            return new
            {
                StatusCode = "001",
                Message = "ลบข้อมูลสำเร็จ",
                Data = student
            };
        }

    }
}
