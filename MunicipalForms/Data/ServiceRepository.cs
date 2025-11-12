using System;
using System.Collections.Generic;
using System.Linq;
using MunicipalForms.Models;

namespace MunicipalForms.Data
{
    public class ServiceRepository
    {
        private static readonly List<ServiceRequest> _requests = new();

        public ServiceRepository()
        {
            // Seed demo data only once
            if (!_requests.Any())
            {
                _requests.AddRange(new[]
                {
                    new ServiceRequest
                    {
                        Id = 1,
                        Category = "Electrical",
                        Description = "Streetlight not working near park.",
                        Status = RequestStatus.Pending,
                        Location = "Main Road, Ward 4",
                        Priority = 2
                    },
                    new ServiceRequest
                    {
                        Id = 2,
                        Category = "Plumbing",
                        Description = "Water leaking underground on Elm Street.",
                        Status = RequestStatus.InProgress,
                        Location = "Elm Street, Ward 2",
                        Priority = 1
                    },
                    new ServiceRequest
                    {
                        Id = 3,
                        Category = "Roads",
                        Description = "Large pothole near taxi rank.",
                        Status = RequestStatus.Completed,
                        Location = "Downtown, Ward 5",
                        Priority = 3
                    }
                });
            }
        }

        // everything below follows CRUD methods for managing servic requests
        public IEnumerable<ServiceRequest> GetAll() => _requests;

 
        public ServiceRequest GetById(int id) =>
            _requests.FirstOrDefault(r => r.Id == id);

        public void Add(ServiceRequest request)
        {
            request.Id = _requests.Count > 0 ? _requests.Max(r => r.Id) + 1 : 1;
            request.SubmittedAt = DateTime.Now;
            _requests.Add(request);
        }

        public void Update(ServiceRequest request)
        {
            var existing = GetById(request.Id);
            if (existing != null)
            {
                existing.Category = request.Category;
                existing.Description = request.Description;
                existing.Status = request.Status;
                existing.Location = request.Location;
                existing.Priority = request.Priority;
            }
        }
      
        public void Delete(int id)
        {
            var request = GetById(id);
            if (request != null)
                _requests.Remove(request);
        }
    }
}
