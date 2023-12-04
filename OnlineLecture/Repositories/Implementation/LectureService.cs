using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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
                foreach (int subjectId in model.Subjects)
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

                var removeSubject = context.SubjectLectureModel.Where(x => x.IdSubject == data.IdSubject);
                foreach (var item in removeSubject)
                {
                    context.SubjectLectureModel.Remove(item);
                }
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
            var list = context.LectureModel.ToList();
            foreach (var lt in list)
            {
                var subjects = (from subject in context.SubjectModel
                                join sl in context.LectureModel
                                on subject.IdSubject equals sl.IdSubject
                                where sl.IdLecture == lt.IdLecture
                                select subject.NameSubject).ToList();
                var subjectName = string.Join(",", subjects);
                lt.SubjectNames = subjectName;
            }
            var data = new LectureListVm
            {
                LectureList = list.AsQueryable()
            };
            return data;

        }

        public List<int> GetSubjectByLectureId(int idLecture)
        {
            var subjectIds = context.SubjectLectureModel.Where(a => a.IdLecture == idLecture).Select(a => a.IdSubject).ToList();
            return subjectIds;
        }

        public async Task<LectureModel?> GetByIdAsync(int id)
        {
            return await context.LectureModel.FirstOrDefaultAsync(a => a.IdLecture == id);
        }

        public async Task<List<LectureModel>> GetAllAsync()
        {
            return await context.LectureModel.ToListAsync();
        }

    }
}
