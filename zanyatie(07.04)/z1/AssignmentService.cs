using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using z1.Models;

namespace z1.Services
{
    public class AssignmentService
    {
        private readonly string _filePath = "assignments.json";

        public async Task<ObservableCollection<AssignmentModel>> LoadAssignmentsAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new ObservableCollection<AssignmentModel>();
            }

            var json = await File.ReadAllTextAsync(_filePath);
            var assignments = JsonSerializer.Deserialize<ObservableCollection<AssignmentModel>>(json);
            return assignments ?? new ObservableCollection<AssignmentModel>();
        }

        public async Task SaveAssignmentsAsync(ObservableCollection<AssignmentModel> assignments)
        {
            var json = JsonSerializer.Serialize(assignments, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }

        public void AddAssignment(ObservableCollection<AssignmentModel> assignments, string subject, string title, string description, DateTime dueDate, int userId)
        {
            var newId = assignments.Count + 1;
            var newAssignment = new AssignmentModel
            {
                Id = newId,
                Subject = subject,
                Title = title,
                Description = description,
                DueDate = dueDate,
                CreatedByUserId = userId
            };

            assignments.Add(newAssignment);
        }

        public void UpdateAssignment(AssignmentModel assignment, string title, string description, DateTime dueDate)
        {
            assignment.Title = title;
            assignment.Description = description;
            assignment.DueDate = dueDate;
        }

        public void DeleteAssignment(ObservableCollection<AssignmentModel> assignments, AssignmentModel assignment)
        {
            assignments.Remove(assignment);
        }
    }
}