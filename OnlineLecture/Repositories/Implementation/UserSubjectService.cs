using OnlineLecture.Models.Domain;
using OnlineLecture.Models.DTO;
using OnlineLecture.Repositories.Abstract;

namespace OnlineLecture.Repositories.Implementation
{
    public class UserSubjectService : IUserSubject
    {
        private readonly DatabaseContext context;

        public UserSubjectService(DatabaseContext context)
        {
            this.context = context;
        }

        public UserSubjectModel FindById(int id)
        {
            return context.UserSubjectModel.Find(id);
        }

        IEnumerable<UserSubjectModel> IUserSubject.GetAll()
        {
            return context.UserSubjectModel.ToList();
        }

        public bool UpdateUserSubject(UserSubjectModel model)
        {
            try
            {
                context.UserSubjectModel.Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddUserSubject(UserSubjectModel model)
        {
            try
            {
                context.UserSubjectModel.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteUserSubject(int idUserSubject)
        {
            try
            {
                var data = this.FindById(idUserSubject);
                if (data == null)
                    return false;

                context.UserSubjectModel.Remove(data);
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
