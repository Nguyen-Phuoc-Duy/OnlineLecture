using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineLecture.Models.Domain;
using OnlineLecture.Models.DTO;
using OnlineLecture.Repositories.Abstract;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineLecture.Controllers
{
    [Authorize(Roles = "admin")]
    public class LectureController : Controller
    {
        private readonly ILectureService _lectureService;
        private readonly ISubjectService _subjectService;

        public LectureController(ILectureService service, ISubjectService subjectService)
        {
            this._lectureService = service;
            this._subjectService = subjectService;
        }

        public IActionResult AddLecture()
        {
            var model = new LectureModel();
            model.SubjectList = _subjectService.GetAll().Select(a =>
            new SelectListItem { Text = a.NameSubject, Value = a.IdSubject.ToString() });
            /*ViewData["SubjectList"] = new SelectList(_subjectService.GetAll(), "IdSubject", "NameSubject");*/
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddLecture(LectureModel model, IFormFile mFile)
        {
            model.SubjectList = _subjectService.GetAll().Select(a =>
           new SelectListItem { Text = a.NameSubject, Value = a.IdSubject.ToString() });
            bool res = await _lectureService.AddLecture(model, mFile);
            if (res)
            {

                TempData["msg"] = "Added successfully";
                return RedirectToAction(nameof(AddLecture));
            }
            else
            {
                TempData["msg"] = "Error has occured on server side";
                return View(model);
            }
        }
        public IActionResult Update(int id)
        {
            var record = _lectureService.FindById(id);
            record.SubjectList = _subjectService.GetAll().Select(a =>
           new SelectListItem { Text = a.NameSubject, Value = a.IdSubject.ToString() });
            var selectedSubjects = _lectureService.GetSubjectByLectureId(record.IdLecture);
            MultiSelectList multiSelectList = new MultiSelectList(_subjectService.GetAll(), "IdSubject", "SubjectNames", selectedSubjects);
            //ViewData["SubjectList"] = new SelectList(_subjectService.GetAll(), "IdSubject", "NameSubject");
            record.MultiSubjectList = multiSelectList;
            return View(record);
        }

        [HttpPost]
        public async Task<IActionResult> Update(LectureModel model, IFormFile mFile)
        {
            model.SubjectList = _subjectService.GetAll().Select(a =>
          new SelectListItem { Text = a.NameSubject, Value = a.IdSubject.ToString() });
            bool res = await _lectureService.UpdateLecture(model, mFile);
            if (res)
            {
                /* return View(model);*/
                TempData["msg"] = "Updated successfully";
                return RedirectToAction(nameof(FilterList));
            }
            else
            {
                TempData["msg"] = "Error has occured on server side";
                return View(model);
            }
        }


        public IActionResult Delete(int id)
        {
            var res = _lectureService.DeleteLecture(id);
            return Ok(res);
        }

        public IActionResult GetAll()
        {
            /*var data = _lectureService.GetAll();*/
            return View();
        }

        public IActionResult FilterList()
        {
            var data = this._lectureService.FilterList();
            return View(data);
        }

        public IActionResult DetailSubject()
        {
            var data = this._lectureService.FilterList();
            return View(data);
        }

        public async Task<IActionResult> GetAllLecture()
        {
            if (!ModelState.IsValid) { return View(); }

            var data = await _lectureService.GetAllAsync();
            return Ok(data);
        }


    }
}
