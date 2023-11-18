using Microsoft.AspNetCore.Mvc;
using OnlineLecture.Models.DTO;
using OnlineLecture.Repositories.Abstract;

namespace OnlineLecture.Controllers
{
    public class LectureController : Controller
    {
        private readonly ILectureService _lectureService;

        public LectureController(ILectureService service)
        {
            this._lectureService = service;
        }

        public IActionResult AddLecture()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddLecture(LectureModel model, IFormFile mFile)
        {
            bool res = await _lectureService.AddLecture(model, mFile);
            if (res)
            {
                return View(model);
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
        public IActionResult Update(LectureModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var res = _lectureService.UpdateLecture(model);
            if (res)
            {
                TempData["msg"] = "Updated successfully";
                return RedirectToAction(nameof(AddLecture));
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
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
