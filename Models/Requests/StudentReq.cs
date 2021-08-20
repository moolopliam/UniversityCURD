using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models.Requests
{
    public class StudentReq
    {
        [Required]
        [MaxLength(13)]
        public string StdId { get; set; }
        [Required(ErrorMessage = "กรุณาระบุ รหัสคำนำหน้าชื่อ")]
        [MaxLength(2)]
        public string TitleId { get; set; }
        [Required(ErrorMessage = "กรุณาระบุข้อมูล ชื่อ")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "กรุณาระบุ นามสกุล")]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "กรุณาระบุ วัน/เดือน/ปี เกิด")]
        public DateTime Birthay { get; set; }
        [Required(ErrorMessage = "กรุณาระบุ  เบอร์โทรศัพท์")]
        [MaxLength(10)]
        public string Phone { get; set; }
        [Required]
        [MaxLength(50)]
        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string Address { get; set; }
        [Required]
        [MaxLength(6)]
        public string DistrictId { get; set; }
        [Required]
        [MaxLength(4)]
        public string Amphur { get; set; }
        [Required]
        [MaxLength(2)]
        public string Province { get; set; }
        [Required(ErrorMessage = "กรุณาระบุ รหัสไปรณีย์={0}")]
        [MaxLength(5)]
        public string Postcode { get; set; }
        [Required]
        [MaxLength(25)]
        public string Username { get; set; }
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
    }
}



