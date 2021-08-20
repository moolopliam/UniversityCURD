using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.Models.Querys;
using University.Models.Requests;
using University.Models.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace University.Controllers
{
    /// <summary>
    /// จัดการข้อมูลนักศึกษา
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private StudentQ _student = new StudentQ();

        /// <summary>
        /// แสดงรายชื่อนักศึกษา
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetStudents")]
        public IActionResult GetStudents(int pageSize = 10, int currentPage = 1, string search = "")
        {
            try
            {
                var result = _student.GetStudenst(pageSize, currentPage, search);
                return StatusCode(200, result);
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    StatusCode = "000",
                    e.Message
                });
            }
        }

        /// <summary>
        ///  เพิ่มรายชื่อนักศึกษา
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        [HttpPost("SaveStudent")]
        public IActionResult SaveStudent([FromBody]StudentReq values)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return StatusCode(200,_student.SaveStudent(values));

                }
                return StatusCode(200,new
                {
                    StatusCode = "000",
                    Message = "inValid"
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    StatusCode = "000",
                    e.Message
                });
            }
        }
        /// <summary>
        /// แก้ไขข้อมูลนักศึกษา
        /// </summary>
        /// <param name="stdID"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        [HttpPut("EditStudent")]
        public IActionResult EditStudent(string stdID, [FromBody] StudentReq values)
        {
            try
            {
                return StatusCode(200, _student.EditStudent(stdID, values));
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    StatusCode = "000",
                    e.Message
                });
            }
        }

        /// <summary>
        /// แสดงรายละเอียดนักศึกษา
        /// </summary>
        /// <param name="stdID"></param>
        /// <returns></returns>
        [HttpGet("DetailStudent/{stdID}")]
        public IActionResult DetailStudent(string stdID)
        {
            try
            {
                return StatusCode(200, _student.DetailStudent(stdID));
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    StatusCode = "000",
                    e.Message
                });
            }
        }

        /// <summary>
        /// ลบข้อมูลนักศึกษา
        /// </summary>
        /// <param name="stdID"></param>
        /// <returns></returns>
        [HttpDelete("RemoveStudent/{stdID}")]
        public IActionResult RemoveStudent(string stdID)
        {
            try
            {
                return StatusCode(200, _student.RemoveStudent(stdID));
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    StatusCode = "000",
                    e.Message
                });
            }
        }
    }
}
