using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace FingerEnglishWebServer.Controllers
{
    public class DomainController : Controller
    {



        [HttpGet]
        public JsonResult GetGradeList()
        {
            var enums = Enum.GetValues(typeof(Grade));
            List<GradeRpModel> grades = new List<GradeRpModel>();

            foreach (var en in enums)
            {
                var grade = new GradeRpModel(){ 

                    Id = (int) en  ,
                    Name = EnumHelper.GetEnumDescription(en)
                };
                grades.Add(grade);
            }
            return Json(grades.ToArray());
        }


        [HttpGet]
        public JsonResult GetSexList()
        {
            var enums = Enum.GetValues(typeof(Sex));
            List<SexRpModel> sexs = new List<SexRpModel>();

            foreach (var en in enums)
            {
                var grade = new SexRpModel(){ 

                    Id = (int) en  ,
                    Name = EnumHelper.GetEnumDescription(en)
                };
                sexs.Add(grade);
            }
            return Json(sexs.ToArray());
        }


        [HttpGet]
        public JsonResult GetSchoolLevelList()
        {
            var enums = Enum.GetValues(typeof(SchoolLevel));
            List<SchoolLevelRpModel> scs = new List<SchoolLevelRpModel>();

            foreach (var en in enums)
            {
                var grade = new SchoolLevelRpModel(){ 

                    Id = (int) en  ,
                    Name = EnumHelper.GetEnumDescription(en)
                };
                scs.Add(grade);
            }
            return Json(scs.ToArray());
        }

    }

}