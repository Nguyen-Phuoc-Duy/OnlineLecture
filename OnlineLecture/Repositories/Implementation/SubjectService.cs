using OnlineLecture.Models.Domain;
using OnlineLecture.Models.DTO;
using OnlineLecture.Repositories.Abstract;

namespace OnlineLecture.Repositories.Implementation
{
    public class SubjectService : ISubjectService
    {
        private readonly DatabaseContext context;

        public SubjectService(DatabaseContext context)
        {
            this.context = context;
        }

        public bool AddSubject(SubjectModel model)
        {
            try
            {
                context.SubjectModel.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteSubject(int idSubject)
        {
            try
            {
                var data = this.FindById(idSubject);
                if (data != null)
                    return false;
                
                context.SubjectModel.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public SubjectModel FindById(int id)
        {
           return context.SubjectModel.Find(id);
        }

        IEnumerable<SubjectModel> ISubjectService.GetAll()
        {
            return context.SubjectModel.ToList();
        }

        public bool UpdateSubject(SubjectModel model)
        {
            try
            {
                context.SubjectModel.Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
