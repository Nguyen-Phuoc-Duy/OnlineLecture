﻿using Microsoft.AspNetCore.Hosting;
using OnlineLecture.Models.Domain;
using OnlineLecture.Models.DTO;
using OnlineLecture.Repositories.Abstract;

namespace OnlineLecture.Repositories.Implementation
{
    public class LectureService : ILectureService
    {
        private readonly DatabaseContext context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LectureService(DatabaseContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this._webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> AddLecture(LectureModel model, IFormFile mFile)
        {
            try
            {
                if (model.Description == null)
                    model.Description = "";

                if (mFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "lecture");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(mFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await mFile.CopyToAsync(fileStream);
                    }

                    model.FileLecture = "/lecture/" + uniqueFileName;
                }
                else
                {
                    return false;
                }

                context.LectureModel.Add(model);
                context.SaveChanges();
                foreach(int subjectId in model.Subjects)
                {
                    var lectureSubject = new SubjectLectureModel
                    {
                        IdLecture = model.IdLecture,
                        IdSubject = subjectId
                    };
                    context.SubjectLectureModel.Add(lectureSubject);
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateLecture(LectureModel model, IFormFile mFile)
        {
            try
            {
                if (model.Description == null)
                    model.Description = "";

                if (mFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "lecture");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(mFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await mFile.CopyToAsync(fileStream);
                    }

                    model.FileLecture = "/lecture/" + uniqueFileName;
                }
                else
                {
                    return false;
                }
                context.LectureModel.Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public LectureModel FindById(int id)
        {
            return context.LectureModel.Find(id);
        }

        public bool DeleteLecture(int idLecture)
        {
            try
            {
                var data = this.FindById(idLecture);
                if (data == null)
                    return false;

                context.LectureModel.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        LectureModel ILectureService.FindById(int id)
        {
            return context.LectureModel.Find(id);
        }

        public IEnumerable<LectureModel> GetAll()
        {
            return context.LectureModel.ToList();
        }

        public LectureListVm FilterList()
        {
            var list = context.LectureModel.AsQueryable();
            var data = new LectureListVm
            {
                LectureList = list
            };
            return data;

        }
    }
}
