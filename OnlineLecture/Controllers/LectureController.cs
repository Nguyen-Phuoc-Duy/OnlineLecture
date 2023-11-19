﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineLecture.Models.DTO;
using OnlineLecture.Repositories.Abstract;

namespace OnlineLecture.Controllers
{
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
            model.SubjectList = _subjectService.GetAll().Select(a => new SelectListItem
            {
                Text = a.NameSubject,
                Value = a.IdSubject.ToString()
            });
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddLecture(LectureModel model, IFormFile mFile)
        {
            model.SubjectList = _subjectService.GetAll().Select(a => new SelectListItem
            {
                Text = a.NameSubject,
                Value = a.IdSubject.ToString()
            });
            bool res = await _lectureService.AddLecture(model, mFile);
            if (res)
            {
               /* return View(model);*/
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
            return View(record);
        }

        [HttpPost]
        public async Task<IActionResult> Update(LectureModel model, IFormFile mFile)
        {
            bool res = await _lectureService.UpdateLecture(model, mFile);
            if (res)
            {
                /* return View(model);*/
                TempData["msg"] = "Updated successfully";
                return RedirectToAction(nameof(GetAll));
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
            return RedirectToAction("GetAll");
        }

        public IActionResult GetAll()
        {
            var data = _lectureService.GetAll();
            return View(data);
        }
    }
}