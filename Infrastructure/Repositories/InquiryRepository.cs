using CrmBackend.Application.DTOs.InquiryDtos;
using CrmBackend.Application.Interfaces;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Enums;
using CrmBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Infrastructure.Repositories
{
    public class InquiryRepository : IInquiryRepository
    {
        private readonly ApplicationDbContext _context;

        public InquiryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> AddCustomerFromInquiry(CreateInquiryRequest request, int branchId, Guid userId)
        {
            var newCustomer = new Customer
            {
                CustomerName = request.CustomerName,
                CustomerEmail = request.CustomerEmail,
                CustomerContact = request.CustomerContact,
                CustomerWhatsapp = request.CustomerWhatsapp,
                CustomerAddress = request.CustomerAddress,
                CustomerCity = request.CustomerCity,
                CustomerCountry = request.CustomerCountry,
                CustomerNationality = request.CustomerNationality,
                CustomerNotes = request.CustomerNotes,
                CustomerNextMeetingDate = request.CustomerNextMeetingDate?.ToUniversalTime(),
                ContactStatus = (ContactStatus)request.ContactStatus,
                WayOfContact = (WayOfContact)request.WayOfContact,
                IsVisitedShowroom = request.IsVisitedShowroom,
                CustomerTimeSpent = request.CustomerTimeSpent,
                CustomerAssignedTo = request.CustomerAssignedTo,
                BranchId = branchId,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = userId
            };

            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();
            return newCustomer;
        }


        public async Task<Building> AddBuildingAsync(CreateInquiryRequest request, Guid userId)
        {
            var building = new Building
            {
                BuildingAddress = request.BuildingAddress,
                BuildingTypeOfUnit = request.BuildingTypeOfUnit,
                BuildingCondition = request.BuildingCondition,
                BuildingFloor = request.BuildingFloor,
                BuildingLatitude = request.BuildingLatitude,
                BuildingLongitude = request.BuildingLongitude,
                BuildingMakaniMap = request.BuildingMakaniMap,
                BuildingReconstruction = request.IsUnderConstruction,
                IsOccupied = request.IsOccupied,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = userId
            };

            _context.Buildings.Add(building);
            await _context.SaveChangesAsync();
            return building;
        }

        public async Task<Inquiry> AddInquiryAsync(CreateInquiryRequest request, int customerId, int buildingId, int branchId, Guid createdBy)
        {
            var inquiry = new Inquiry
            {
                InquiryDescription = request.InquiryDescription,
                InquiryStartDate = DateTime.UtcNow,
                IsDesignProvidedByCustomer = request.IsDesignProvidedByCustomer,
                IsMeasurementProvidedByCustomer = request.IsMeasurementProvidedByCustomer,
                Status = InquiryStatus.MeasurementAssigneePending,
                CustomerId = customerId,
                BuildingId = buildingId,
                BranchId = branchId,
                ManagedBy= createdBy,
                CreatedBy = createdBy,
                CreatedDate = DateTime.UtcNow
            };

            _context.Inquiries.Add(inquiry);
            await _context.SaveChangesAsync();
            return inquiry;
        }

        public async Task AddInquiryWorkscopesAsync(int inquiryId, CreateInquiryRequest request, Guid createdBy)
        {
            foreach (var ws in request.Workscopes)
            {
                var exists = await _context.WorkScopes.AnyAsync(w => w.WorkScopeId == ws.WorkscopeId);
                if (exists)
                {
                    var inquiryWS = new InquiryWorkscope
                    {
                        InquiryId = inquiryId,
                        WorkScopeId = ws.WorkscopeId,
                        InquiryWorkscopeDetailName = ws.InquiryWorkscopeDetailName,
                        MeasurementAssignedTo = request.MeasurementAssignedTo,
                        MeasurementScheduleDate = request.MeasurementScheduleDate?.ToUniversalTime(),
                        CreatedBy = createdBy,
                        CreatedDate = DateTime.UtcNow
                    };

                    _context.InquiryWorkscopes.Add(inquiryWS);
                }
            }
        }



        public async Task<List<InquiryListDto>> GetAllForDisplayAsync(int branchId)
        {
            return await _context.Inquiries
                .Include(i => i.Customer)
                .Include(i => i.Building)
                .Include(i => i.InquiryWorkscopes)
                    .ThenInclude(iw => iw.WorkScope)
                .Include(i => i.InquiryWorkscopes)
                    .ThenInclude(iw => iw.MeasurementAssignedUser)
                .Include(i => i.InquiryWorkscopes)
                    .ThenInclude(iw => iw.DesignAssignedUser)
                .Include(i => i.ManagedByUser)
                .Where(i => i.BranchId == branchId && i.IsActive && !i.IsDeleted)
                .Select(i => new InquiryListDto
                {
                    InquiryId = i.InquiryId,
                    InquiryCode = i.InquiryCode,
                    InquiryDescription = i.InquiryDescription,
                    InquiryStartDate = i.InquiryStartDate,
                    InquiryEndDate = i.InquiryEndDate,
                    InquiryStatusName = i.Status.ToString(),
                    ManagedByUserName = i.ManagedByUser.FullName,

                    IsMeasurementProvidedByCustomer = i.IsMeasurementProvidedByCustomer,
                    IsDesignProvidedByCustomer = i.IsDesignProvidedByCustomer,

                    // ✅ Customer Info
                    CustomerName = i.Customer.CustomerName,
                    CustomerContact = i.Customer.CustomerContact,
                    CustomerEmail = i.Customer.CustomerEmail,
                    CustomerNotes = i.Customer.CustomerNotes,
                    CustomerNextMeetingDate = i.Customer.CustomerNextMeetingDate,
                    ContactStatus = i.Customer.ContactStatus.ToString(),
                    WayOfContact = i.Customer.WayOfContact.ToString(),

                    // ✅ Building Info
                    BuildingAddress = i.Building.BuildingAddress,
                    BuildingMakaniMap = i.Building.BuildingMakaniMap,
                    BuildingTypeOfUnit = i.Building.BuildingTypeOfUnit,
                    BuildingCondition = i.Building.BuildingCondition,
                    BuildingFloor = i.Building.BuildingFloor,
                    BuildingReconstruction = i.Building.BuildingReconstruction,
                    IsOccupied = i.Building.IsOccupied,

                    // ✅ Workscope Info
                    WorkscopeDetails = i.InquiryWorkscopes.Select(w => new InquiryWorkscopeDisplayDto
                    {
                        WorkscopeName = w.WorkScope.WorkScopeName,
                        InquiryWorkscopeDetailName = w.InquiryWorkscopeDetailName,
                        InquiryStatus = w.InquiryStatus.ToString(),
                        MeasurementAssignedTo = w.MeasurementAssignedUser.FullName,
                        DesignAssignedTo = w.DesignAssignedUser.FullName,
                        MeasurementScheduleDate = w.MeasurementScheduleDate,
                        DesignScheduleDate = w.DesignScheduleDate,
                        MeasurementAddedOn = w.MeasurementAddedOn,
                        DesignAddedOn = w.DesignAddedOn,
                        IsDesignApproved = w.IsDesignApproved,
                        IsDesignSentToCustomer = w.IsDesignSentToCustomer,
                        FeedbackReaction = w.FeedbackReaction.ToString(),
                        IsMeasurementReschedule = w.IsMeasurementReschedule,
                        IsDesignReschedule = w.IsDesignReschedule
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int customerId)
        {
            return await _context.Customers.FindAsync(customerId);
        }

        public async Task UpdateCustomerAsyncIfNeeded(Customer customer, CreateInquiryRequest request, Guid userId)
        {
            bool hasChanges = false;

            void SetIfChanged<T>(T currentValue, T newValue, Action<T> updateAction)
            {
                if (!EqualityComparer<T>.Default.Equals(currentValue, newValue))
                {
                    updateAction(newValue);
                    hasChanges = true;
                }
            }

            SetIfChanged(customer.CustomerName, request.CustomerName, val => customer.CustomerName = val);
            SetIfChanged(customer.CustomerEmail, request.CustomerEmail, val => customer.CustomerEmail = val);
            SetIfChanged(customer.CustomerContact, request.CustomerContact, val => customer.CustomerContact = val);
            SetIfChanged(customer.CustomerWhatsapp, request.CustomerWhatsapp, val => customer.CustomerWhatsapp = val);
            SetIfChanged(customer.CustomerAddress, request.CustomerAddress, val => customer.CustomerAddress = val);
            SetIfChanged(customer.CustomerCity, request.CustomerCity, val => customer.CustomerCity = val);
            SetIfChanged(customer.CustomerCountry, request.CustomerCountry, val => customer.CustomerCountry = val);
            SetIfChanged(customer.CustomerNationality, request.CustomerNationality, val => customer.CustomerNationality = val);
            SetIfChanged(customer.CustomerNotes, request.CustomerNotes, val => customer.CustomerNotes = val);

            var meetingDateUtc = request.CustomerNextMeetingDate?.ToUniversalTime();
            SetIfChanged(
                customer.CustomerNextMeetingDate?.Date,
                meetingDateUtc?.Date,
                val => customer.CustomerNextMeetingDate = meetingDateUtc
            );


            SetIfChanged(customer.ContactStatus, (ContactStatus)request.ContactStatus, val => customer.ContactStatus = val);
            SetIfChanged(customer.WayOfContact, (WayOfContact)request.WayOfContact, val => customer.WayOfContact = val);
            SetIfChanged(customer.IsVisitedShowroom, request.IsVisitedShowroom, val => customer.IsVisitedShowroom = val);
            SetIfChanged(customer.CustomerTimeSpent, request.CustomerTimeSpent, val => customer.CustomerTimeSpent = val);
            SetIfChanged(customer.CustomerAssignedTo, request.CustomerAssignedTo, val => customer.CustomerAssignedTo = val);

            if (hasChanges)
            {
                customer.UpdatedBy = userId;
                customer.UpdatedDate = DateTime.UtcNow;

                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
