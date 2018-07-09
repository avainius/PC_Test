using DataAccess.Interfaces.IServices;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DataAccess.Services
{
    public class ProjectService : IProjectService
    {
        private List<Project> Projects => DataManager.Projects;
        private List<User> Users => DataManager.Users;

        public User AssignToUser(int userId, int projectId)
        {
            if (!Users.Any(c => c.Id == userId) || !Projects.Any(c => c.Id == projectId)) throw new Exception("Unable to assign project to requested user. Either user or project not found");
            var selectedUser = Users[Users.FindIndex(c => c.Id == userId)];
            if (selectedUser.Projects.Any(c => c.Id == projectId) == true) throw new Exception("Project already assigned to the user");
            if (selectedUser.Projects == null) selectedUser.Projects = new List<Project>();
            selectedUser.Projects.Add(Projects.First(c => c.Id == projectId));

            return selectedUser;
        }

        public void CreateOrUpdate(Project project)
        {
            if (Projects.Any(c => c.Id == project.Id))
            {
                Projects[Users.FindIndex(c => c.Id == project.Id)] = project;
            }
            else
            {
                if (Projects.Any()) project.Id = Projects.Max(c => c.Id) + 1;
                else project.Id = 1;
                Projects.Add(project);
            }
        }

        public void Delete(Project project)
        {
            if (DataManager.Projects.Any(c => c.Id == project.Id))
            {
                DataManager.Projects.RemoveAt(DataManager.Projects.FindIndex(c => c.Id == project.Id));
            }
            else
            {
                MessageBox.Show("User does not exist");
            }
        }
    }
}
